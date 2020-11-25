using Abp.Authorization;
using PearAdmin.AbpTemplate.Authorization.Roles;
using PearAdmin.AbpTemplate.Authorization.Users;

namespace PearAdmin.AbpTemplate.Authorization
{
    public class AppPermissionChecker : PermissionChecker<Role, User>
    {
        public AppPermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
