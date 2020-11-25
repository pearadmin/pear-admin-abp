using Abp.AutoMapper;
using PearAdmin.AbpTemplate.Organizations.Dto;
using PearAdmin.AbpTemplate.Admin.Models.Common;

namespace PearAdmin.AbpTemplate.Admin.Models.OrganizationUnits
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
