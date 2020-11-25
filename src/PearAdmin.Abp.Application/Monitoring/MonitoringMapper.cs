using Abp.Auditing;
using AutoMapper;
using PearAdmin.Abp.Auditing.Dto;

namespace PearAdmin.Abp.Monitoring
{
    internal static class MonitoringMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<AuditLog, AuditLogListDto>();
        }
    }
}
