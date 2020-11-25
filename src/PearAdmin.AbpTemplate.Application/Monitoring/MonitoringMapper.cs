using Abp.Auditing;
using AutoMapper;
using PearAdmin.AbpTemplate.Auditing.Dto;

namespace PearAdmin.AbpTemplate.Monitoring
{
    internal static class MonitoringMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<AuditLog, AuditLogListDto>();
        }
    }
}
