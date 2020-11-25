using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

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
