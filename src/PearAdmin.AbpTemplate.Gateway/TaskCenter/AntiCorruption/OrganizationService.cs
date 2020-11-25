using PearAdmin.AbpTemplate.Organizations;
using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.TaskCenter.AntiCorruption
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IOrganizationUnitAppService _organizationUnitAppService;

        public OrganizationService(IOrganizationUnitAppService organizationUnitAppService)
        {
            _organizationUnitAppService = organizationUnitAppService;
        }

        public async Task GetOrganizationListAsync()
        {
            var test = await _organizationUnitAppService.GetAllOrganizationUnitTree();
        }
    }
}
