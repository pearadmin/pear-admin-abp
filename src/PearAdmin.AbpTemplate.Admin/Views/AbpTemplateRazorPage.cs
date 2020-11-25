using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace PearAdmin.AbpTemplate.Admin.Views
{
    public abstract class AbpTemplateRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected AbpTemplateRazorPage()
        {
            LocalizationSourceName = AbpTemplateCoreConsts.LocalizationSourceName;
        }
    }
}
