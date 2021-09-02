using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Configuration;
using Abp.Extensions;
using Abp.Timing;
using PearAdmin.AbpTemplate.Authorization;
using PearAdmin.AbpTemplate.Configuration;
using PearAdmin.AbpTemplate.Host.HostSettings.Dto;
using PearAdmin.AbpTemplate.Timing;

namespace PearAdmin.AbpTemplate.Host.HostSettings
{
    [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_HostSettings)]
    public class HostSettingsAppService : AbpTemplateApplicationServiceBase, IHostSettingsAppService
    {
        private readonly ITimeZoneService _timeZoneService;

        public HostSettingsAppService(
            ITimeZoneService timeZoneService)
        {
            _timeZoneService = timeZoneService;
        }

        #region Get Settings

        public async Task<HostSettingsEditDto> GetAllSettings()
        {
            return new HostSettingsEditDto
            {
                General = await GetGeneralSettingsAsync(),
                HostManagement = await GetHostManagementSettingsAsync()
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

            var defaultTimeZoneId = await _timeZoneService.GetDefaultTimezoneAsync(SettingScopes.Application, AbpSession.TenantId);
            if (settings.Timezone == defaultTimeZoneId)
            {
                settings.Timezone = string.Empty;
            }

            return settings;
        }

        private async Task<HostManagementSettingsEditDto> GetHostManagementSettingsAsync()
        {
            var settings = new HostManagementSettingsEditDto
            {
                CompanyName = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.HostManagement.CompanyAddress),
                CompanyAddress = await SettingManager.GetSettingValueAsync<bool>(AppSettingNames.HostManagement.CompanyName),
            };

            return settings;
        }

        #endregion

        #region Update Settings

        public async Task UpdateAllSettings(HostSettingsEditDto input)
        {
            await UpdateGeneralSettingsAsync(input.General);
            await UpdateHostManagementAsync(input.HostManagement);
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

        private async Task UpdateHostManagementAsync(HostManagementSettingsEditDto settings)
        {
            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.HostManagement.CompanyName,
                settings.CompanyName.ToString().ToLowerInvariant()
            );
            await SettingManager.ChangeSettingForApplicationAsync(
                AppSettingNames.HostManagement.CompanyAddress,
                settings.CompanyAddress.ToString().ToLowerInvariant()
            );
        }
        #endregion
    }
}

