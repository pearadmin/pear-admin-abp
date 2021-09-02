namespace PearAdmin.AbpTemplate.Configuration
{
    /// <summary>
    /// 基础配置定义
    /// </summary>
    public static class AppSettingNames
    {
        public static class HostManagement
        {
            public const string CompanyName = "App.HostManagement.CompanyName";
            public const string CompanyAddress = "App.HostManagement.CompanyAddress";
        }

        public static class TenantManagement
        {
            public const string DefaultEdition = "App.TenantManagement.DefaultEdition";
            public const string CompanyName = "App.TenantManagement.CompanyName";
            public const string CompanyAddress = "App.TenantManagement.CompanyAddress";
            public const string InviteMailboxTemplate = "App.TenantManagement.InviteMailboxTemplate";
        }

        public static class Email
        {
            public const string UseHostDefaultEmailSettings = "App.Email.UseHostDefaultEmailSettings";
        }
    }
}
