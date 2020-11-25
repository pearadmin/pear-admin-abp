using System.Threading.Tasks;
using Abp.Application.Services;
using PearAdmin.Abp.Authorization.Accounts.Dto;

namespace PearAdmin.Abp.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
