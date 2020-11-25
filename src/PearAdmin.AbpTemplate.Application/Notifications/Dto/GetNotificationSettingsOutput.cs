using System;
using System.Collections.Generic;
using System.Text;

namespace PearAdmin.AbpTemplate.Notifications.Dto
{
    /// <summary>
    /// 获取消息设置Dto
    /// </summary>
    public class GetNotificationSettingsOutput
    {
        public bool ReceiveNotifications { get; set; }

        public List<NotificationSubscriptionWithDisplayNameDto> Notifications { get; set; }
    }
}
