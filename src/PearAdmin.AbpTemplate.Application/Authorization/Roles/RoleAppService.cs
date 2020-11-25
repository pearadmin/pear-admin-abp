using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using PearAdmin.AbpTemplate.Authorization.Users;
using PearAdmin.AbpTemplate.Authorization.Roles.Dto;
using Microsoft.EntityFrameworkCore;
using Abp.Extensions;

namespace PearAdmin.AbpTemplate.Authorization.Roles
{
    public class RoleAppService : AbpTemplateApplicationServiceBase, IRoleAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;

        public RoleAppService(IRepository<Role> roleRepository, RoleManager roleManager)
        {
            _roleManager = roleManager;
            _roleRepository = roleRepository;
        }

        public async Task<ListResultDto<RoleDto>> GetAllRole()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        public async Task<PagedResultDto<RoleDto>> GetPagedRole(GetPagedRoleInput input)
        {
            var query = _roleRepository.GetAll()
                .WhereIf(!input.Name.IsNullOrWhiteSpace(), r => r.Name.Contains(input.Name));

            var totalCount = await query.CountAsync();
            var items = await query.PageBy(input).ToListAsync();

            return new PagedResultDto<RoleDto>(
                totalCount,
                items.Select(item =>
                {
                    var dto = ObjectMapper.Map<RoleDto>(item);
                    return dto;
                }).ToList());
        }

        public async Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input)
        {
            var output = new GetRoleForEditOutput()
            {
                Role = new RoleDto(),
                Permissions = new List<RolePermissionDto>()
            };

            var permissions = PermissionManager.GetAllPermissions();
            output.Permissions = permissions
                .Select(p => new RolePermissionDto()
                {
                    Id = p.Name,
                    ParentId = p.Parent == null ? string.Empty : p.Parent.Name,
                    Name = p.Name,
                    //Description = p.Description.ToString(),
                    DisplayName = p.Name,
                })
                .OrderBy(p => p.DisplayName)
                .ToList();

            if (input.Id.HasValue)
            {
                var role = await _roleManager.GetRoleByIdAsync(input.Id.Value);
                output.Role = ObjectMapper.Map<RoleDto>(role);

                var grantedPermissions = (await _roleManager.GetGrantedPermissionsAsync(role)).ToArray();
                foreach (var permission in output.Permissions)
                {
                    permission.IsAssigned = grantedPermissions.Select(g => g.Name).Contains(permission.Name);
                }
            }

            return output;
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Roles_Create)]
        public async Task CreateRole(CreateRoleDto input)
        {
            var role = Role.CreateRole(AbpSession.TenantId, input.Name)
                .SetDescription(input.Description)
                .SetIsDefault(input.IsDefault);

            CheckErrors(await _roleManager.CreateAsync(role));

            var grantedPermissions = PermissionManager
                .GetAllPermissions()
                .Where(p => input.GrantedPermissionNames.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Roles_Update)]
        public async Task UpdateRole(UpdateRoleDto input)
        {
            var role = await _roleManager.GetRoleByIdAsync(input.Id);

            role.SetName(input.Name)
                .SetDescription(input.Description)
                .SetIsDefault(input.IsDefault);

            CheckErrors(await _roleManager.UpdateAsync(role));

            var grantedPermissions = PermissionManager.GetAllPermissions()
                .Where(p => input.GrantedPermissionNames.Contains(p.Name))
                .ToList();

            await _roleManager.SetGrantedPermissionsAsync(role, grantedPermissions);
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Roles_Delete)]
        public async Task DeleteRole(List<EntityDto<int>> inputs)
        {
            foreach (var input in inputs)
            {
                var role = await _roleManager.FindByIdAsync(input.Id.ToString());
                var users = await UserManager.GetUsersInRoleAsync(role.Name);

                foreach (var user in users)
                {
                    CheckErrors(await UserManager.RemoveFromRoleAsync(user, role.Name));
                }

                CheckErrors(await _roleManager.DeleteAsync(role));
            }
        }
    }
}

