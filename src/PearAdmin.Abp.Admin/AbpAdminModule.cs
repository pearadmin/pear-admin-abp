using System.IO;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using PearAdmin.Abp.Admin.Configuration;
using PearAdmin.Abp.Admin.ViewResources;
using PearAdmin.Abp.EntityFrameworkCore;
using PearAdmin.Abp.Gateway;

namespace PearAdmin.Abp.Admin
{
    [DependsOn(
        typeof(AbpApplicationModule),
        typeof(AbpEntityFrameworkModule),
        typeof(AbpGatewayModule),
        typeof(AbpAspNetCoreModule),
        typeof(AbpAspNetCoreSignalRModule)
        )]
    public class AbpAdminModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AbpAdminModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                AbpCoreConsts.ConnectionStringName
            );

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();


            Configuration.Navigation.Providers.Add<AbpNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpAdminModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            SetAppFolders();
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(AbpAdminModule).Assembly);
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }

        private void SetAppFolders()
        {
            var appFolders = IocManager.Resolve<AppFolders>();

            appFolders.WebLogsFolder = Path.Combine(_env.ContentRootPath, $"App_Data{Path.DirectorySeparatorChar}Logs");
        }
    }
}
