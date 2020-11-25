using Abp.AutoMapper;
using Abp.Notifications;
using PearAdmin.Abp.Notifications.Dto;
using PearAdmin.Abp.Admin.Models.Common;

namespace PearAdmin.Abp.Admin.Models.Notifications
{
    /// <summary>
    /// 消息分页模型
    /// </summary>
    [AutoMapTo(typeof(GetUserNotificationsPagedInput))]
    public class GetUserNotificationPagedViewModel : PagedViewModel
    {
        public UserNotificationState? State { get; set; }

        //扩展...
    }
}
