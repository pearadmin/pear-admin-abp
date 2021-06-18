using System.Collections.Generic;
using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using Abp.Zero.Configuration;
using Microsoft.Extensions.Configuration;

namespace PearAdmin.AbpTemplate.Configuration
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
            context.Manager.GetSettingDefinition(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled)
                .DefaultValue = false.ToString().ToLowerInvariant();

            ChangeEmailSettingScopes(context);

            return GetHostSettings()
                .Union(GetTenantSettings())
                .Union(GetSharedSettings())
                .Union(GetDefaultThemeSettings());
        }

        private void ChangeEmailSettingScopes(SettingDefinitionProviderContext context)
        {
            if (!AbpTemplateCoreConsts.AllowTenantsToChangeEmailSettings)
            {
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Host).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Port).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.UserName).Scopes =
                    SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Password).Scopes =
                    SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Domain).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.EnableSsl).Scopes =
                    SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials).Scopes =
                    SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.DefaultFromAddress).Scopes =
                    SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.DefaultFromDisplayName).Scopes =
                    SettingScopes.Application;
            }
        }

        private IEnumerable<SettingDefinition> GetHostSettings()
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.TenantManagement.AllowSelfRegistration,
                    GetFromAppSettingNames(AppSettingNames.TenantManagement.AllowSelfRegistration, "true"),
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.TenantManagement.IsNewRegisteredTenantActiveByDefault,
                    GetFromAppSettingNames(AppSettingNames.TenantManagement.IsNewRegisteredTenantActiveByDefault, "false")),
                new SettingDefinition(AppSettingNames.TenantManagement.UseCaptchaOnRegistration,
                    GetFromAppSettingNames(AppSettingNames.TenantManagement.UseCaptchaOnRegistration, "true"),
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.TenantManagement.DefaultEdition,
                    GetFromAppSettingNames(AppSettingNames.TenantManagement.DefaultEdition, "")),
                new SettingDefinition(AppSettingNames.UserManagement.SmsVerificationEnabled,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.SmsVerificationEnabled, "false"),
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.TenantManagement.SubscriptionExpireNotifyDayCount,
                    GetFromAppSettingNames(AppSettingNames.TenantManagement.SubscriptionExpireNotifyDayCount, "7"),
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.HostManagement.BillingLegalName,
                    GetFromAppSettingNames(AppSettingNames.HostManagement.BillingLegalName, "")),
                new SettingDefinition(AppSettingNames.HostManagement.BillingAddress,
                    GetFromAppSettingNames(AppSettingNames.HostManagement.BillingAddress, "")),
                new SettingDefinition(AppSettingNames.Recaptcha.SiteKey, GetFromSettings("Recaptcha:SiteKey"),
                    isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.UiManagement.Theme,
                    GetFromAppSettingNames(AppSettingNames.UiManagement.Theme, "default"), isVisibleToClients: true,
                    scopes: SettingScopes.All),
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
                new SettingDefinition(AppSettingNames.UserManagement.AllowSelfRegistration,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.AllowSelfRegistration, "true"),
                    scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.UserManagement.IsNewRegisteredUserActiveByDefault,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.IsNewRegisteredUserActiveByDefault, "false"),
                    scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.UserManagement.UseCaptchaOnRegistration,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.UseCaptchaOnRegistration, "true"),
                    scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.TenantManagement.BillingLegalName,
                    GetFromAppSettingNames(AppSettingNames.TenantManagement.BillingLegalName, ""),
                    scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.TenantManagement.BillingAddress,
                    GetFromAppSettingNames(AppSettingNames.TenantManagement.BillingAddress, ""), scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.TenantManagement.BillingTaxVatNo,
                    GetFromAppSettingNames(AppSettingNames.TenantManagement.BillingTaxVatNo, ""), scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.Email.UseHostDefaultEmailSettings,
                    GetFromAppSettingNames(AppSettingNames.Email.UseHostDefaultEmailSettings, AbpTemplateCoreConsts.MultiTenancyEnabled.ToString()), scopes: SettingScopes.Tenant)
            };
        }

        private IEnumerable<SettingDefinition> GetSharedSettings()
        {
            return new[]
            {
                new SettingDefinition(AppSettingNames.UserManagement.TwoFactorLogin.IsGoogleAuthenticatorEnabled,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.TwoFactorLogin.IsGoogleAuthenticatorEnabled, "false"),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.UserManagement.IsCookieConsentEnabled,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.IsCookieConsentEnabled, "false"),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.UserManagement.IsQuickThemeSelectEnabled,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.IsQuickThemeSelectEnabled, "false"),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.UserManagement.UseCaptchaOnLogin,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.UseCaptchaOnLogin, "false"),
                    scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(AppSettingNames.UserManagement.SessionTimeOut.IsEnabled,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.SessionTimeOut.IsEnabled, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.UserManagement.SessionTimeOut.TimeOutSecond,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.SessionTimeOut.TimeOutSecond, "30"),
                    isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.UserManagement.SessionTimeOut.ShowTimeOutNotificationSecond,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.SessionTimeOut.ShowTimeOutNotificationSecond, "30"),
                    isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.UserManagement.SessionTimeOut.ShowLockScreenWhenTimedOut,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.SessionTimeOut.ShowLockScreenWhenTimedOut, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.UserManagement.AllowOneConcurrentLoginPerUser,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.AllowOneConcurrentLoginPerUser, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.UserManagement.AllowUsingGravatarProfilePicture,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.AllowUsingGravatarProfilePicture, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant),
                new SettingDefinition(AppSettingNames.UserManagement.UseGravatarProfilePicture,
                    GetFromAppSettingNames(AppSettingNames.UserManagement.UseGravatarProfilePicture, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.User),
                new SettingDefinition(LocalizationSettingNames.DefaultLanguage,
                    "zh-Hans",
                    isVisibleToClients: true, scopes: SettingScopes.All)
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

        private IEnumerable<SettingDefinition> GetDefaultThemeSettings()
        {
            var themeName = "default";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettingNames.UiManagement.Header.DesktopFixedHeader,
                    GetFromAppSettingNames(themeName + "." + AppSettingNames.UiManagement.Header.DesktopFixedHeader, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettingNames.UiManagement.Header.MobileFixedHeader,
                    GetFromAppSettingNames(themeName + "." + AppSettingNames.UiManagement.Header.MobileFixedHeader, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettingNames.UiManagement.Header.Skin,
                    GetFromAppSettingNames(themeName + "." + AppSettingNames.UiManagement.Header.Skin, "light"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettingNames.UiManagement.SubHeader.Fixed,
                    GetFromAppSettingNames(themeName + "." + AppSettingNames.UiManagement.SubHeader.Fixed, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettingNames.UiManagement.SubHeader.Style,
                    GetFromAppSettingNames(themeName + "." + AppSettingNames.UiManagement.SubHeader.Style, "solid"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettingNames.UiManagement.LeftAside.AsideSkin,
                    GetFromAppSettingNames(themeName + "." + AppSettingNames.UiManagement.LeftAside.AsideSkin, "light"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettingNames.UiManagement.LeftAside.FixedAside,
                    GetFromAppSettingNames(themeName + "." + AppSettingNames.UiManagement.LeftAside.FixedAside, "true"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettingNames.UiManagement.LeftAside.AllowAsideMinimizing,
                    GetFromAppSettingNames(themeName + "." + AppSettingNames.UiManagement.LeftAside.AllowAsideMinimizing,
                        "true"), isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettingNames.UiManagement.LeftAside.DefaultMinimizedAside,
                    GetFromAppSettingNames(themeName + "." + AppSettingNames.UiManagement.LeftAside.DefaultMinimizedAside,
                        "false"), isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettingNames.UiManagement.LeftAside.SubmenuToggle,
                    GetFromAppSettingNames(themeName + "." + AppSettingNames.UiManagement.LeftAside.SubmenuToggle, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettingNames.UiManagement.Footer.FixedFooter,
                    GetFromAppSettingNames(themeName + "." + AppSettingNames.UiManagement.Footer.FixedFooter, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettingNames.UiManagement.SearchActive,
                    GetFromAppSettingNames(themeName + "." + AppSettingNames.UiManagement.SearchActive, "false"),
                    isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }
    }
}