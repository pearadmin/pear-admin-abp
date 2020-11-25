using System.ComponentModel.DataAnnotations;

namespace PearAdmin.Abp.Host.HostSettings.Dto
{
    public class HostSettingsEditDto
    {
        [Required]
        public GeneralSettingsEditDto General { get; set; }

        [Required]
        public HostUserManagementSettingsEditDto UserManagement { get; set; }

        [Required]
        public TenantManagementSettingsEditDto TenantManagement { get; set; }

        public HostOtherSettingsEditDto OtherSettings { get; set; }
    }
}