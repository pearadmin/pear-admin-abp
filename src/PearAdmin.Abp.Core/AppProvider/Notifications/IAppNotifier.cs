using System.Threading.Tasks;

namespace PearAdmin.Abp.Notifications
{
    /// <summary>
    /// 消息通知发布接口
    /// </summary>
    public interface IAppNotifier
    {
        Task NewDailyTaskAsync();
    }
}
