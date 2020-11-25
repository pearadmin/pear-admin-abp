using System.Threading.Tasks;
using Abp.Application.Services;
using PearAdmin.Abp.MultiTenancy.TenantSetting.Dto;

namespace PearAdmin.Abp.MultiTenancy.TenantSetting
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);
    }
}
