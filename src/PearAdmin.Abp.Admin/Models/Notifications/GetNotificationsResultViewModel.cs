using Abp.AutoMapper;
using Abp.Notifications;
using PearAdmin.Abp.Notifications.Dto;
using PearAdmin.Abp.Admin.Models.Common;
using System.Collections.Generic;

namespace PearAdmin.Abp.Admin.Models.Notifications
{
    /// <summary>
    /// 用户消息分页结果视图模型
    /// </summary>
    [AutoMapFrom(typeof(GetNotificationsOutput))]
    public class GetNotificationsResultViewModel : PagedResultViewModel<UserNotification>
    {
        public int UnreadCount { get; set; }

        public GetNotificationsResultViewModel(int totalCount, int unreadCount, IReadOnlyList<UserNotification> notifications)
            : base(totalCount, notifications)
        {
            UnreadCount = unreadCount;
        }
    }
}
