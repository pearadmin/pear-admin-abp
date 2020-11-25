using System.Threading.Tasks;
using Abp.Application.Services;
using PearAdmin.AbpTemplate.Authorization.Accounts.Dto;

namespace PearAdmin.AbpTemplate.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
