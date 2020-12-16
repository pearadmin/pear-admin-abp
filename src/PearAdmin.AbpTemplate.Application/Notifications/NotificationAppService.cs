using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp.UI;
using PearAdmin.AbpTemplate.Notifications.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.Notifications
{
    public class NotificationAppService : AbpTemplateApplicationServiceBase, INotificationAppService
    {
        #region 初始化
        private readonly INotificationDefinitionManager _notificationDefinitionManager;
        private readonly INotificationSubscriptionManager _notificationSubscriptionManager;
        private readonly IUserNotificationManager _userNotificationManager;

        public NotificationAppService(
            INotificationDefinitionManager notificationDefinitionManager,
            INotificationSubscriptionManager notificationSubscriptionManager,
            IUserNotificationManager userNotificationManager)
        {
            _notificationDefinitionManager = notificationDefinitionManager;
            _notificationSubscriptionManager = notificationSubscriptionManager;
            _userNotificationManager = userNotificationManager;
        }
        #endregion

        public async Task<GetNotificationSettingsOutput> GetNotificationSettings()
        {
            var output = new GetNotificationSettingsOutput
            {
                ReceiveNotifications = true,
            };

            #region 消息定义列表
            var notificationDefinitions = (await _notificationDefinitionManager.GetAllAvailableAsync(AbpSession.ToUserIdentifier())).Where(nd => nd.EntityType == null);

            output.Notifications = ObjectMapper.Map<List<NotificationSubscriptionWithDisplayNameDto>>(notificationDefinitions);
            #endregion

            #region 已经订阅的消息定义
            var subscribedNotifications = (await _notificationSubscriptionManager
               .GetSubscribedNotificationsAsync(AbpSession.ToUserIdentifier()))
               .Select(ns => ns.NotificationName)
               .ToList();

            output.Notifications.ForEach(n => n.IsSubscribed = subscribedNotifications.Contains(n.Name));
            #endregion

            return output;
        }

        public async Task UpdateNotificationSettings(UpdateNotificationSettingsInput input)
        {
            foreach (var notification in input.Notifications)
            {
                if (notification.IsSubscribed)
                {
                    await _notificationSubscriptionManager.SubscribeAsync(AbpSession.ToUserIdentifier(), notification.Name);
                }
                else
                {
                    await _notificationSubscriptionManager.UnsubscribeAsync(AbpSession.ToUserIdentifier(), notification.Name);
                }
            }
        }

        [DisableAuditing]
        public async Task<GetNotificationsOutput> GetPagedUserNotifications(GetUserNotificationsPagedInput input)
        {
            var totalCount = await _userNotificationManager.GetUserNotificationCountAsync(AbpSession.ToUserIdentifier(), input.State);

            var unreadCount = await _userNotificationManager.GetUserNotificationCountAsync(AbpSession.ToUserIdentifier(), UserNotificationState.Unread);

            var notifications = await _userNotificationManager.GetUserNotificationsAsync(AbpSession.ToUserIdentifier(), input.State, input.SkipCount, input.MaxResultCount);

            return new GetNotificationsOutput(totalCount, unreadCount, notifications);
        }

        public async Task<int> UnreadMessageCount()
        {
            var unreadCount = await _userNotificationManager.GetUserNotificationCountAsync(AbpSession.ToUserIdentifier(), UserNotificationState.Unread);

            return unreadCount;
        }

        public async Task SetNotificationAsRead(List<EntityDto<Guid>> inputs)
        {
            foreach (var input in inputs)
            {
                var userNotification = await _userNotificationManager.GetUserNotificationAsync(AbpSession.TenantId, input.Id);
                if (userNotification.UserId != AbpSession.GetUserId())
                {
                    throw new Exception($"Given user notification id({input.Id}) is not belong to the current user ({AbpSession.UserId})");
                }

                await _userNotificationManager.UpdateUserNotificationStateAsync(AbpSession.TenantId, input.Id, UserNotificationState.Read);
            }
        }

        public async Task SetAllNotificationsAsRead()
        {
            await _userNotificationManager.UpdateAllUserNotificationStatesAsync(AbpSession.ToUserIdentifier(), UserNotificationState.Read);
        }

        public async Task DeleteNotification(List<EntityDto<Guid>> inputs)
        {
            foreach (var input in inputs)
            {
                var notification = await _userNotificationManager.GetUserNotificationAsync(AbpSession.TenantId, input.Id);
                if (notification.UserId != AbpSession.GetUserId())
                {
                    throw new UserFriendlyException(L("ThisNotificationDoesntBelongToYou"));
                }

                await _userNotificationManager.DeleteUserNotificationAsync(AbpSession.TenantId, input.Id);
            }
        }
    }
}
