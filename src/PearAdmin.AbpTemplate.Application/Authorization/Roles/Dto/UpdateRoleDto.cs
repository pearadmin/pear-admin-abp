using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.Authorization.Roles;

namespace PearAdmin.AbpTemplate.Authorization.Roles.Dto
{
    public class UpdateRoleDto : EntityDto<int>
    {
        [Required]
        [StringLength(AbpRoleBase.MaxNameLength)]
        public string Name { get; set; }

        [StringLength(Role.MaxDescriptionLength)]
        public string Description { get; set; }

        public bool IsDefault { get; set; }

        [Required]
        public List<string> GrantedPermissionNames { get; set; }
    }
}