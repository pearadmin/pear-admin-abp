using Abp.AutoMapper;
using PearAdmin.Abp.Organizations.Dto;
using PearAdmin.Abp.Admin.Models.Common;

namespace PearAdmin.Abp.Admin.Models.OrganizationUnits
{
    /// <summary>
    /// 组织机构视图模型
    /// </summary>
    [AutoMap(typeof(OrganizationUnitDto))]
    public class OrganizationUnitListViewModel : EntityViewModel<long>
    {
        public long? ParentId { get; set; }

        public string DisplayName { get; set; }

        public bool Checked { get; set; } = false;
    }
}
