namespace PearAdmin.AbpTemplate.MultiTenancy.TenantSetting.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}