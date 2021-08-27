using Castle.Facilities.Logging;
using Microsoft.Extensions.Configuration;

namespace PearAdmin.AbpTemplate.NLog
{
    public static class LoggingFacilityExtensions
    {
        public static LoggingFacility UseAbpNLog(this LoggingFacility loggingFacility, IConfigurationSection section)
        {
            return loggingFacility.LogUsing(new NLogLoggerFactory(section));
        }
    }
}