using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using PearAdmin.AbpTemplate.MultiTenancy.TenantSetting;
using PearAdmin.AbpTemplate.MultiTenancy.TenantSetting.Dto;

namespace PearAdmin.AbpTemplate.Admin.Controllers
{
    /// <summary>
    /// 租户设置控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class TenantSettingsController : AbpTemplateControllerBase
    {
        private readonly ITenantSettingsAppService _tenantSettingsAppService;

        public TenantSettingsController(ITenantSettingsAppService tenantSettingsAppService)
        {
            _tenantSettingsAppService = tenantSettingsAppService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var settings = await _tenantSettingsAppService.GetAllSettings();

            return View(settings);
        }

        /// <summary>
        /// 更新设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UpdateAllSettings([FromBody]TenantSettingsEditDto input)
        {
            await _tenantSettingsAppService.UpdateAllSettings(input);
            return Json(new { code = 200, msg = "更新设置成功" });
        }
    }
}