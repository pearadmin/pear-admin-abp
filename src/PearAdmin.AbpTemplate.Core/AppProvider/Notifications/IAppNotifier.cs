using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.Notifications
{
    /// <summary>
    /// 消息通知发布接口
    /// </summary>
    public interface IAppNotifier
    {
        Task NewDailyTaskAsync();
    }
}
