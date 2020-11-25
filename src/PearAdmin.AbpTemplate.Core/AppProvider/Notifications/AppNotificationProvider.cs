using Abp.Authorization;
using Abp.Localization;
using Abp.Notifications;
using PearAdmin.AbpTemplate.Authorization;

namespace PearAdmin.AbpTemplate.Notifications
{
    /// <summary>
    /// 通知定义
    /// </summary>
    public class AppNotificationProvider : NotificationProvider
    {
        /// <summary>
        /// 设置通知定义
        /// </summary>
        /// <param name="context"></param>
        public override void SetNotifications(INotificationDefinitionContext context)
        {
            context.Manager.Add(
                new NotificationDefinition(
                    AppNotificationNames.NewDailyTask,
                    displayName: L("NewDailyTask"),
                    permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_TaskCenter_DailyTasks)
                )
            );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpTemplateCoreConsts.LocalizationSourceName);
        }
    }
}
