using System;
using Abp;
using Castle.Facilities.Logging;
using Microsoft.Extensions.Configuration;
using PearAdmin.AbpTemplate.NLog;

namespace PearAdmin.AbpTemplate.Admin.Extensions
{
    public class AbpBootstrapperOptionsExtension
    {
        public static Action<AbpBootstrapperOptions> GetOptions(IConfiguration configuration)
        {
            return options => options.IocManager.IocContainer
                    .AddFacility<LoggingFacility>(f => f.UseAbpNLog(configuration.GetSection("NLog")));
        }
    }
}
