using System.Collections.Generic;
using PearAdmin.AbpTemplate.Authorization.Roles.Dto;

namespace PearAdmin.AbpTemplate.Admin.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<RolePermissionDto> Permissions { get; set; }
    }
}