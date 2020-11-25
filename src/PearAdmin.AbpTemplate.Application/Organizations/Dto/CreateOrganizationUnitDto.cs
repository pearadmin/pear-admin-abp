using Abp.Application.Services.Dto;

namespace PearAdmin.AbpTemplate.Organizations.Dto
{
    public class CreateOrganizationUnitDto
    {
        public long? ParentId { get; set; }

        public string DisplayName { get; set; }
    }
}
