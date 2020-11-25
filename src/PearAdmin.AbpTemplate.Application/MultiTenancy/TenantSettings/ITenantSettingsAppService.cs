using System.Threading.Tasks;
using Abp.Application.Services;
using PearAdmin.AbpTemplate.MultiTenancy.TenantSetting.Dto;

namespace PearAdmin.AbpTemplate.MultiTenancy.TenantSetting
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);
    }
}
