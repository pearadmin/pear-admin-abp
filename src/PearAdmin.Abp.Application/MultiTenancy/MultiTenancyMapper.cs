using Abp.Application.Editions;
using AutoMapper;
using PearAdmin.Abp.MultiTenancy.Editions.Dto;
using PearAdmin.Abp.MultiTenancy.Tenants.Dto;

namespace PearAdmin.Abp.MultiTenancy
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
