using Abp.Organizations;
using AutoMapper;
using PearAdmin.Abp.Organizations.Dto;

namespace PearAdmin.Abp.Organizations
{
    internal static class OrganizationMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<OrganizationUnit, OrganizationUnitDto>();
        }
    }
}
