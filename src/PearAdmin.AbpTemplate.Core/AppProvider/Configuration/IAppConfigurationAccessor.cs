using Microsoft.Extensions.Configuration;

namespace PearAdmin.AbpTemplate.Configuration
{
    /// <summary>
    /// 应用配置读取访问接口
    /// </summary>
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
