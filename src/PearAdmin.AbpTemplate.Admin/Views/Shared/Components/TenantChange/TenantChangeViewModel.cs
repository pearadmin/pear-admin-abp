using Abp.AutoMapper;
using PearAdmin.AbpTemplate.Sessions.Dto;

namespace PearAdmin.AbpTemplate.Admin.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
