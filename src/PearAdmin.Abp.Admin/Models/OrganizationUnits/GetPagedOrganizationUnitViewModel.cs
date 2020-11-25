using Abp.AutoMapper;
using PearAdmin.Abp.Organizations.Dto;
using PearAdmin.Abp.Admin.Models.Common;

namespace PearAdmin.Abp.Admin.Models.OrganizationUnits
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
