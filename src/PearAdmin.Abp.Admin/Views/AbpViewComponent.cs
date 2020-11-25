using Abp.AspNetCore.Mvc.ViewComponents;

namespace PearAdmin.Abp.Admin.Views
{
    public abstract class AbpViewComponent : AbpViewComponent
    {
        protected AbpViewComponent()
        {
            LocalizationSourceName = AbpCoreConsts.LocalizationSourceName;
        }
    }
}
