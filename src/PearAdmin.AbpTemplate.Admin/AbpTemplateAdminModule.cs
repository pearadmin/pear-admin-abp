using System.IO;
using Abp.AspNetCore;
using Abp.AspNetCore.SignalR;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using PearAdmin.AbpTemplate.Admin.Configuration;
using PearAdmin.AbpTemplate.Admin.ViewResources;
using PearAdmin.AbpTemplate.EntityFrameworkCore;
using PearAdmin.AbpTemplate.Gateway;

namespace PearAdmin.AbpTemplate.Admin
{
    [DependsOn(
        typeof(AbpTemplateApplicationModule),
        typeof(AbpTemplateEntityFrameworkModule),
        typeof(AbpTemplateGatewayModule),
        typeof(AbpAspNetCoreModule),
        typeof(AbpAspNetCoreSignalRModule)
        )]
    public class AbpTemplateAdminModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public AbpTemplateAdminModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                AbpTemplateCoreConsts.ConnectionStringName
            );

            // Use database for language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();


            Configuration.Navigation.Providers.Add<AbpTemplateNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpTemplateAdminModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            SetAppFolders();
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(AbpTemplateAdminModule).Assembly);
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
