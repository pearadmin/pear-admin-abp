using Abp.AutoMapper;
using PearAdmin.AbpTemplate.Authorization.Permissions.Dto;
using PearAdmin.AbpTemplate.Admin.Models.Common;

namespace PearAdmin.AbpTemplate.Admin.Models.Permissions
{
    /// <summary>
    /// 权限分页模型
    /// </summary>
    [AutoMapTo(typeof(GetPagedPermissionInput))]
    public class GetPagedPermissionViewModel : PagedViewModel
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }
    }
}
