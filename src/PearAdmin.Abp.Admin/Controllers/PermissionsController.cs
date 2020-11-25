using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using PearAdmin.Abp.Authorization.Permissions;
using PearAdmin.Abp.Authorization.Permissions.Dto;
using PearAdmin.Abp.Admin.Models.Common;
using PearAdmin.Abp.Admin.Models.Permissions;

namespace PearAdmin.Abp.Admin.Controllers
{
    /// <summary>
    /// 权限管理控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class PermissionsController : AbpControllerBase
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

            return Json(new PagedResultViewModel<PermissionDto>(pagedPermissionList.TotalCount, pagedPermissionList.Items));
        }
        #endregion
    }
}