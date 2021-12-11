using System.Collections.Generic;
using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using Microsoft.Extensions.Configuration;
using PearAdmin.AbpTemplate.AppProvider.Settings.MailboxTemplates;
using PearAdmin.AbpTemplate.Configuration;

namespace PearAdmin.AbpTemplate.Settings
{
    public class AppSettingProvider : SettingProvider
    {
        private readonly IConfigurationRoot _appConfiguration;

        public AppSettingProvider(IAppConfigurationAccessor configurationAccessor)
        {
            _appConfiguration = configurationAccessor.Configuration;
        }

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            ChangeEmailSettingScopes(context);

            return GetHostSettings()
                .Union(GetTenantSettings())
                .Union(GetSharedSettings());
        }

        private void ChangeEmailSettingScopes(SettingDefinitionProviderContext context)
        {
            if (AbpTemplateCoreConsts.AllowTenantsToChangeEmailSettings)
            {
                return;
            }

            context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Host).Scopes = SettingScopes.Application;
            context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Port).Scopes = SettingScopes.Application;
            context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.UserName).Scopes = SettingScopes.Application;
            context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Password).Scopes = SettingScopes.Application;
            context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Domain).Scopes = SettingScopes.Application;
            context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.EnableSsl).Scopes = SettingScopes.Application;
            context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials).Scopes = SettingScopes.Application;
            context.Manager.GetSettingDefinition(EmailSettingNames.DefaultFromAddress).Scopes = SettingScopes.Application;
            context.Manager.GetSettingDefinition(EmailSettingNames.DefaultFromDisplayName).Scopes = SettingScopes.Application;
        }

        private IEnumerable<SettingDefinition> GetHostSettings()
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.HostManagement.CompanyName,
                    GetFromAppSettingNames(AppSettingNames.HostManagement.CompanyName)),
                new SettingDefinition(AppSettingNames.HostManagement.CompanyAddress,
                    GetFromAppSettingNames(AppSettingNames.HostManagement.CompanyAddress)),
            };
        }

        private IEnumerable<SettingDefinition> GetTenantSettings()
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.TenantManagement.CompanyName,
                    GetFromAppSettingNames(AppSettingNames.TenantManagement.CompanyName, ""),
                    scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.TenantManagement.CompanyAddress,
                    GetFromAppSettingNames(AppSettingNames.TenantManagement.CompanyAddress, ""),
                    scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.TenantManagement.InviteMailboxTemplate,
                    GetFromAppSettingNames(AppSettingNames.TenantManagement.InviteMailboxTemplate,
                    InviteMailboxTemplate.DefaultTemplate),
                    scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.Email.UseHostDefaultEmailSettings,
                    GetFromAppSettingNames(AppSettingNames.Email.UseHostDefaultEmailSettings,
                    AbpTemplateCoreConsts.MultiTenancyEnabled.ToString()),
                    scopes: SettingScopes.Tenant)
            };
        }

        private IEnumerable<SettingDefinition> GetSharedSettings()
        {
            return new[]
            {
                new SettingDefinition(LocalizationSettingNames.DefaultLanguage,
                    "zh-Hans",
                    isVisibleToClients: true,
                    scopes: SettingScopes.All)
            };
        }

        private string GetFromAppSettingNames(string name, string defaultValue = null)
        {
            return GetFromSettings("App:" + name, defaultValue);
        }

        private string GetFromSettings(string name, string defaultValue = null)
        {
            return _appConfiguration[name] ?? defaultValue;
        }
    }
}