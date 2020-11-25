using Abp.Application.Services.Dto;

namespace PearAdmin.AbpTemplate.Organizations.Dto
{
    public class UpdateOrganizationUnitDto : EntityDto<long>
    {
        public string DisplayName { get; set; }
    }
}
