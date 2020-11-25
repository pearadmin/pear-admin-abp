using PearAdmin.Abp.Authorization.Roles.Dto;
using PearAdmin.Abp.Organizations.Dto;
using System.Collections.Generic;

namespace PearAdmin.Abp.Authorization.Users.Dto
{
    /// <summary>
    /// 编辑用户信息响应Dto
    /// </summary>
    public class GetUserForEditOutput
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserEditDto User { get; set; }

        /// <summary>
        /// 角色集合
        /// </summary>
        public List<UserRoleDto> Roles { get; set; }

        /// <summary>
        /// 组织机构集合
        /// </summary>
        public List<UserOrganizationUnitDto> OrganizationUnits { get; set; }
    }
}