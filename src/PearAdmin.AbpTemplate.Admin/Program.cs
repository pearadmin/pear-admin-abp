using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace PearAdmin.AbpTemplate.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"))
                        .AddDebug()
                        .AddEventSourceLogger()
                        .AddFilter("System", LogLevel.Debug)
                        .AddFilter("Microsoft.EntityFrameworkCore.*", LogLevel.Warning)
                        .AddFilter("Microsoft.AspNetCore.*", LogLevel.Error);
                })
                .Build();
        }
    }
}
