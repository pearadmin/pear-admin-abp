using System.Collections.Generic;
using PearAdmin.Abp.Authorization.Roles.Dto;

namespace PearAdmin.Abp.Admin.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<RolePermissionDto> Permissions { get; set; }
    }
}