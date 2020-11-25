using System.Threading.Tasks;
using Abp.Application.Services;
using PearAdmin.AbpTemplate.Sessions.Dto;

namespace PearAdmin.AbpTemplate.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
