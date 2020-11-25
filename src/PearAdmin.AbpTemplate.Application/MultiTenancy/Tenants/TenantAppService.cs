using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using PearAdmin.AbpTemplate.Authorization;
using PearAdmin.AbpTemplate.MultiTenancy.Tenants.Dto;

namespace PearAdmin.AbpTemplate.MultiTenancy.Tenants
{
    public class TenantAppService : AbpTemplateApplicationServiceBase, ITenantAppService
    {
        private readonly IRepository<Tenant, int> _tenantRepository;

        public TenantAppService(IRepository<Tenant, int> tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }

        public async Task<ListResultDto<TenantDto>> GetAllTenant()
        {
            var tenants = await _tenantRepository.GetAll()
                .Include(t => t.Edition)
                .ToListAsync();

            return new ListResultDto<TenantDto>(
                tenants.Select(item =>
                {
                    var dto = ObjectMapper.Map<TenantDto>(item);
                    return dto;
                }).ToList());
        }

        public async Task<TenantDto> GetTenantForEdit(EntityDto<int> input)
        {
            var tenant = await TenantManager.GetByIdAsync(input.Id);
            return ObjectMapper.Map<TenantDto>(tenant);
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Tenants_ChangeTenantEdition)]
        public async Task ChangeTenantEdition(ChangeTenantEditionDto input)
        {
            if (input.SourceEditionId == input.TargetEditionId)
            {
                throw new UserFriendlyException("The version is consistent and there is no need to switch.");
            }

            var tenant = await TenantManager.GetByIdAsync(input.Id);
            tenant.EditionId = input.TargetEditionId;
            await TenantManager.UpdateAsync(tenant);
        }
    }
}

