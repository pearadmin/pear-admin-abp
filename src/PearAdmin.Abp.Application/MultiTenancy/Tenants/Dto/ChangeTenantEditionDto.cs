using Abp.Application.Services.Dto;

namespace PearAdmin.Abp.MultiTenancy.Tenants.Dto
{
    public class ChangeTenantEditionDto : EntityDto<int>
    {
        public int SourceEditionId { get; set; }

        public int TargetEditionId { get; set; }
    }
}

