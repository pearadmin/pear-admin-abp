using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PearAdmin.Abp.Social.Chat.Dto;

namespace PearAdmin.Abp.Social.Chat
{
    public interface IChatAppService : IApplicationService
    {
        Task<ListResultDto<ChatUserDto>> GetAllChatUser();

        Task<ListResultDto<ChatMessageDto>> GetUserChatMessages(GetUserChatMessagesInput input);

        Task<bool> IsExistUnreadMessage();

        Task MarkAllUnreadMessagesOfUserAsRead(MarkAllUnreadMessagesOfUserAsReadInput input);
    }
}
