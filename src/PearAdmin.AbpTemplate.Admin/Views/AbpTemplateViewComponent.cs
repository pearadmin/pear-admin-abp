using Abp.AspNetCore.Mvc.ViewComponents;

namespace PearAdmin.AbpTemplate.Admin.Views
{
    public abstract class AbpTemplateViewComponent : AbpViewComponent
    {
        protected AbpTemplateViewComponent()
        {
            LocalizationSourceName = AbpTemplateCoreConsts.LocalizationSourceName;
        }
    }
}
