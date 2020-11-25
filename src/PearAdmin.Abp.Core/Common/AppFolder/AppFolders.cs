using Abp.Dependency;

namespace PearAdmin.Abp
{
    public class AppFolders : IAppFolders, ISingletonDependency
    {
        public string WebLogsFolder { get; set; }
    }
}
