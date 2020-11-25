using Abp.Application.Services.Dto;

namespace PearAdmin.AbpTemplate.MultiTenancy.Tenants.Dto
{
    public class ChangeTenantEditionDto : EntityDto<int>
    {
        public int SourceEditionId { get; set; }

        public int TargetEditionId { get; set; }
    }
}

