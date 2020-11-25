using Abp.Application.Services.Dto;

namespace PearAdmin.Abp.Organizations.Dto
{
    public class UpdateOrganizationUnitDto : EntityDto<long>
    {
        public string DisplayName { get; set; }
    }
}
