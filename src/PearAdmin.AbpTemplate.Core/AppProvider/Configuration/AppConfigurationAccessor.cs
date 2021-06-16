using System.IO;
using Abp.Dependency;
using Microsoft.Extensions.Configuration;

namespace PearAdmin.AbpTemplate.Configuration
{
    public class AppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public AppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(Directory.GetCurrentDirectory());
        }
    }
}
