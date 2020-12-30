using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using PearAdmin.AbpTemplate.Authorization.Permissions;
using PearAdmin.AbpTemplate.Authorization.Permissions.Dto;
using PearAdmin.AbpTemplate.Admin.Models.Common;
using PearAdmin.AbpTemplate.Admin.Models.Permissions;

namespace PearAdmin.AbpTemplate.Admin.Controllers
{
    /// <summary>
    /// 权限管理控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class PermissionsController : AbpTemplateControllerBase
    {
        #region 初始化
        private readonly IPermissionAppService _permissionAppService;

        public PermissionsController(IPermissionAppService permissionAppService)
        {
            _permissionAppService = permissionAppService;
        }
        #endregion

        #region 权限管理
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 权限列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetPermissionList(GetPagedPermissionViewModel viewModel)
        {
            var input = PagedViewModelMapToPagedInputDto<GetPagedPermissionViewModel, GetPagedPermissionInput>(viewModel);

            var pagedPermissionList = _permissionAppService.GetPagedPermission(input);

            return Json(new ResponseParamPagedViewModel<PermissionDto>(pagedPermissionList.TotalCount, pagedPermissionList.Items));
        }
        #endregion
    }
}