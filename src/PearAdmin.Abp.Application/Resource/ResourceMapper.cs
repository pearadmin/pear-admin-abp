using AutoMapper;
using PearAdmin.Abp.Resource.DataDictionaries;
using PearAdmin.Abp.Resource.DataDictionaries.Dto;

namespace PearAdmin.Abp.Resource
{
    internal static class ResourceMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<DataDictionaryItem, DataDictionaryItemDto>();
        }
    }
}
