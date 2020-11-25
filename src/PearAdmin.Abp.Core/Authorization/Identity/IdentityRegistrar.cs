using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PearAdmin.Abp.Authorization;
using PearAdmin.Abp.Authorization.Roles;
using PearAdmin.Abp.Authorization.Users;
using PearAdmin.Abp.Editions;
using PearAdmin.Abp.MultiTenancy;

namespace PearAdmin.Abp.Identity
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
