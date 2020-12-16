using System.Collections.Generic;
using Abp.Domain.Repositories;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Linq.Extensions;
using Abp.RealTime;
using Abp.Runtime.Session;
using Abp.Timing;
using Microsoft.EntityFrameworkCore;
using PearAdmin.AbpTemplate.Social.Friendships.Cache;
using PearAdmin.AbpTemplate.Social.Chat.Dto;
using PearAdmin.AbpTemplate.Social.Friendships.Dto;
using PearAdmin.AbpTemplate.Authorization.Users.Dto;
using PearAdmin.AbpTemplate.Authorization.Users;

namespace PearAdmin.AbpTemplate.Social.Chat
{
    [AbpAuthorize]
    public class ChatAppService : AbpTemplateApplicationServiceBase, IChatAppService
    {
        private readonly IRepository<ChatMessage, long> _chatMessageRepository;
        private readonly IUserFriendsCache _userFriendsCache;
        private readonly IOnlineClientManager<ChatChannel> _onlineClientManager;
        private readonly IChatCommunicator _chatCommunicator;
        private readonly IRepository<User, long> _userRepository;

        public ChatAppService(
            IRepository<ChatMessage, long> chatMessageRepository,
            IUserFriendsCache userFriendsCache,
            IOnlineClientManager<ChatChannel> onlineClientManager,
            IChatCommunicator chatCommunicator,
            IRepository<User, long> userRepository)
        {
            _chatMessageRepository = chatMessageRepository;
            _userFriendsCache = userFriendsCache;
            _onlineClientManager = onlineClientManager;
            _chatCommunicator = chatCommunicator;
            _userRepository = userRepository;
        }

        [DisableAuditing]
        public async Task<ListResultDto<ChatUserDto>> GetAllChatUser()
        {
            var userList = await _userRepository.GetAll()
                .Where(u => u.Id != AbpSession.UserId)
                .ToListAsync();

            var chatUserDtos = new List<ChatUserDto>();
            foreach (var user in userList)
            {
                var targetUserIdentityfier = new UserIdentifier(user.TenantId, user.Id);
                var isOnline = _onlineClientManager.IsOnline(targetUserIdentityfier);
                var unReadMessageCount = await _chatMessageRepository.CountAsync(cm => 
                        cm.UserId == AbpSession.UserId &&
                        cm.TargetUserId == user.Id &&
                        cm.TargetTenantId == user.TenantId &&
                        cm.ReadState == ChatMessageReadState.Unread);

                chatUserDtos.Add(new ChatUserDto()
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    ProfilePictureId = user.ProfilePictureId,
                    UnreadMessageCount = unReadMessageCount,
                    IsOnline = isOnline,
                });
            }

            return new ListResultDto<ChatUserDto>(chatUserDtos);
        }

        [DisableAuditing]
        public async Task<ListResultDto<ChatMessageDto>> GetUserChatMessages(GetUserChatMessagesInput input)
        {
            input.TenantId = AbpSession.TenantId;

            var userId = AbpSession.GetUserId();
            var messages = await _chatMessageRepository.GetAll()
                    .WhereIf(input.MinMessageId.HasValue, m => m.Id < input.MinMessageId.Value)
                    .Where(m => m.UserId == userId && m.TargetTenantId == input.TenantId && m.TargetUserId == input.UserId)
                    .OrderByDescending(m => m.CreationTime)
                    .Take(50)
                    .ToListAsync();

            messages.Reverse();

            return new ListResultDto<ChatMessageDto>(ObjectMapper.Map<List<ChatMessageDto>>(messages));
        }

        public async Task<int> UnreadMessageCount()
        {
            var userId = AbpSession.GetUserId();
            var unreadMessageCount = await _chatMessageRepository.GetAll()
                    .Where(m => m.UserId == userId && m.ReadState == ChatMessageReadState.Unread)
                    .CountAsync();

            return unreadMessageCount;
        }

        public async Task MarkAllUnreadMessagesOfUserAsRead(MarkAllUnreadMessagesOfUserAsReadInput input)
        {
            var userId = AbpSession.GetUserId();
            var tenantId = AbpSession.TenantId;
            input.TenantId = AbpSession.TenantId;

            // receiver messages
            var messages = await _chatMessageRepository
                 .GetAll()
                 .Where(m =>
                        m.UserId == userId &&
                        m.TargetTenantId == input.TenantId &&
                        m.TargetUserId == input.UserId &&
                        m.ReadState == ChatMessageReadState.Unread)
                 .ToListAsync();

            if (!messages.Any())
            {
                return;
            }

            foreach (var message in messages)
            {
                message.ChangeReadState(ChatMessageReadState.Read);
            }

            // sender messages
            using (CurrentUnitOfWork.SetTenantId(input.TenantId))
            {
                var reverseMessages = await _chatMessageRepository.GetAll()
                    .Where(m => m.UserId == input.UserId && m.TargetTenantId == tenantId && m.TargetUserId == userId)
                    .ToListAsync();

                if (!reverseMessages.Any())
                {
                    return;
                }

                foreach (var message in reverseMessages)
                {
                    message.ChangeReceiverReadState(ChatMessageReadState.Read);
                }
            }

            var userIdentifier = AbpSession.ToUserIdentifier();
            var friendIdentifier = input.ToUserIdentifier();

            _userFriendsCache.ResetUnreadMessageCount(userIdentifier, friendIdentifier);

            var onlineUserClients = _onlineClientManager.GetAllByUserId(userIdentifier);
            if (onlineUserClients.Any())
            {
                await _chatCommunicator.SendAllUnreadMessagesOfUserReadToClients(onlineUserClients, friendIdentifier);
            }

            var onlineFriendClients = _onlineClientManager.GetAllByUserId(friendIdentifier);
            if (onlineFriendClients.Any())
            {
                await _chatCommunicator.SendReadStateChangeToClients(onlineFriendClients, userIdentifier);
            }
        }
    }
}