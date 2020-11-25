using System.Threading.Tasks;
using Abp.Application.Services;
using PearAdmin.AbpTemplate.Host.HostSettings.Dto;

namespace PearAdmin.AbpTemplate.Host.HostSettings
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);
    }
}
