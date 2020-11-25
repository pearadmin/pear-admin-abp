using Abp.Authorization;
using Abp.Runtime.Validation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PearAdmin.AbpTemplate.Authorization.Permissions
{
    /// <summary>
    /// 权限管理扩展
    /// </summary>
    public static class PermissionManagerExtensions
    {
        /// <summary>
        /// 通过权限名称获取所有的权限信息
        /// </summary>
        /// <param name="permissionManager"></param>
        /// <param name="AppPermissions"></param>
        /// <returns></returns>
        public static IEnumerable<Permission> GetPermissionsFromNamesByValidating(
            this IPermissionManager permissionManager, IEnumerable<string> AppPermissions)
        {
            var permissions = new List<Permission>();
            var undefinedAppPermissions = new List<string>();

            foreach (var permissionName in AppPermissions)
            {
                var permission = permissionManager.GetPermissionOrNull(permissionName);
                if (permission == null) undefinedAppPermissions.Add(permissionName);

                permissions.Add(permission);
            }

            if (undefinedAppPermissions.Count > 0)
                throw new AbpValidationException(
                    $"There are {undefinedAppPermissions.Count} undefined permission names.")
                {
                    ValidationErrors = undefinedAppPermissions.ConvertAll(permissionName =>
                        new ValidationResult("Undefined permission: " + permissionName))
                };

            return permissions;
        }
    }
}
