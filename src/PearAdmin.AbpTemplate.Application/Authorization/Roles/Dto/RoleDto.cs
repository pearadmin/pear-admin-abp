using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;

namespace PearAdmin.AbpTemplate.Authorization.Roles.Dto
{
    public class RoleDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string NormalizedName { get; set; }

        public string Description { get; set; }

        public bool IsStatic { get; set; }

        public bool IsDefault { get; set; }

        public DateTime CreationTime { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}