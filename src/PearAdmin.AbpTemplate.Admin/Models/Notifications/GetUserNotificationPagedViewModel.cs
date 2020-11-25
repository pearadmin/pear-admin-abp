using Abp.AutoMapper;
using Abp.Notifications;
using PearAdmin.AbpTemplate.Notifications.Dto;
using PearAdmin.AbpTemplate.Admin.Models.Common;

namespace PearAdmin.AbpTemplate.Admin.Models.Notifications
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
