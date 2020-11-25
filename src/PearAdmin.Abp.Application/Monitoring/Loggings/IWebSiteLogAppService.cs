using Abp.Application.Services;
using PearAdmin.Abp.CommonDto;
using PearAdmin.Abp.Loggings.Dto;

namespace PearAdmin.Abp.Loggings
{
    /// <summary>
    /// 网站运行日志应用层服务
    /// </summary>
    public interface IWebSiteLogAppService : IApplicationService
    {
        /// <summary>
        /// 获取最近的一个日志文件
        /// </summary>
        /// <returns></returns>
        GetLatestWebLogsOutput GetLatestWebLogs();

        /// <summary>
        /// 下载所有的日志文件
        /// </summary>
        /// <returns></returns>
        FileDto DownloadWebLogs();
    }
}
