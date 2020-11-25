using PearAdmin.Abp.Security;

namespace PearAdmin.Abp.MultiTenancy.TenantSetting.Dto
{
    public class SecuritySettingsEditDto
    {
        public bool AllowOneConcurrentLoginPerUser { get; set; }

        public bool UseDefaultPasswordComplexitySettings { get; set; }

        public PasswordComplexitySetting PasswordComplexity { get; set; }

        public PasswordComplexitySetting DefaultPasswordComplexity { get; set; }

        public UserLockOutSettingsEditDto UserLockOut { get; set; }
    }
}