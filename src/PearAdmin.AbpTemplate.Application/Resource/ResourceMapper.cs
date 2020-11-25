using AutoMapper;
using PearAdmin.AbpTemplate.Resource.DataDictionaries;
using PearAdmin.AbpTemplate.Resource.DataDictionaries.Dto;

namespace PearAdmin.AbpTemplate.Resource
{
    internal static class ResourceMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<DataDictionaryItem, DataDictionaryItemDto>();
        }
    }
}
