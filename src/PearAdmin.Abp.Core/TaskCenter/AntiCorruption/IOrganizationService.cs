using Abp.Dependency;
using System.Threading.Tasks;

namespace PearAdmin.Abp.TaskCenter.AntiCorruption
{
    public interface IOrganizationService : ITransientDependency
    {
        Task GetOrganizationListAsync();
    }
}
