using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using PearAdmin.Abp.Admin.Models.Common;
using PearAdmin.Abp.MultiTenancy.Editions;
using PearAdmin.Abp.MultiTenancy.Tenants;
using PearAdmin.Abp.MultiTenancy.Tenants.Dto;

namespace PearAdmin.Abp.Admin.Controllers
{
    /// <summary>
    /// 多租户控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class TenantsController : AbpControllerBase
    {
        #region 初始化
        private readonly ITenantAppService _tenantAppService;
        private readonly IEditionAppService _editionAppService;

        public TenantsController(ITenantAppService tenantAppService,
            IEditionAppService editionAppService)
        {
            _tenantAppService = tenantAppService;
            _editionAppService = editionAppService;
        }
        #endregion

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取租户列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetTenantList()
        {
            var tenantDtos = await _tenantAppService.GetAllTenant();
            return Json(new ResponseParamListViewModel<TenantDto>(tenantDtos.Items));
        }

        /// <summary>
        /// 租户版本变更
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IActionResult> ChangeTenantEdition(EntityDto<int> input)
        {
            var tenant = await _tenantAppService.GetTenantForEdit(input);
            var editionList = await _editionAppService.GetAllEdition();
            ViewBag.EditionList = editionList.Items;

            return View(tenant);
        }

        /// <summary>
        /// 租户版本变更
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> ChangeTenantEdition([FromBody]ChangeTenantEditionDto input)
        {
            await _tenantAppService.ChangeTenantEdition(input);
            return Json(new ResponseParamViewModel(L("EditionChangeSuccessful")));
        }
    }
}
