using PearAdmin.Abp.CommonDto;

namespace PearAdmin.Abp.Organizations.Dto
{
    /// <summary>
    /// 组织机构列表分页请求Dto
    /// </summary>
    public class GetPagedOrganizationUnitInput : PagedInputDto
    {
        public long? Id { get; set; }

        public string DisplayName { get; set; }
    }
}
