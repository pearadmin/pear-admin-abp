using PearAdmin.AbpTemplate.CommonDto;

namespace PearAdmin.AbpTemplate.Authorization.Permissions.Dto
{
    /// <summary>
    /// 分页、筛选请求获取权限Dto
    /// </summary>
    public class GetPagedPermissionInput : PagedAndFilteredInputDto
    {
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }
    }
}
