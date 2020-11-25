using Abp.Notifications;
using AutoMapper;
using PearAdmin.Abp.Notifications.Dto;

namespace PearAdmin.Abp.Notifications
{
    internal static class NotificationMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<NotificationDefinition, NotificationSubscriptionWithDisplayNameDto>();
        }
    }
}
