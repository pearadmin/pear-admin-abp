using Abp.AutoMapper;
using Abp.Notifications;
using PearAdmin.AbpTemplate.Notifications.Dto;
using PearAdmin.AbpTemplate.Admin.Models.Common;
using System.Collections.Generic;

namespace PearAdmin.AbpTemplate.Admin.Models.Notifications
{
    /// <summary>
    /// 用户消息分页结果视图模型
    /// </summary>
    [AutoMapFrom(typeof(GetNotificationsOutput))]
    public class GetNotificationsResultViewModel : ResponseParamPagedViewModel<UserNotification>
    {
        public int UnreadCount { get; set; }

        public GetNotificationsResultViewModel(int totalCount, int unreadCount, IReadOnlyList<UserNotification> notifications)
            : base(totalCount, notifications)
        {
            UnreadCount = unreadCount;
        }
    }
}
