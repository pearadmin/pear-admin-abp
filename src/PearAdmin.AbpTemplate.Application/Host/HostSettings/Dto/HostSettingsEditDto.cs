using System.ComponentModel.DataAnnotations;

namespace PearAdmin.AbpTemplate.Host.HostSettings.Dto
{
    public class HostSettingsEditDto
    {
        [Required]
        public GeneralSettingsEditDto General { get; set; }

        [Required]
        public HostManagementSettingsEditDto HostManagement { get; set; }
    }
}