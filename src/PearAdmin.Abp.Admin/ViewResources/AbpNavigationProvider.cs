using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using PearAdmin.Abp.Authorization;

namespace PearAdmin.Abp.Admin.ViewResources
{
    /// <summary>
    /// 菜单定义
    /// </summary>
    public class AbpNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        PageNames.WorkSpace,
                        L("WorkSpace"),
                        icon: "layui-icon-console",
                        permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_WorkSpace)
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.TenantConsole,
                            L("TenantConsole"),
                            url: "/WorkSpace/TenantConsole",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_WorkSpace_TenantConsole)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.HostConsole,
                            L("HostConsole"),
                            url: "/WorkSpace/HostConsole",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_WorkSpace_HostConsole)
                        )
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.TaskCenter,
                        L("TaskCenter"),
                        icon: "layui-icon-read",
                        permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_TaskCenter)
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.DailyTask,
                            L("DailyTask"),
                            url: "/TaskCenter/DailyTask",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_TaskCenter_DailyTasks)
                        )
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.ResourceManagement,
                        L("ResourceManagement"),
                        icon: "layui-icon-engine",
                        permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_ResourceManagement)
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.DataDictionary,
                            L("DataDictionary"),
                            url: "/Resource/DataDictionary",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_ResourceManagement_DataDictionary)
                        )
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        PageNames.SystemManagement,
                        L("SystemManagement"),
                        icon: "layui-icon-set-fill",
                        permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement)
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.OrganizationUnits,
                            L("OrganizationUnitManagement"),
                            url: "OrganizationUnits",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_OrganizationUnits)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.Users,
                            L("UserManagement"),
                            url: "Users",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_Users)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.Roles,
                            L("RoleManagement"),
                            url: "Roles",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_Roles)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.Permissions,
                            L("PermissionManagement"),
                            url: "Permissions",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_Permissions)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.AuditLogs,
                            L("AuditLogs"),
                            url: "AuditLogs",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_AuditLogs)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.Editions,
                            L("EditionManagement"),
                            url: "Editions",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_Editions)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.Tenants,
                            L("TenantManagement"),
                            url: "Tenants",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_Tenants)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.TenantSettings,
                            L("TenantSettings"),
                            url: "TenantSettings",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_TenantSettings)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.HostSettings,
                            L("HostSettings"),
                            url: "HostSettings",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_HostSettings)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            PageNames.Maintenance,
                            L("Maintenance"),
                            url: "Maintenance",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_Maintenance)
                        )
                    )
                );
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpCoreConsts.LocalizationSourceName);
        }
    }
}
