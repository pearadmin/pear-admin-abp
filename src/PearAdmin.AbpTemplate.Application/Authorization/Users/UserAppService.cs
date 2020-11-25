using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using PearAdmin.AbpTemplate.Authorization.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PearAdmin.AbpTemplate.Authorization.Users.Dto;
using Abp.Organizations;
using Abp.Authorization.Users;
using System.Collections.ObjectModel;

namespace PearAdmin.AbpTemplate.Authorization.Users
{
    public class UserAppService : AbpTemplateApplicationServiceBase, IUserAppService
    {
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IAbpSession _abpSession;
        private readonly IRepository<OrganizationUnit, long> _organizationUnitRepository;
        private readonly IRepository<UserRole, long> _userRoleRepository;
        private readonly IEnumerable<IPasswordValidator<User>> _passwordValidators;

        public UserAppService(
            UserManager userManager,
            RoleManager roleManager,
            IRepository<Role> roleRepository,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            IAbpSession abpSession,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserRole, long> userRoleRepository)
        {
            UserManager = userManager;
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _passwordHasher = passwordHasher;
            _passwordValidators = passwordValidators;
            _abpSession = abpSession;
            _organizationUnitRepository = organizationUnitRepository;
            _userRoleRepository = userRoleRepository;
        }

        public async Task<PagedResultDto<UserDto>> GetPagedUser(GetPagedUserInput input)
        {
            var query = UserManager.Users
                    .WhereIf(input.IsActive.HasValue, u => u.IsActive == input.IsActive)
                    .WhereIf(!input.FilterText.IsNullOrWhiteSpace(), u => u.UserName.Contains(input.FilterText));

            var totalCount = await query.CountAsync();
            var items = await query.PageBy(input).ToListAsync();

            var userDtos = ObjectMapper.Map<List<UserDto>>(items);
            await FillRoleNames(userDtos);

            return new PagedResultDto<UserDto>(totalCount, userDtos);
        }

        private async Task FillRoleNames(List<UserDto> userDtos)
        {
            var userRoles = await _userRoleRepository.GetAll()
                .Where(userRole => userDtos.Select(u => u.Id).Contains(userRole.UserId))
                .ToListAsync();

            foreach (var user in userDtos)
            {
                var roleIds = userRoles.Where(userRole => userRole.UserId == user.Id).Select(u => u.RoleId).ToList();

                var roleNames = await _roleRepository.GetAll()
                    .Where(r => roleIds.Contains(r.Id))
                    .Select(r => r.DisplayName)
                    .ToListAsync();

                user.RoleNames = roleNames.ToArray<string>();
            }
        }

        public async Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input)
        {
            var userRoleDtos = await _roleManager.Roles
                .OrderBy(r => r.DisplayName)
                .Select(r => new UserRoleDto()
                {
                    RoleId = r.Id,
                    RoleName = r.Name
                })
                .ToListAsync();

            var userOrganizationUnitDtos = await _organizationUnitRepository.GetAll()
                .Select(ou => new UserOrganizationUnitDto()
                {
                    Id = ou.Id,
                    ParentId = ou.ParentId,
                    DisplayName = ou.DisplayName,
                    OrganizationUnitCode = ou.Code
                })
                .ToListAsync();

            var userForEditOutput = new GetUserForEditOutput
            {
                Roles = userRoleDtos,
                OrganizationUnits = userOrganizationUnitDtos
            };

            if (!input.Id.HasValue)  //创建用户
            {
                userForEditOutput.User = new UserEditDto
                {
                    IsActive = true,
                    IsLockoutEnabled = true
                };

                var defaultRoles = await _roleManager.Roles.Where(r => r.IsDefault).ToListAsync();

                foreach (var defaultRole in defaultRoles)
                {
                    var defaultUserRole = userRoleDtos.FirstOrDefault(ur => ur.RoleName == defaultRole.Name);
                    if (defaultUserRole != null)
                    {
                        defaultUserRole.IsAssigned = true;
                    }
                }
            }
            else  //已存在的用户
            {
                var user = await UserManager.GetUserByIdAsync(input.Id.Value);
                userForEditOutput.User = ObjectMapper.Map<UserEditDto>(user);

                foreach (var userRoleDto in userRoleDtos)
                {
                    userRoleDto.IsAssigned = await UserManager.IsInRoleAsync(user, userRoleDto.RoleName);
                }

                foreach (var userOrganizationUnitDto in userOrganizationUnitDtos)
                {
                    userOrganizationUnitDto.IsAssigned = await UserManager.IsInOrganizationUnitAsync(user.Id, userOrganizationUnitDto.Id);
                }
            }

            return userForEditOutput;
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Users_Create)]
        public async Task CreateUser(CreateUserDto input)
        {
            var user = User.CreateUser(AbpSession.TenantId);
            user.IsEmailConfirmed = true;
            user.UserName = input.UserName;
            user.Name = "Name";
            user.Surname = "Surname";
            user.EmailAddress = input.EmailAddress;
            user.PhoneNumber = input.PhoneNumber;

            await UserManager.InitializeOptionsAsync(AbpSession.TenantId);
            foreach (var validator in _passwordValidators)
            {
                CheckErrors(await validator.ValidateAsync(UserManager, user, AbpTemplateApplicationConsts.DefaultPassword));
            }

            user.Password = _passwordHasher.HashPassword(user, AbpTemplateApplicationConsts.DefaultPassword);

            await UserManager.InitializeOptionsAsync(AbpSession.TenantId);

            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.AssignedRoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }

            CheckErrors(await UserManager.CreateAsync(user, AbpTemplateApplicationConsts.DefaultPassword));

            if (input.AssignedOrganizationUnitIds != null)
            {
                await UserManager.SetOrganizationUnitsAsync(user, input.AssignedOrganizationUnitIds);
            }

            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Users_Update)]
        public async Task UpdateUser(UpdateUserDto input)
        {
            var user = await UserManager.GetUserByIdAsync(input.Id);
            user.UserName = input.UserName;
            user.EmailAddress = input.EmailAddress;
            user.PhoneNumber = input.PhoneNumber;

            CheckErrors(await UserManager.UpdateAsync(user));

            if (input.AssignedRoleNames != null)
            {
                CheckErrors(await UserManager.SetRolesAsync(user, input.AssignedRoleNames));
            }

            if (input.AssignedOrganizationUnitIds != null)
            {
                await UserManager.SetOrganizationUnitsAsync(user, input.AssignedOrganizationUnitIds);
            }

            CurrentUnitOfWork.SaveChanges();
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Users_Delete)]
        public async Task DeleteUser(List<EntityDto<long>> inputs)
        {
            foreach (var input in inputs)
            {
                var user = await UserManager.GetUserByIdAsync(input.Id);
                await UserManager.DeleteAsync(user);
            }
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Users_ResetPassword)]
        public async Task<bool> ResetPassword(ResetPasswordInput input)
        {
            if (_abpSession.UserId == null)
            {
                throw new UserFriendlyException("请先登录系统！");
            }

            long currentUserId = _abpSession.UserId.Value;
            var currentUser = await UserManager.GetUserByIdAsync(currentUserId);
            if (currentUser.IsDeleted || !currentUser.IsActive)
            {
                return false;
            }

            var roles = await UserManager.GetRolesAsync(currentUser);
            if (!roles.Contains(StaticRoleNames.Tenants.Admin))
            {
                throw new UserFriendlyException("只有管理员能够重置密码！.");
            }

            var user = await UserManager.GetUserByIdAsync(input.UserId);
            if (user != null)
            {
                user.Password = _passwordHasher.HashPassword(user, AbpTemplateApplicationConsts.DefaultPassword);
                CurrentUnitOfWork.SaveChanges();
            }

            return true;
        }
    }
}

