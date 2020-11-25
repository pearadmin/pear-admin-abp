using Abp.AutoMapper;
using PearAdmin.Abp.Authorization.Permissions.Dto;
using PearAdmin.Abp.Admin.Models.Common;

namespace PearAdmin.Abp.Admin.Models.Permissions
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
