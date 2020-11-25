using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace PearAdmin.Abp.Authorization
{
    public class AppPermissionProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            #region GlobalPermission
            var pages = context.CreatePermission(AppPermissionNames.Pages, L("Pages"));
            #endregion

            #region SystemManagement
            var systemManagement = pages.CreateChildPermission(AppPermissionNames.Pages_SystemManagement, L("SystemManagement"));

            var organizationUnits = systemManagement.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_OrganizationUnits, L("OrganizationUnitManagement"), multiTenancySides: MultiTenancySides.Tenant);
            organizationUnits.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_OrganizationUnits_Create, L("CreateOrganizationUnit"), multiTenancySides: MultiTenancySides.Tenant);
            organizationUnits.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_OrganizationUnits_Update, L("UpdateOrganizationUnit"), multiTenancySides: MultiTenancySides.Tenant);
            organizationUnits.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_OrganizationUnits_Delete, L("DeleteOrganizationUnit"), multiTenancySides: MultiTenancySides.Tenant);
            organizationUnits.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_OrganizationUnits_MoveOrganizationUnit, L("MoveOrganizationUnit"), multiTenancySides: MultiTenancySides.Tenant);

            var users = systemManagement.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Users, L("UserManagement"), multiTenancySides: MultiTenancySides.Tenant);
            users.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Users_Create, L("CreateUser"), multiTenancySides: MultiTenancySides.Tenant);
            users.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Users_Update, L("UpdateUser"), multiTenancySides: MultiTenancySides.Tenant);
            users.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Users_Delete, L("DeleteUser"), multiTenancySides: MultiTenancySides.Tenant);
            users.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Users_ResetPassword, L("ResetPassword"), multiTenancySides: MultiTenancySides.Tenant);

            var roles = systemManagement.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Roles, L("RoleManagement"), multiTenancySides: MultiTenancySides.Tenant);
            roles.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Roles_Create, L("CreateRole"), multiTenancySides: MultiTenancySides.Tenant);
            roles.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Roles_Update, L("UpdateRole"), multiTenancySides: MultiTenancySides.Tenant);
            roles.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Roles_Delete, L("DeleteRole"), multiTenancySides: MultiTenancySides.Tenant);

            systemManagement.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Permissions, L("PermissionManagement"), multiTenancySides: MultiTenancySides.Tenant);

            systemManagement.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_AuditLogs, L("AuditLogs"));

            var editions = systemManagement.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Editions, L("EditionManagement"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Editions_Create, L("CreateEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Editions_Update, L("UpdateEdition"), multiTenancySides: MultiTenancySides.Host);
            editions.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Editions_Delete, L("DeleteEdition"), multiTenancySides: MultiTenancySides.Host);

            var tenants = systemManagement.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Tenants, L("TenantManagement"), multiTenancySides: MultiTenancySides.Host);
            tenants.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Tenants_ChangeTenantEdition, L("ChangeTenantEdition"), multiTenancySides: MultiTenancySides.Host);

            systemManagement.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_TenantSettings, L("TenantSettings"), multiTenancySides: MultiTenancySides.Tenant);
            systemManagement.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_HostSettings, L("HostSettings"), multiTenancySides: MultiTenancySides.Host);

            var maintenance = systemManagement.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Maintenance, L("Maintenance"));
            var logs = maintenance.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Maintenance_Logs, L("Logs"));
            logs.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Maintenance_Logs_DownLoad, L("DownLoadLog"));
            logs.CreateChildPermission(AppPermissionNames.Pages_SystemManagement_Maintenance_Logs_Refresh, L("RefreshLog"));
            #endregion

            #region ResourceManagement
            var resourceManagement = pages.CreateChildPermission(AppPermissionNames.Pages_ResourceManagement, L("ResourceManagement"), multiTenancySides: MultiTenancySides.Tenant);

            var dataDictionary = resourceManagement.CreateChildPermission(AppPermissionNames.Pages_ResourceManagement_DataDictionary, L("DataDictionary"), multiTenancySides: MultiTenancySides.Tenant);

            var dataDictionaryItem = dataDictionary.CreateChildPermission(AppPermissionNames.Pages_ResourceManagement_DataDictionary_DataDictionaryItem, L("DataDictionaryItem"), multiTenancySides: MultiTenancySides.Tenant);
            dataDictionaryItem.CreateChildPermission(AppPermissionNames.Pages_ResourceManagement_DataDictionary_DataDictionaryItem_Create, L("CreateDataDictionaryItem"), multiTenancySides: MultiTenancySides.Tenant);
            dataDictionaryItem.CreateChildPermission(AppPermissionNames.Pages_ResourceManagement_DataDictionary_DataDictionaryItem_Update, L("UpdateDataDictionaryItem"), multiTenancySides: MultiTenancySides.Tenant);
            dataDictionaryItem.CreateChildPermission(AppPermissionNames.Pages_ResourceManagement_DataDictionary_DataDictionaryItem_Delete, L("DeleteDataDictionaryItem"), multiTenancySides: MultiTenancySides.Tenant);
            #endregion

            #region TaskCenter
            var taskCenter = pages.CreateChildPermission(AppPermissionNames.Pages_TaskCenter, L("TaskCenter"), multiTenancySides: MultiTenancySides.Tenant);
            var dailyTasks = taskCenter.CreateChildPermission(AppPermissionNames.Pages_TaskCenter_DailyTasks, L("DailyTask"), multiTenancySides: MultiTenancySides.Tenant);
            dailyTasks.CreateChildPermission(AppPermissionNames.Pages_TaskCenter_DailyTasks_Create, L("CreateDailyTask"), multiTenancySides: MultiTenancySides.Tenant);
            dailyTasks.CreateChildPermission(AppPermissionNames.Pages_TaskCenter_DailyTasks_Update, L("UpdateDailyTask"), multiTenancySides: MultiTenancySides.Tenant);
            dailyTasks.CreateChildPermission(AppPermissionNames.Pages_TaskCenter_DailyTasks_Delete, L("DeleteDailyTask"), multiTenancySides: MultiTenancySides.Tenant);
            #endregion

            #region WorkSpace
            var workSpace = pages.CreateChildPermission(AppPermissionNames.Pages_WorkSpace, L("WorkSpace"));
            workSpace.CreateChildPermission(AppPermissionNames.Pages_WorkSpace_TenantConsole, L("TenantConsole"), multiTenancySides: MultiTenancySides.Tenant);
            workSpace.CreateChildPermission(AppPermissionNames.Pages_WorkSpace_HostConsole, L("HostConsole"), multiTenancySides: MultiTenancySides.Host);
            #endregion
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpCoreConsts.LocalizationSourceName);
        }
    }
}
