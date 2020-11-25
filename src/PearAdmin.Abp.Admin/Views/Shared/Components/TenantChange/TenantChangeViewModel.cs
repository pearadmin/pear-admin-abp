using Abp.AutoMapper;
using PearAdmin.Abp.Sessions.Dto;

namespace PearAdmin.Abp.Admin.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
