using Abp.Domain.Services;

namespace PearAdmin.Abp
{
    public class AbpCoreServiceBase : DomainService
    {
        protected AbpCoreServiceBase()
        {
            LocalizationSourceName = AbpCoreConsts.LocalizationSourceName;
        }
    }
}
