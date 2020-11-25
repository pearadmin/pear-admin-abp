using Abp.Dependency;
using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.TaskCenter.AntiCorruption
{
    public interface IOrganizationService : ITransientDependency
    {
        Task GetOrganizationListAsync();
    }
}
