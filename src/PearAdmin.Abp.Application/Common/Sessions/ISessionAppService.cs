using System.Threading.Tasks;
using Abp.Application.Services;
using PearAdmin.Abp.Sessions.Dto;

namespace PearAdmin.Abp.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
