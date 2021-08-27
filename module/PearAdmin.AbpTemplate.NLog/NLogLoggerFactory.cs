using System;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;
using NLogCore = NLog;

namespace PearAdmin.AbpTemplate.NLog
{
    public class NLogLoggerFactory : AbstractLoggerFactory
    {
        public NLogLoggerFactory(IConfigurationSection section)
        {
            NLogCore.LogManager.Configuration = new NLogLoggingConfiguration(section);
        }

        public override ILogger Create(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            return new NLogLogger(NLogCore.LogManager.GetLogger(name));
        }

        public override ILogger Create(string name, LoggerLevel level)
        {
            throw new NotSupportedException("Logger levels cannot be set at runtime. Please review your configuration file.");
        }
    }
}