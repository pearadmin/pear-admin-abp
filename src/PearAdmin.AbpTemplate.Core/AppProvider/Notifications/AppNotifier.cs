using Abp.Domain.Services;
using Abp.Notifications;
using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.Notifications
{
    /// <summary>
    /// 消息通知发布实例
    /// </summary>
    public class AppNotifier : DomainService, IAppNotifier
    {
        #region 初始化
        private readonly INotificationPublisher _notificationPublisher;

        public AppNotifier(INotificationPublisher notificationPublisher)
        {
            _notificationPublisher = notificationPublisher;
        }
        #endregion

        #region 任务提醒
        public async Task NewDailyTaskAsync()
        {
            var message = "新的日常任务已创建";
            await _notificationPublisher.PublishAsync(AppNotificationNames.NewDailyTask, new MessageNotificationData(message));
        }
        #endregion
    }
}
