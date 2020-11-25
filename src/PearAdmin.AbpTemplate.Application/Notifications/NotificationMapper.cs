using Abp.Notifications;
using AutoMapper;
using PearAdmin.AbpTemplate.Notifications.Dto;

namespace PearAdmin.AbpTemplate.Notifications
{
    internal static class NotificationMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
        }
    }
}
