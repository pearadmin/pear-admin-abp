using System;
using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Configuration;
using Abp.Extensions;
using Abp.Timing;
using Abp.Zero.Configuration;
using PearAdmin.AbpTemplate.Authorization;
using PearAdmin.AbpTemplate.Configuration;
using PearAdmin.AbpTemplate.Editions;
using PearAdmin.AbpTemplate.Host.HostSettings.Dto;
using PearAdmin.AbpTemplate.Security;
using PearAdmin.AbpTemplate.Timing;

namespace PearAdmin.AbpTemplate.Host.HostSettings
{
    [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_HostSettings)]
    public class HostSettingsAppService : AbpTemplateApplicationServiceBase, IHostSettingsAppService
    {
        private readonly EditionManager _editionManager;
        private readonly ITimeZoneService _timeZoneService;
        private readonly ISettingDefinitionManager _settingDefinitionManager;

        public HostSettingsAppService(
            EditionManager editionManager,
            ITimeZoneService timeZoneService,
            ISettingDefinitionManager settingDefinitionManager)
        {
            _editionManager = editionManager;
            _timeZoneService = timeZoneService;
            _settingDefinitionManager = settingDefinitionManager;
        }

        #region Get Settings

        public async Task<HostSettingsEditDto> GetAllSettings()
        {
            return new HostSettingsEditDto
            {
                General = await GetGeneralSettingsAsync(),
                TenantManagement = await GetTenantManagementSettingsAsync(),
                UserManagement = await GetUserManagementAsync(),
                OtherSettings = await GetOtherSettingsAsync()
            };
        }

        private async Task<GeneralSettingsEditDto> GetGeneralSettingsAsync()
        {
            var timezone = await SettingManager.GetSettingValueForApplicationAsync(TimingSettingNames.TimeZone);
            var settings = new GeneralSettingsEditDto
            {
                Timezone = timezone,
                TimezoneForComparison = timezone
            };

            var defaultTimeZoneId =
                await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Application, AbpSession.TenantId);
            if (settings.Timezone == defaultTimeZoneId)
            {
                settings.Timezone = string.Empty;
            }

            return settings;
        }

        private async Task<TenantManagementSettingsEditDto> GetTenantManagementSettingsAsync()
        {
            var settings = new TenantManagementSettingsEditDto
            {
                AllowSelfRegistration =
                    await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.TenantManagement.AllowSelfRegistration),
                IsNewRegisteredTenantActiveByDefault =
                    await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.TenantManagement
                        .IsNewRegisteredTenantActiveByDefault),
                UseCaptchaOnRegistration =
                    await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.TenantManagement
                        .UseCaptchaOnRegistration),
            };

            var defaultEditionId =
                await SettingManager.GetSettingValueAsync(AppSettingNames.TenantManagement.DefaultEdition);
            if (!string.IsNullOrEmpty(defaultEditionId) &&
                (await _editionManager.FindByIdAsync(Convert.ToInt32(defaultEditionId)) != null))
            {
                settings.DefaultEditionId = Convert.ToInt32(defaultEditionId);
            }

            return settings;
        }

        private async Task<HostUserManagementSettingsEditDto> GetUserManagementAsync()
        {
            return new HostUserManagementSettingsEditDto
            {
                IsEmailConfirmationRequiredForLogin =
                    await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement
                        .IsEmailConfirmationRequiredForLogin),
                SmsVerificationEnabled =
                    await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.SmsVerificationEnabled),
                IsCookieConsentEnabled =
                    await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.IsCookieConsentEnabled),
                IsQuickThemeSelectEnabled =
                    await SettingManager.GetSettingValueAsync<bool>(
                        AppSettingNames.UserManagement.IsQuickThemeSelectEnabled),
                UseCaptchaOnLogin =
                    await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.UseCaptchaOnLogin),
                AllowUsingGravatarProfilePicture =
                    await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement
                        .AllowUsingGravatarProfilePicture),
                SessionTimeOutSettings = new SessionTimeOutSettingsEditDto
                {
                    IsEnabled = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement
                        .SessionTimeOut.IsEnabled),
                    TimeOutSecond =
                        await SettingManager.GetSettingValueAsync<int>(AppSettingNames.UserManagement.SessionTimeOut
                            .TimeOutSecond),
                    ShowTimeOutNotificationSecond =
                        await SettingManager.GetSettingValueAsync<int>(AppSettingNames.UserManagement.SessionTimeOut
                            .ShowTimeOutNotificationSecond),
                    ShowLockScreenWhenTimedOut =
                        await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement.SessionTimeOut
                            .ShowLockScreenWhenTimedOut)
                }
            };
        }

        private async Task<HostOtherSettingsEditDto> GetOtherSettingsAsync()
        {
            return new HostOtherSettingsEditDto()
            {
                IsQuickThemeSelectEnabled =
                    await SettingManager.GetSettingValueAsync<bool>(
                        AppSettingNames.UserManagement.IsQuickThemeSelectEnabled)
            };
        }

        private async Task<bool> GetOneConcurrentLoginPerUserSetting()
        {
            return await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.UserManagement
                .AllowOneConcurrentLoginPerUser);
        }

        #endregion

        #region Update Settings

        public async Task UpdateAllSettings(HostSettingsEditDto input)
        {
            await UpdateGeneralSettingsAsync(input.General);
            await UpdateTenantManagementAsync(input.TenantManagement);
            await UpdateUserManagementSettingsAsync(input.UserManagement);
            await UpdateOtherSettingsAsync(input.OtherSettings);
        }

        private async Task UpdateOtherSettingsAsync(HostOtherSettingsEditDto input)
        {
            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.IsQuickThemeSelectEnabled,
                input.IsQuickThemeSelectEnabled.ToString().ToLowerInvariant()
            );
        }

        private async Task UpdateGeneralSettingsAsync(GeneralSettingsEditDto settings)
        {
            if (Clock.SupportsMultipleTimezone)
            {
                if (settings.Timezone.IsNullOrEmpty())
                {
                    var defaultValue =
                        await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Application, AbpSession.TenantId);
                    await SettingManager.ChangeSettingForApplicationAsync(TimingSettingNames.TimeZone, defaultValue);
                }
                else
                {
                    await SettingManager.ChangeSettingForApplicationAsync(TimingSettingNames.TimeZone,
                        settings.Timezone);
                }
            }
        }

        private async Task UpdateTenantManagementAsync(TenantManagementSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.TenantManagement.AllowSelfRegistration,
                settings.AllowSelfRegistration.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.TenantManagement.IsNewRegisteredTenantActiveByDefault,
                settings.IsNewRegisteredTenantActiveByDefault.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.TenantManagement.UseCaptchaOnRegistration,
                settings.UseCaptchaOnRegistration.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.TenantManagement.DefaultEdition,
                settings.DefaultEditionId?.ToString() ?? ""
            );
        }

        private async Task UpdateUserManagementSettingsAsync(HostUserManagementSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForApplicationAsync(
                AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin,
                settings.IsEmailConfirmationRequiredForLogin.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.SmsVerificationEnabled,
                settings.SmsVerificationEnabled.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.IsCookieConsentEnabled,
                settings.IsCookieConsentEnabled.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.UseCaptchaOnLogin,
                settings.UseCaptchaOnLogin.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.AllowUsingGravatarProfilePicture,
                settings.AllowUsingGravatarProfilePicture.ToString().ToLowerInvariant()
            );

            await UpdateUserManagementSessionTimeOutSettingsAsync(settings.SessionTimeOutSettings);
        }

        private async Task UpdateUserManagementSessionTimeOutSettingsAsync(SessionTimeOutSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.SessionTimeOut.IsEnabled,
                settings.IsEnabled.ToString().ToLowerInvariant()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.SessionTimeOut.TimeOutSecond,
                settings.TimeOutSecond.ToString()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.SessionTimeOut.ShowTimeOutNotificationSecond,
                settings.ShowTimeOutNotificationSecond.ToString()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.SessionTimeOut.ShowLockScreenWhenTimedOut,
                settings.ShowLockScreenWhenTimedOut.ToString()
            );
        }

        private async Task UpdatePasswordComplexitySettingsAsync(PasswordComplexitySetting settings)
        {
            await SettingManager.ChangeSettingForApplicationAsync(
                AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireDigit,
                settings.RequireDigit.ToString()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireLowercase,
                settings.RequireLowercase.ToString()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireNonAlphanumeric,
                settings.RequireNonAlphanumeric.ToString()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AbpZeroSettingNames.UserManagement.PasswordComplexity.RequireUppercase,
                settings.RequireUppercase.ToString()
            );

            await SettingManager.ChangeSettingForApplicationAsync(
                AbpZeroSettingNames.UserManagement.PasswordComplexity.RequiredLength,
                settings.RequiredLength.ToString()
            );
        }

        private async Task UpdateOneConcurrentLoginPerUserSettingAsync(bool allowOneConcurrentLoginPerUser)
        {
            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.UserManagement.AllowOneConcurrentLoginPerUser, allowOneConcurrentLoginPerUser.ToString());
        }
        #endregion
    }
}

