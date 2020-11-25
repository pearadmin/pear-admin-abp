using System.Globalization;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Net.Mail;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Abp.Timing;
using Abp.Zero.Configuration;
using PearAdmin.AbpTemplate.Authorization;
using PearAdmin.AbpTemplate.Configuration;
using PearAdmin.AbpTemplate.MultiTenancy.TenantSetting.Dto;
using PearAdmin.AbpTemplate.Security;
using PearAdmin.AbpTemplate.Timing;

namespace PearAdmin.AbpTemplate.MultiTenancy.TenantSetting
{
    [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_TenantSettings)]
    public class TenantSettingsAppService : AbpTemplateApplicationServiceBase, ITenantSettingsAppService
    {
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ITimeZoneService _timeZoneService;

        public TenantSettingsAppService(
            IMultiTenancyConfig multiTenancyConfig,
            ITimeZoneService timeZoneService)
        {
            _multiTenancyConfig = multiTenancyConfig;
            _timeZoneService = timeZoneService;
        }

        #region Get Settings
        public async Task<TenantSettingsEditDto> GetAllSettings()
        {
            var settings = new TenantSettingsEditDto
            {
                //UserManagement = await GetUserManagementSettingsAsync(),
                //Security = await GetSecuritySettingsAsync(),
                //OtherSettings = await GetOtherSettingsAsync(),
                //Email = await GetEmailSettingsAsync(),
                CompanySettings = await GetCompanySettingsAsync()
            };

            //if (!_multiTenancyConfig.IsEnabled || Clock.SupportsMultipleTimezone)
            //{
            //    settings.General = await GetGeneralSettingsAsync();
            //}

            return settings;
        }

        private async Task<TenantEmailSettingsEditDto> GetEmailSettingsAsync()
        {
            var useHostDefaultEmailSettings = await SettingManager.GetSettingValueForTenantAsync<bool>(AppSettingNames.Email.UseHostDefaultEmailSettings, AbpSession.GetTenantId());

            if (useHostDefaultEmailSettings)
            {
                return new TenantEmailSettingsEditDto
                {
                    UseHostDefaultEmailSettings = true
                };
            }

            var smtpPassword = await SettingManager.GetSettingValueForTenantAsync(EmailSettingNames.Smtp.Password, AbpSession.GetTenantId());

            return new TenantEmailSettingsEditDto
            {
                UseHostDefaultEmailSettings = false,
                DefaultFromAddress = await SettingManager.GetSettingValueForTenantAsync(EmailSettingNames.DefaultFromAddress, AbpSession.GetTenantId()),
                DefaultFromDisplayName = await SettingManager.GetSettingValueForTenantAsync(EmailSettingNames.DefaultFromDisplayName, AbpSession.GetTenantId()),
                SmtpHost = await SettingManager.GetSettingValueForTenantAsync(EmailSettingNames.Smtp.Host, AbpSession.GetTenantId()),
                SmtpPort = await SettingManager.GetSettingValueForTenantAsync<int>(EmailSettingNames.Smtp.Port, AbpSession.GetTenantId()),
                SmtpUserName = await SettingManager.GetSettingValueForTenantAsync(EmailSettingNames.Smtp.UserName, AbpSession.GetTenantId()),
                SmtpPassword = SimpleStringCipher.Instance.Decrypt(smtpPassword),
                SmtpDomain = await SettingManager.GetSettingValueForTenantAsync(EmailSettingNames.Smtp.Domain, AbpSession.GetTenantId()),
                SmtpEnableSsl = await SettingManager.GetSettingValueForTenantAsync<bool>(EmailSettingNames.Smtp.EnableSsl, AbpSession.GetTenantId()),
                SmtpUseDefaultCredentials = await SettingManager.GetSettingValueForTenantAsync<bool>(EmailSettingNames.Smtp.UseDefaultCredentials, AbpSession.GetTenantId())
            };
        }

        private async Task<CompanySettingsEditDto> GetCompanySettingsAsync()
        {
            return new CompanySettingsEditDto()
            {
                CompanyName = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.HostManagement.CompanyName),
                CompanyAddress = await SettingManager.GetSettingValueForApplicationAsync(AppSettingNames.HostManagement.CompanyAddress)
            };
        }

        private async Task<GeneralSettingsEditDto> GetGeneralSettingsAsync()
        {
            var settings = new GeneralSettingsEditDto();

            if (Clock.SupportsMultipleTimezone)
            {
                var timezone = await SettingManager.GetSettingValueForTenantAsync(TimingSettingNames.TimeZone, AbpSession.GetTenantId());

                settings.Timezone = timezone;
                settings.TimezoneForComparison = timezone;
            }

            var defaultTimeZoneId = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Tenant, AbpSession.TenantId);

            if (settings.Timezone == defaultTimeZoneId)
            {
                settings.Timezone = string.Empty;
            }

            return settings;
        }

        private async Task<TenantUserManagementSettingsEditDto> GetUserManagementSettingsAsync()
        {
            return new TenantUserManagementSettingsEditDto
            {
                AllowSelfRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.AllowSelfRegistration),
                IsNewRegisteredUserActiveByDefault = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.IsNewRegisteredUserActiveByDefault),
                IsEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin),
                UseCaptchaOnRegistration = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.UseCaptchaOnRegistration),
                UseCaptchaOnLogin = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.UseCaptchaOnLogin),
                IsCookieConsentEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.IsCookieConsentEnabled),
                IsQuickThemeSelectEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.IsQuickThemeSelectEnabled),
                AllowUsingGravatarProfilePicture = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.AllowUsingGravatarProfilePicture),
                SessionTimeOutSettings = new SessionTimeOutSettingsEditDto()
                {
                    IsEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.SessionTimeOut.IsEnabled),
                    TimeOutSecond = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.UserManagement.SessionTimeOut.TimeOutSecond),
                    ShowTimeOutNotificationSecond = await SettingManager.GetSettingValueAsync<int>(AppSettingNames.UserManagement.SessionTimeOut.ShowTimeOutNotificationSecond),
                    ShowLockScreenWhenTimedOut = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.SessionTimeOut.ShowLockScreenWhenTimedOut)
                }
            };
        }

        private async Task<SecuritySettingsEditDto> GetSecuritySettingsAsync()
        {
            var passwordComplexitySetting = new PasswordComplexitySetting
            {
                RequireDigit = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireDigit),
                RequireLowercase = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireLowercase),
                RequireNonAlphanumeric = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric),
                RequireUppercase = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireUppercase),
                RequiredLength = await SettingManager.GetSettingValueAsync<int>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequiredLength)
            };

            var defaultPasswordComplexitySetting = new PasswordComplexitySetting
            {
                RequireDigit = await SettingManager.GetSettingValueForApplicationAsync<bool>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireDigit),
                RequireLowercase = await SettingManager.GetSettingValueForApplicationAsync<bool>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireLowercase),
                RequireNonAlphanumeric = await SettingManager.GetSettingValueForApplicationAsync<bool>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric),
                RequireUppercase = await SettingManager.GetSettingValueForApplicationAsync<bool>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireUppercase),
                RequiredLength = await SettingManager.GetSettingValueForApplicationAsync<int>(AbpZeroSettingNames.UserManagement.PasswordComplexity.RequiredLength)
            };

            return new SecuritySettingsEditDto
            {
                UseDefaultPasswordComplexitySettings = passwordComplexitySetting.Equals(defaultPasswordComplexitySetting),
                PasswordComplexity = passwordComplexitySetting,
                DefaultPasswordComplexity = defaultPasswordComplexitySetting,
                UserLockOut = await GetUserLockOutSettingsAsync(),
                AllowOneConcurrentLoginPerUser = await GetOneConcurrentLoginPerUserSetting()
            };
        }

        private async Task<TenantOtherSettingsEditDto> GetOtherSettingsAsync()
        {
            return new TenantOtherSettingsEditDto()
            {
                IsQuickThemeSelectEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.IsQuickThemeSelectEnabled)
            };
        }

        private async Task<UserLockOutSettingsEditDto> GetUserLockOutSettingsAsync()
        {
            return new UserLockOutSettingsEditDto
            {
                IsEnabled = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.UserLockOut.IsEnabled),
                MaxFailedAccessAttemptsBeforeLockout = await SettingManager.GetSettingValueAsync<int>(AbpZeroSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout),
                DefaultAccountLockoutSeconds = await SettingManager.GetSettingValueAsync<int>(AbpZeroSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds)
            };
        }

        private async Task<bool> GetOneConcurrentLoginPerUserSetting()
        {
            return await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.AllowOneConcurrentLoginPerUser);
        }
        #endregion

        #region Update Settings
        public async Task UpdateAllSettings(TenantSettingsEditDto input)
        {
            //await UpdateUserManagementSettingsAsync(input.UserManagement);
            //await UpdateSecuritySettingsAsync(input.Security);
            //await UpdateEmailSettingsAsync(input.Email);
            await UpdateCompanySettingsAsync(input.CompanySettings);

            //Time Zone
            //if (Clock.SupportsMultipleTimezone)
            //{
            //    if (input.General.Timezone.IsNullOrEmpty())
            //    {
            //        var defaultValue = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Tenant, AbpSession.TenantId);
            //        await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), TimingSettingNames.TimeZone, defaultValue);
            //    }
            //    else
            //    {
            //        await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), TimingSettingNames.TimeZone, input.General.Timezone);
            //    }
            //}

            //if (!_multiTenancyConfig.IsEnabled)
            //{
            //    await UpdateOtherSettingsAsync(input.OtherSettings);

            //    input.ValidateHostSettings();
            //}
        }

        private async Task UpdateOtherSettingsAsync(TenantOtherSettingsEditDto input)
        {
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettingNames.UserManagement.IsQuickThemeSelectEnabled,
                input.IsQuickThemeSelectEnabled.ToString().ToLowerInvariant()
            );
        }

        private async Task UpdateCompanySettingsAsync(CompanySettingsEditDto input)
        {
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.HostManagement.CompanyName, input.CompanyName);
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.HostManagement.CompanyAddress, input.CompanyAddress);
        }

        private async Task UpdateEmailSettingsAsync(TenantEmailSettingsEditDto input)
        {
            if (_multiTenancyConfig.IsEnabled && !AbpTemplateCoreConsts.AllowTenantsToChangeEmailSettings)
            {
                return;
            }

            var useHostDefaultEmailSettings = _multiTenancyConfig.IsEnabled && input.UseHostDefaultEmailSettings;

            if (useHostDefaultEmailSettings)
            {
                var smtpPassword = await SettingManager.GetSettingValueForApplicationAsync(EmailSettingNames.Smtp.Password);

                input = new TenantEmailSettingsEditDto
                {
                    UseHostDefaultEmailSettings = true,
                    DefaultFromAddress = await SettingManager.GetSettingValueForApplicationAsync(EmailSettingNames.DefaultFromAddress),
                    DefaultFromDisplayName = await SettingManager.GetSettingValueForApplicationAsync(EmailSettingNames.DefaultFromDisplayName),
                    SmtpHost = await SettingManager.GetSettingValueForApplicationAsync(EmailSettingNames.Smtp.Host),
                    SmtpPort = await SettingManager.GetSettingValueForApplicationAsync<int>(EmailSettingNames.Smtp.Port),
                    SmtpUserName = await SettingManager.GetSettingValueForApplicationAsync(EmailSettingNames.Smtp.UserName),
                    SmtpPassword = SimpleStringCipher.Instance.Decrypt(smtpPassword),
                    SmtpDomain = await SettingManager.GetSettingValueForApplicationAsync(EmailSettingNames.Smtp.Domain),
                    SmtpEnableSsl = await SettingManager.GetSettingValueForApplicationAsync<bool>(EmailSettingNames.Smtp.EnableSsl),
                    SmtpUseDefaultCredentials = await SettingManager.GetSettingValueForApplicationAsync<bool>(EmailSettingNames.Smtp.UseDefaultCredentials)
                };
            }

            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AppSettingNames.Email.UseHostDefaultEmailSettings, useHostDefaultEmailSettings.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EmailSettingNames.DefaultFromAddress, input.DefaultFromAddress);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EmailSettingNames.DefaultFromDisplayName, input.DefaultFromDisplayName);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EmailSettingNames.Smtp.Host, input.SmtpHost);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EmailSettingNames.Smtp.Port, input.SmtpPort.ToString(CultureInfo.InvariantCulture));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EmailSettingNames.Smtp.UserName, input.SmtpUserName);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EmailSettingNames.Smtp.Password, SimpleStringCipher.Instance.Encrypt(input.SmtpPassword));
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EmailSettingNames.Smtp.Domain, input.SmtpDomain);
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EmailSettingNames.Smtp.EnableSsl, input.SmtpEnableSsl.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), EmailSettingNames.Smtp.UseDefaultCredentials, input.SmtpUseDefaultCredentials.ToString().ToLowerInvariant());
        }

        private async Task UpdateUserManagementSettingsAsync(TenantUserManagementSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettingNames.UserManagement.AllowSelfRegistration,
                settings.AllowSelfRegistration.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettingNames.UserManagement.IsNewRegisteredUserActiveByDefault,
                settings.IsNewRegisteredUserActiveByDefault.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin,
                settings.IsEmailConfirmationRequiredForLogin.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettingNames.UserManagement.UseCaptchaOnRegistration,
                settings.UseCaptchaOnRegistration.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettingNames.UserManagement.UseCaptchaOnLogin,
                settings.UseCaptchaOnLogin.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettingNames.UserManagement.IsCookieConsentEnabled,
                settings.IsCookieConsentEnabled.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettingNames.UserManagement.AllowUsingGravatarProfilePicture,
                settings.AllowUsingGravatarProfilePicture.ToString().ToLowerInvariant()
            );

            await UpdateUserManagementSessionTimeOutSettingsAsync(settings.SessionTimeOutSettings);
        }

        private async Task UpdateUserManagementSessionTimeOutSettingsAsync(SessionTimeOutSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettingNames.UserManagement.SessionTimeOut.IsEnabled,
                settings.IsEnabled.ToString().ToLowerInvariant()
            );
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettingNames.UserManagement.SessionTimeOut.TimeOutSecond,
                settings.TimeOutSecond.ToString()
            );
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettingNames.UserManagement.SessionTimeOut.ShowTimeOutNotificationSecond,
                settings.ShowTimeOutNotificationSecond.ToString()
            );
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AppSettingNames.UserManagement.SessionTimeOut.ShowLockScreenWhenTimedOut,
                settings.ShowLockScreenWhenTimedOut.ToString()
            );
        }

        private async Task UpdateSecuritySettingsAsync(SecuritySettingsEditDto settings)
        {
            if (settings.UseDefaultPasswordComplexitySettings)
            {
                await UpdatePasswordComplexitySettingsAsync(settings.DefaultPasswordComplexity);
            }
            else
            {
                await UpdatePasswordComplexitySettingsAsync(settings.PasswordComplexity);
            }

            await UpdateUserLockOutSettingsAsync(settings.UserLockOut);
            await UpdateOneConcurrentLoginPerUserSettingAsync(settings.AllowOneConcurrentLoginPerUser);
        }

        private async Task UpdatePasswordComplexitySettingsAsync(PasswordComplexitySetting settings)
        {
            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireDigit,
                settings.RequireDigit.ToString()
            );

            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireLowercase,
                settings.RequireLowercase.ToString()
            );

            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric,
                settings.RequireNonAlphanumeric.ToString()
            );

            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireUppercase,
                settings.RequireUppercase.ToString()
            );

            await SettingManager.ChangeSettingForTenantAsync(
                AbpSession.GetTenantId(),
                AbpZeroSettingNames.UserManagement.PasswordComplexity.RequiredLength,
                settings.RequiredLength.ToString()
            );
        }

        private async Task UpdateUserLockOutSettingsAsync(UserLockOutSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.UserLockOut.IsEnabled, settings.IsEnabled.ToString().ToLowerInvariant());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.UserLockOut.DefaultAccountLockoutSeconds, settings.DefaultAccountLockoutSeconds.ToString());
            await SettingManager.ChangeSettingForTenantAsync(AbpSession.GetTenantId(), AbpZeroSettingNames.UserManagement.UserLockOut.MaxFailedAccessAttemptsBeforeLockout, settings.MaxFailedAccessAttemptsBeforeLockout.ToString());
        }

        private async Task UpdateOneConcurrentLoginPerUserSettingAsync(bool allowOneConcurrentLoginPerUser)
        {
            if (_multiTenancyConfig.IsEnabled)
            {
                return;
            }
            await SettingManager.ChangeSettingForApplicationAsync(AppSettingNames.UserManagement.AllowOneConcurrentLoginPerUser, allowOneConcurrentLoginPerUser.ToString());
        }
        #endregion
    }
}

