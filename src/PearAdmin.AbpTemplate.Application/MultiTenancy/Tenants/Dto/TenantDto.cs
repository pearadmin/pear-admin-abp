using Abp.Application.Services.Dto;

namespace PearAdmin.AbpTemplate.MultiTenancy.Tenants.Dto
{
    public class TenantDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string EditionName { get; set; }

        public int EditionId { get; set; }
    }
}