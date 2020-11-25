using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using PearAdmin.Abp.Admin.Models.Common;
using PearAdmin.Abp.Admin.Models.Notifications;
using PearAdmin.Abp.Notifications;
using PearAdmin.Abp.Notifications.Dto;

namespace PearAdmin.Abp.Admin.Controllers
{
    /// <summary>
    /// 消息管理控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class NotificationsController : AbpControllerBase
    {
        private readonly INotificationAppService _notificationAppService;

        public NotificationsController(INotificationAppService notificationAppService)
        {
            _notificationAppService = notificationAppService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 根据分页条件获取消息列表
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetUserNotificationList(GetUserNotificationPagedViewModel viewModel)
        {
            var input = PagedViewModelMapToPagedInputDto<GetUserNotificationPagedViewModel, GetUserNotificationsPagedInput>(viewModel);
            var pagedUserNotificationDto = await _notificationAppService.GetPagedUserNotifications(input);

            return Json(new GetNotificationsResultViewModel(pagedUserNotificationDto.TotalCount, pagedUserNotificationDto.UnreadCount, pagedUserNotificationDto.Items));
        }

        /// <summary>
        /// 设置消息记录全部已读
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SetAllNotificationsAsRead()
        {
            await _notificationAppService.SetAllNotificationsAsRead();

            return Json(new ResponseParamViewModel(L("AllNotificationSetAsReadSuccessful")));
        }

        /// <summary>
        /// 设置指定消息记录已读
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SetNotificationsAsRead([FromBody]List<EntityDto<Guid>> input)
        {
            await _notificationAppService.SetNotificationAsRead(input);

            return Json(new ResponseParamViewModel(L("NotificationSetAsReadSuccessful")));
        }

        /// <summary>
        /// 删除消息记录
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> DeleteNotification([FromBody]List<EntityDto<Guid>> input)
        {
            await _notificationAppService.DeleteNotification(input);

            return Json(new ResponseParamViewModel(L("DeleteNotificationSuccessful")));
        }
    }
}
