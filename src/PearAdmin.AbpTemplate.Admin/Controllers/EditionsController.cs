using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using PearAdmin.AbpTemplate.Admin.Models.Common;
using PearAdmin.AbpTemplate.MultiTenancy;
using PearAdmin.AbpTemplate.MultiTenancy.Editions;
using PearAdmin.AbpTemplate.MultiTenancy.Editions.Dto;

namespace PearAdmin.AbpTemplate.Admin.Controllers
{
    /// <summary>
    /// 版本管理控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class EditionsController : AbpTemplateControllerBase
    {
        #region 初始化
        private readonly IEditionAppService _editionAppService;
        private readonly TenantManager _tenantManager;

        public EditionsController(
            IEditionAppService editionAppService,
            TenantManager tenantManager)
        {
            _editionAppService = editionAppService;
            _tenantManager = tenantManager;
        }
        #endregion

        #region 版本管理
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取版本数据列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetEditionList()
        {
            var editionDtos = await _editionAppService.GetAllEdition();
            return Json(new ResponseParamListViewModel<EditionDto>(editionDtos.Items));
        }

        /// <summary>
        /// 新增或更新版本页面
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateOrUpdateEdition(NullableIdDto input)
        {
            var output = await _editionAppService.GetEditionForEdit(input);

            return View(output);
        }

        /// <summary>
        /// 新增版本
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreateEdition([FromBody]CreateEditionDto input)
        {
            await _editionAppService.CreateEdition(input);
            return Json(new ResponseParamViewModel(L("CreateEditionSuccessful")));
        }

        /// <summary>
        /// 更新版本
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UpdateEdition([FromBody]UpdateEditionDto input)
        {
            await _editionAppService.UpdateEdition(input);
            return Json(new ResponseParamViewModel(L("UpdateEditionSuccessful")));
        }

        /// <summary>
        /// 删除版本
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> DeleteEdition([FromBody]EntityDto input)
        {
            await _editionAppService.DeleteEdition(input);
            return Json(new ResponseParamViewModel(L("DeleteEditionSuccessful")));
        }
        #endregion
    }
}
