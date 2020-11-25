using Abp.Organizations;
using AutoMapper;
using PearAdmin.AbpTemplate.Organizations.Dto;

namespace PearAdmin.AbpTemplate.Organizations
{
    internal static class OrganizationMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();
        }
    }
}
