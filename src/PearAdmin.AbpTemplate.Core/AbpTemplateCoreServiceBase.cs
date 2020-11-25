using Abp.Domain.Services;

namespace PearAdmin.AbpTemplate
{
    public class AbpTemplateCoreServiceBase : DomainService
    {
        protected AbpTemplateCoreServiceBase()
        {
            LocalizationSourceName = AbpTemplateCoreConsts.LocalizationSourceName;
        }
    }
}
