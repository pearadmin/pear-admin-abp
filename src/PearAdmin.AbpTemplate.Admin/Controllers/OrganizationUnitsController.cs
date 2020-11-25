using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using PearAdmin.AbpTemplate.Organizations;
using System.Threading.Tasks;
using PearAdmin.AbpTemplate.Admin.Models.Common;
using Abp.Application.Services.Dto;
using System.Collections.Generic;
using PearAdmin.AbpTemplate.Organizations.Dto;
using PearAdmin.AbpTemplate.Admin.Models.OrganizationUnits;
using Abp.Web.Models;

namespace PearAdmin.AbpTemplate.Admin.Controllers
{
    /// <summary>
    /// 组织机构控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class OrganizationUnitsController : AbpTemplateControllerBase
    {
        #region 初始化
        private readonly IOrganizationUnitAppService _organizationUnitAppService;

        public OrganizationUnitsController(IOrganizationUnitAppService organizationUnitAppService)
        {
            _organizationUnitAppService = organizationUnitAppService;
        }
        #endregion

        /// <summary>
        /// 组织机构首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取组织机构列表
        /// </summary>
        /// <returns></returns>
        [DontWrapResult]
        public async Task<JsonResult> GetOrganizationUnitList()
        {
            var organizationUnitDtos = await _organizationUnitAppService.GetAllOrganizationUnitTree();
            return Json(new ResponseParamListViewModel<OrganizationUnitDto>(organizationUnitDtos.Items));
        }

        /// <summary>
        /// 获取组织机构列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetPagedOrganizationUnit(GetPagedOrganizationUnitViewModel viewModel)
        {
            var input = PagedViewModelMapToPagedInputDto<GetPagedOrganizationUnitViewModel, GetPagedOrganizationUnitInput>(viewModel);

            var organizationList = await _organizationUnitAppService.GetPagedOrganizationUnit(input);

            return Json(new PagedResultViewModel<OrganizationUnitDto>(organizationList.TotalCount, organizationList.Items));
        }

        /// <summary>
        /// 创建或更新组织机构
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateOrUpdateOrganizationUnit(OrganizationUnitRequestViewModel viewModel)
        {
            var output = await _organizationUnitAppService.GetOrganizationUnitForEdit(new NullableIdDto<long>(viewModel.Id));

            if (!viewModel.Id.HasValue)
            {
                output.ParentId = viewModel.ParentId;
            }

            return View(output);
        }

        /// <summary>
        /// 创建组织机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreateOrganizationUnit([FromBody]CreateOrganizationUnitDto input)
        {
            await _organizationUnitAppService.CreateOrganizationUnit(input);

            return Json(new ResponseParamViewModel(L("新增机构成功")));
        }

        /// <summary>
        /// 更新组织机构
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UpdateOrganizationUnit([FromBody]UpdateOrganizationUnitDto input)
        {
            await _organizationUnitAppService.UpdateOrganizationUnit(input);

            return Json(new ResponseParamViewModel(L("更新机构成功")));
        }

        /// <summary>
        /// 移动一个机构到另一个机构下
        /// </summary>
        /// <param name="moveOrganizationUnitInput"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> MoveOrganizationUnit([FromBody]MoveOrganizationUnitInput input)
        {
            var organizationUnitDto = await _organizationUnitAppService.MoveOrganizationUnit(input);

            return Json(new ResponseParamSingleViewModel<OrganizationUnitDto>(organizationUnitDto));
        }

        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> DeleteOrganizationUnit([FromBody]List<EntityDto<long>> input)
        {
            await _organizationUnitAppService.DeleteOrganizationUnit(input);

            return Json(new ResponseParamViewModel(L("删除机构成功")));
        }
    }
}
