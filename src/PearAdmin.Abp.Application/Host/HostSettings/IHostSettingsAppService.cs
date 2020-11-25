using System.Threading.Tasks;
using Abp.Application.Services;
using PearAdmin.Abp.Host.HostSettings.Dto;

namespace PearAdmin.Abp.Host.HostSettings
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);
    }
}
