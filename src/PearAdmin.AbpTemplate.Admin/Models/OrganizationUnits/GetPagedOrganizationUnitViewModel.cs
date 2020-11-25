using Abp.AutoMapper;
using PearAdmin.AbpTemplate.Organizations.Dto;
using PearAdmin.AbpTemplate.Admin.Models.Common;

namespace PearAdmin.AbpTemplate.Admin.Models.OrganizationUnits
{
    /// <summary>
    /// 组织机构分页模型
    /// </summary>
    [AutoMapTo(typeof(GetPagedOrganizationUnitInput))]
    public class GetPagedOrganizationUnitViewModel : PagedViewModel
    {
        public long? Id { get; set; }

        public string DisplayName { get; set; }
    }
}
