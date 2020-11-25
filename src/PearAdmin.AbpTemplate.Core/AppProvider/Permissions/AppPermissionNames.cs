namespace PearAdmin.AbpTemplate.Authorization
{
    public static class AppPermissionNames
    {
        #region GlobalPermission
        public const string Pages = "Pages";
        #endregion

        #region SystemManagement
        public const string Pages_SystemManagement = "Pages.SystemManagement";

        public const string Pages_SystemManagement_OrganizationUnits = "Pages.SystemManagement.OrganizationUnits";
        public const string Pages_SystemManagement_OrganizationUnits_Create = "Pages.SystemManagement.OrganizationUnits.Create";
        public const string Pages_SystemManagement_OrganizationUnits_Update = "Pages.SystemManagement.OrganizationUnits.Update";
        public const string Pages_SystemManagement_OrganizationUnits_Delete = "Pages.SystemManagement.OrganizationUnits.Delete";
        public const string Pages_SystemManagement_OrganizationUnits_MoveOrganizationUnit = "Pages.SystemManagement.OrganizationUnits.MoveOrganizationUnit";

        public const string Pages_SystemManagement_Users = "Pages.SystemManagement.Users";
        public const string Pages_SystemManagement_Users_Create = "Pages.SystemManagement.Users.Create";
        public const string Pages_SystemManagement_Users_Update = "Pages.SystemManagement.Users.Update";
        public const string Pages_SystemManagement_Users_Delete = "Pages.SystemManagement.Users.Delete";
        public const string Pages_SystemManagement_Users_ResetPassword = "Pages.SystemManagement.Users.ResetPassword";

        public const string Pages_SystemManagement_Roles = "Pages.SystemManagement.Roles";
        public const string Pages_SystemManagement_Roles_Create = "Pages.SystemManagement.Roles.Create";
        public const string Pages_SystemManagement_Roles_Update = "Pages.SystemManagement.Roles.Update";
        public const string Pages_SystemManagement_Roles_Delete = "Pages.SystemManagement.Roles.Delete";

        public const string Pages_SystemManagement_Permissions = "Pages.SystemManagement.Permissions";

        public const string Pages_SystemManagement_AuditLogs = "Pages.SystemManagement.AuditLogs";

        public const string Pages_SystemManagement_Editions = "Pages.SystemManagement.Editions";
        public const string Pages_SystemManagement_Editions_Create = "Pages.SystemManagement.Editions.Create";
        public const string Pages_SystemManagement_Editions_Update = "Pages.SystemManagement.Editions.Update";
        public const string Pages_SystemManagement_Editions_Delete = "Pages.SystemManagement.Editions.Delete";

        public const string Pages_SystemManagement_Tenants = "Pages.SystemManagement.Tenants";
        public const string Pages_SystemManagement_Tenants_ChangeTenantEdition = "Pages.SystemManagement.Tenants.ChangeTenantEdition";

        public const string Pages_SystemManagement_TenantSettings = "Pages.SystemManagement.TenantSettings";
        public const string Pages_SystemManagement_HostSettings = "Pages.SystemManagement.HostSettings";

        public const string Pages_SystemManagement_Maintenance = "Pages.SystemManagement.Maintenance";
        public const string Pages_SystemManagement_Maintenance_Logs = "Pages.SystemManagement.Maintenance.Logs";
        public const string Pages_SystemManagement_Maintenance_Logs_DownLoad = "Pages.SystemManagement.Maintenance.Logs.DownLoad";
        public const string Pages_SystemManagement_Maintenance_Logs_Refresh = "Pages.SystemManagement.Maintenance.Logs.Refresh";
        #endregion

        #region ResourceManagement
        public const string Pages_ResourceManagement = "Pages.ResourceManagement";

        public const string Pages_ResourceManagement_DataDictionary = "Pages.ResourceManagement.DataDictionary";
        public const string Pages_ResourceManagement_DataDictionary_DataDictionaryItem = "Pages.ResourceManagement.DataDictionary.DataDictionaryItem";
        public const string Pages_ResourceManagement_DataDictionary_DataDictionaryItem_Create = "Pages.ResourceManagement.DataDictionary.DataDictionaryItem.Create";
        public const string Pages_ResourceManagement_DataDictionary_DataDictionaryItem_Update = "Pages.ResourceManagement.DataDictionary.DataDictionaryItem.Update";
        public const string Pages_ResourceManagement_DataDictionary_DataDictionaryItem_Delete = "Pages.ResourceManagement.DataDictionary.DataDictionaryItem.Delete";
        #endregion

        #region TaskCenter
        public const string Pages_TaskCenter = "Pages.TaskCenter";

        public const string Pages_TaskCenter_DailyTasks = "Pages.TaskCenter.DailyTasks";
        public const string Pages_TaskCenter_DailyTasks_Create = "Pages.TaskCenter.DailyTasks.Create";
        public const string Pages_TaskCenter_DailyTasks_Update = "Pages.TaskCenter.DailyTasks.Update";
        public const string Pages_TaskCenter_DailyTasks_Delete = "Pages.TaskCenter.DailyTasks.Delete";
        #endregion

        #region WorkSpace
        public const string Pages_WorkSpace = "Pages.WorkSpace";
        public const string Pages_WorkSpace_TenantConsole = "Pages.WorkSpace.TenantConsole";
        public const string Pages_WorkSpace_HostConsole = "Pages.WorkSpace.HostConsole";
        #endregion
    }
}
