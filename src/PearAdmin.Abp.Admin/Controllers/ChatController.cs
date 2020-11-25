using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using System.Threading.Tasks;
using PearAdmin.Abp.Admin.Models.Common;
using PearAdmin.Abp.Social.Chat;
using PearAdmin.Abp.Social.Chat.Dto;

namespace PearAdmin.Abp.Admin.Controllers
{
    [AbpMvcAuthorize]
    public class ChatController : AbpControllerBase
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

        public async Task<JsonResult> GetIsExistUnreadMessage()
        {
            var isExistUnreadMessage = await _chatAppService.IsExistUnreadMessage();
            return Json(new ResponseParamSingleViewModel<bool>(isExistUnreadMessage));
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