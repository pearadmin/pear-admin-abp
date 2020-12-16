using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using System.Threading.Tasks;
using PearAdmin.AbpTemplate.Admin.Models.Common;
using PearAdmin.AbpTemplate.Social.Chat;
using PearAdmin.AbpTemplate.Social.Chat.Dto;

namespace PearAdmin.AbpTemplate.Admin.Controllers
{
    [AbpMvcAuthorize]
    public class ChatController : AbpTemplateControllerBase
    {
        private readonly IChatAppService _chatAppService;

        public ChatController(IChatAppService chatAppService)
        {
            _chatAppService = chatAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetChatUserList()
        {
            var chatUserDtos = await _chatAppService.GetAllChatUser();
            return Json(new ResponseParamListViewModel<ChatUserDto>(chatUserDtos.Items));
        }

        public async Task<JsonResult> GetUnreadMessageCount()
        {
            var unreadMessageCount = await _chatAppService.UnreadMessageCount();
            return Json(new ResponseParamSingleViewModel<int>(unreadMessageCount));
        }

        public async Task<JsonResult> GetUserChatMessageList(long userId)
        {
            var userChatMessages = await _chatAppService.GetUserChatMessages(new GetUserChatMessagesInput()
            {
                UserId = userId
            });
            return Json(new ResponseParamListViewModel<ChatMessageDto>(userChatMessages.Items));
        }

        public async Task MarkAllUnreadMessagesOfUserAsRead(long userId)
        {
            await _chatAppService.MarkAllUnreadMessagesOfUserAsRead(new MarkAllUnreadMessagesOfUserAsReadInput()
            {
                UserId = userId
            });
        }
    }
}