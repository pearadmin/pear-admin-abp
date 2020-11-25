using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PearAdmin.AbpTemplate.Authorization;
using PearAdmin.AbpTemplate.Authorization.Roles;
using PearAdmin.AbpTemplate.Authorization.Users;
using PearAdmin.AbpTemplate.Editions;
using PearAdmin.AbpTemplate.MultiTenancy;

namespace PearAdmin.AbpTemplate.Identity
{
    public static class IdentityRegistrar
    {
        public static IdentityBuilder Register(IServiceCollection services)
        {
            services.AddLogging();

            return services.AddAbpIdentity<Tenant, User, Role>()
                .AddAbpTenantManager<TenantManager>()
                .AddAbpUserManager<UserManager>()
                .AddAbpRoleManager<RoleManager>()
                .AddAbpEditionManager<EditionManager>()
                .AddAbpUserStore<UserStore>()
                .AddAbpRoleStore<RoleStore>()
                .AddAbpLogInManager<LogInManager>()
                .AddAbpSignInManager<SignInManager>()
                .AddAbpSecurityStampValidator<SecurityStampValidator>()
                .AddAbpUserClaimsPrincipalFactory<UserClaimsPrincipalFactory>()
                .AddPermissionChecker<AppPermissionChecker>()
                .AddDefaultTokenProviders();
        }
    }
}
