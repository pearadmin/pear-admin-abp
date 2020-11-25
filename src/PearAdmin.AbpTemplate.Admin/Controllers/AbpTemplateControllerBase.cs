using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;
using PearAdmin.AbpTemplate.CommonDto;
using PearAdmin.AbpTemplate.Admin.Models.Common;

namespace PearAdmin.AbpTemplate.Admin.Controllers
{
    public abstract class AbpTemplateControllerBase : AbpController
    {
        protected AbpTemplateControllerBase()
        {
            LocalizationSourceName = AbpTemplateCoreConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        protected SPagedInput PagedViewModelMapToPagedInputDto<TViewModel, SPagedInput>(TViewModel viewModel)
           where SPagedInput : PagedInputDto
           where TViewModel : PagedViewModel
        {
            var input = ObjectMapper.Map<SPagedInput>(viewModel);
            input.MaxResultCount = viewModel.Limit;
            input.SkipCount = (viewModel.Page - 1) * viewModel.Limit;

            return input;
        }
    }
}
