using System;
using System.Collections.Generic;
using System.Text;

namespace PearAdmin.Abp.Notifications.Dto
{
    /// <summary>
    /// 更新消息设置Dto
    /// </summary>
    public class UpdateNotificationSettingsInput
    {
        public bool ReceiveNotifications { get; set; }

        public List<NotificationSubscriptionDto> Notifications { get; set; }
    }
}
