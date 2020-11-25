using Abp.Authorization;
using PearAdmin.Abp.Authorization.Roles;
using PearAdmin.Abp.Authorization.Users;

namespace PearAdmin.Abp.Authorization
{
    public class AppPermissionChecker : PermissionChecker<Role, User>
    {
        public AppPermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
