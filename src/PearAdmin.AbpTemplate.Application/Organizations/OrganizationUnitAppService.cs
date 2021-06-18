using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Organizations;
using Microsoft.EntityFrameworkCore;
using PearAdmin.AbpTemplate.Authorization;
using PearAdmin.AbpTemplate.Organizations.Dto;

namespace PearAdmin.AbpTemplate.Organizations
{
    public class OrganizationUnitAppService : AbpTemplateApplicationServiceBase, IOrganizationUnitAppService
    {
        private readonly OrganizationUnitManager _organizationUnitManager;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<UserOrganizationUnit, long> _userOrganizationUnitRepository;

        public OrganizationUnitAppService(
            OrganizationUnitManager organizationUnitManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository)
        {
            _organizationUnitManager = organizationUnitManager;
            _organizationUnitRepository = organizationUnitRepository;
            _userOrganizationUnitRepository = userOrganizationUnitRepository;
        }

        public async Task<ListResultDto<OrganizationUnitDto>> GetAllOrganizationUnitTree()
        {
            var organizationUnits = await _organizationUnitRepository.GetAllListAsync();
            return new ListResultDto<OrganizationUnitDto>(ObjectMapper.Map<List<OrganizationUnitDto>>(organizationUnits));
        }

        public async Task<PagedResultDto<OrganizationUnitDto>> GetPagedOrganizationUnit(GetPagedOrganizationUnitInput input)
        {
            var query = _organizationUnitRepository.GetAll()
                .WhereIf(!input.DisplayName.IsNullOrWhiteSpace(), o => o.DisplayName.Contains(input.DisplayName));

            if (input.Id.HasValue)
            {
                var parentOrganizationUnit = await _organizationUnitRepository.FirstOrDefaultAsync(o => o.Id == input.Id.Value);
                query = query.WhereIf(parentOrganizationUnit != null, o => o.Code.StartsWith(parentOrganizationUnit.Code));
            }

            var organizationUnitMemberCounts = await _userOrganizationUnitRepository.GetAll()
                .GroupBy(x => x.OrganizationUnitId)
                .Select(groupedUsers => new
                {
                    organizationUnitId = groupedUsers.Key,
                    count = groupedUsers.Count()
                })
                .ToDictionaryAsync(x => x.organizationUnitId, y => y.count);

            var totalCount = await query.CountAsync();
            var items = await query.PageBy(input).ToListAsync();

            return new PagedResultDto<OrganizationUnitDto>(
                totalCount,
                items.Select(item =>
                {
                    var dto = ObjectMapper.Map<OrganizationUnitDto>(item);
                    dto.MemberCount = organizationUnitMemberCounts.ContainsKey(item.Id) ? organizationUnitMemberCounts[item.Id] : 0;
                    return dto;
                }).ToList());
        }

        public async Task<OrganizationUnitDto> GetOrganizationUnitForEdit(NullableIdDto<long> input)
        {
            if (input.Id.HasValue && input.Id.Value > 0)
            {
                var organizationUnits = await _organizationUnitRepository.GetAsync(input.Id.Value);
                var organizationUnitDto = ObjectMapper.Map<OrganizationUnitDto>(organizationUnits);
                return organizationUnitDto;
            }
            else
            {
                return new OrganizationUnitDto();
            }
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_OrganizationUnits_Create)]
        public async Task CreateOrganizationUnit(CreateOrganizationUnitDto input)
        {
            var organizationUnit = new OrganizationUnit(AbpSession.TenantId, input.DisplayName, input.ParentId);
            await _organizationUnitManager.CreateAsync(organizationUnit);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_OrganizationUnits_Update)]
        public async Task UpdateOrganizationUnit(UpdateOrganizationUnitDto input)
        {
            var organizationUnit = await _organizationUnitRepository.GetAsync(input.Id);

            organizationUnit.DisplayName = input.DisplayName;

            await _organizationUnitManager.UpdateAsync(organizationUnit);
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_OrganizationUnits_Update)]
        public async Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input)
        {
            await _organizationUnitManager.MoveAsync(input.Id, input.NewParentId);
            var organizationUnit = await _organizationUnitRepository.GetAsync(input.Id);

            var dto = ObjectMapper.Map<OrganizationUnitDto>(organizationUnit);
            dto.MemberCount = await _userOrganizationUnitRepository.CountAsync(uou => uou.OrganizationUnitId == organizationUnit.Id);

            return dto;
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_OrganizationUnits_Delete)]
        public async Task DeleteOrganizationUnit(List<EntityDto<long>> inputs)
        {
            foreach (var input in inputs)
            {
                await _userOrganizationUnitRepository.DeleteAsync(x => x.OrganizationUnitId == input.Id);
                await _organizationUnitManager.DeleteAsync(input.Id);
            }
        }
    }
}
