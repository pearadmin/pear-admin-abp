using System;
using System.Collections.Generic;
using System.Text;

namespace PearAdmin.Abp.Notifications.Dto
{
    /// <summary>
    /// 消息订阅信息展示Dto
    /// </summary>
    public class NotificationSubscriptionWithDisplayNameDto : NotificationSubscriptionDto
    {
        public string DisplayName { get; set; }

        public string Description { get; set; }
    }
}
