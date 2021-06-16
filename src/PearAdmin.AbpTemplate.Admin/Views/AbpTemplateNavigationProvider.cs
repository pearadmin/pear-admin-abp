using Abp.Application.Navigation;
using Abp.Authorization;
using Abp.Localization;
using PearAdmin.AbpTemplate.Authorization;

namespace PearAdmin.AbpTemplate.Admin.Views
{
    /// <summary>
    /// 菜单定义
    /// </summary>
    public class AbpTemplateNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        AbpTemplatePageName.WorkSpace,
                        L("WorkSpace"),
                        icon: "layui-icon-console",
                        permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_WorkSpace)
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.TenantConsole,
                            L("TenantConsole"),
                            url: "/WorkSpace/TenantConsole",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_WorkSpace_TenantConsole)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.HostConsole,
                            L("HostConsole"),
                            url: "/WorkSpace/HostConsole",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_WorkSpace_HostConsole)
                        )
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        AbpTemplatePageName.TaskCenter,
                        L("TaskCenter"),
                        icon: "layui-icon-read",
                        permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_TaskCenter)
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.DailyTask,
                            L("DailyTask"),
                            url: "/TaskCenter/DailyTask",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_TaskCenter_DailyTasks)
                        )
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        AbpTemplatePageName.ResourceManagement,
                        L("ResourceManagement"),
                        icon: "layui-icon-engine",
                        permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_ResourceManagement)
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.DataDictionary,
                            L("DataDictionary"),
                            url: "/Resource/DataDictionary",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_ResourceManagement_DataDictionary)
                        )
                    )
                ).AddItem(
                    new MenuItemDefinition(
                        AbpTemplatePageName.SystemManagement,
                        L("SystemManagement"),
                        icon: "layui-icon-set-fill",
                        permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement)
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.OrganizationUnits,
                            L("OrganizationUnitManagement"),
                            url: "OrganizationUnits",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_OrganizationUnits)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.Users,
                            L("UserManagement"),
                            url: "Users",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_Users)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.Roles,
                            L("RoleManagement"),
                            url: "Roles",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_Roles)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.Permissions,
                            L("PermissionManagement"),
                            url: "Permissions",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_Permissions)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.AuditLogs,
                            L("AuditLogs"),
                            url: "AuditLogs",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_AuditLogs)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.Editions,
                            L("EditionManagement"),
                            url: "Editions",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_Editions)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.Tenants,
                            L("TenantManagement"),
                            url: "Tenants",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_Tenants)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.TenantSettings,
                            L("TenantSettings"),
                            url: "TenantSettings",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_TenantSettings)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.HostSettings,
                            L("HostSettings"),
                            url: "HostSettings",
                            icon: "layui-icon-console",
                            permissionDependency: new SimplePermissionDependency(AppPermissionNames.Pages_SystemManagement_HostSettings)
                        )
                    ).AddItem(
                        new MenuItemDefinition(
                            AbpTemplatePageName.Maintenance,
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
            return new LocalizableString(name, AbpTemplateCoreConsts.LocalizationSourceName);
        }
    }
}
