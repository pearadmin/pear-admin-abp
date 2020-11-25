using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PearAdmin.AbpTemplate.Social.Chat.Dto;

namespace PearAdmin.AbpTemplate.Social.Chat
{
    public interface IChatAppService : IApplicationService
    {
        Task<ListResultDto<ChatUserDto>> GetAllChatUser();

        Task<ListResultDto<ChatMessageDto>> GetUserChatMessages(GetUserChatMessagesInput input);

        Task<bool> IsExistUnreadMessage();

        Task MarkAllUnreadMessagesOfUserAsRead(MarkAllUnreadMessagesOfUserAsReadInput input);
    }
}
