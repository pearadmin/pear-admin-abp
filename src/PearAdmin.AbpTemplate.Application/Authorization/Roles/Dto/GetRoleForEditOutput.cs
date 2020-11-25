using System.Collections.Generic;

namespace PearAdmin.AbpTemplate.Authorization.Roles.Dto
{
    /// <summary>
    /// 角色信息(包含授权权限)响应Dto
    /// </summary>
    public class GetRoleForEditOutput
    {
        /// <summary>
        /// 角色信息
        /// </summary>
        public RoleDto Role { get; set; }

        /// <summary>
        /// 所有权限列表集合
        /// </summary>
        public List<RolePermissionDto> Permissions { get; set; }
    }
}