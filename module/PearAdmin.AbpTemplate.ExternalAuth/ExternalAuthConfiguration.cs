using System.Collections.Generic;
using Abp.Dependency;

namespace PearAdmin.AbpTemplate.ExternalAuth
{
    public class ExternalAuthConfiguration : IExternalAuthConfiguration, ISingletonDependency
    {
        public List<IExternalLoginInfoProvider> Providers { get; }

        public ExternalAuthConfiguration()
        {
            Providers = new List<IExternalLoginInfoProvider>();
        }
    }
}
