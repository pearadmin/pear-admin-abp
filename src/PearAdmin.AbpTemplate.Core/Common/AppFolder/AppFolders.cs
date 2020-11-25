using Abp.Dependency;

namespace PearAdmin.AbpTemplate
{
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string WebLogsFolder { get; set; }
    }
}
