using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using PearAdmin.Abp.MultiTenancy;

namespace PearAdmin.Abp.Sessions.Dto
{
    [AutoMapFrom(typeof(Tenant))]
    public class TenantLoginInfoDto : EntityDto
    {
        public string TenancyName { get; set; }

        public string Name { get; set; }
    }
}
