using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using PearAdmin.AbpTemplate.Admin.Models.Roles;
using PearAdmin.AbpTemplate.Authorization.Roles;
using PearAdmin.AbpTemplate.Authorization.Roles.Dto;
using PearAdmin.AbpTemplate.Admin.Models.Common;
using System.Collections.Generic;

namespace PearAdmin.AbpTemplate.Admin.Controllers
{
    [AbpMvcAuthorize]
    public class RolesController : AbpTemplateControllerBase
    {
        #region 初始化
        private readonly IRoleAppService _roleAppService;

        public RolesController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;
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
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetRoleList(GetPagedRoleViewModel viewModel)
        {
            var input = PagedViewModelMapToPagedInputDto<GetPagedRoleViewModel, GetPagedRoleInput>(viewModel);

            var pagedRoleList = await _roleAppService.GetPagedRole(input);

            return Json(new PagedResultViewModel<RoleDto>(pagedRoleList.TotalCount, pagedRoleList.Items));
        }

        /// <summary>
        /// 创建或更新角色
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> CreateOrUpdateRole(NullableIdDto input)
        {
            var getRoleForEditOutput = await _roleAppService.GetRoleForEdit(input);

            return View(getRoleForEditOutput);
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreateRole([FromBody]CreateRoleDto input)
        {
            await _roleAppService.CreateRole(input);

            return Json(new ResponseParamViewModel(L("CreateRoleSuccessful")));
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UpdateRole([FromBody]UpdateRoleDto input)
        {
            await _roleAppService.UpdateRole(input);

            return Json(new ResponseParamViewModel(L("UpdateRoleSuccessful")));
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> DeleteRole([FromBody]List<EntityDto<int>> input)
        {
            await _roleAppService.DeleteRole(input);

            return Json(new ResponseParamViewModel(L("DeleteRoleSuccessful")));
        }
    }
}
