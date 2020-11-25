using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace PearAdmin.Abp.Admin.Views
{
    public abstract class AbpRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected AbpRazorPage()
        {
            LocalizationSourceName = AbpCoreConsts.LocalizationSourceName;
        }
    }
}
