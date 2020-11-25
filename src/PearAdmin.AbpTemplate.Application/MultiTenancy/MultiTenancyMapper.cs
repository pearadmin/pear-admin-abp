using Abp.Application.Editions;
using AutoMapper;
using PearAdmin.AbpTemplate.MultiTenancy.Editions.Dto;
using PearAdmin.AbpTemplate.MultiTenancy.Tenants.Dto;

namespace PearAdmin.AbpTemplate.MultiTenancy
{
    internal static class MultiTenancyMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Tenant, TenantDto>();
            configuration.CreateMap<Edition, EditionDto>();
        }
    }
}
