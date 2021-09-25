using System.Collections.Generic;

namespace PearAdmin.AbpTemplate.ExternalAuth
{
    public interface IExternalAuthConfiguration
    {
        List<IExternalLoginInfoProvider> Providers { get; }
    }
}
