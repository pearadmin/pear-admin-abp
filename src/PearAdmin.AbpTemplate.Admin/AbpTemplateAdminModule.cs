using System.IO;
using Abp.AspNetCore;
using Abp.AspNetCore.SignalR;
using Abp.Configuration.Startup;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using PearAdmin.AbpTemplate.Admin.Configuration;
using PearAdmin.AbpTemplate.Admin.Views;
using PearAdmin.AbpTemplate.EntityFrameworkCore;
using PearAdmin.AbpTemplate.Gateway;

namespace PearAdmin.AbpTemplate.Admin
{
    [DependsOn(
        typeof(AbpTemplateApplicationModule),
        typeof(AbpTemplateEntityFrameworkModule),
        typeof(AbpTemplateGatewayModule),
        typeof(AbpAspNetCoreModule),
        typeof(AbpAspNetCoreSignalRModule),
        typeof(AbpHangfireAspNetCoreModule)
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
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(AbpTemplateCoreConsts.ConnectionStringName);

            // 本地化内容存储到数据库
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            // 显示所有错误信息到客户端
            Configuration.Modules.AbpWebCommon().SendAllExceptionsToClients = false;

            Configuration.Navigation.Providers.Add<AbpTemplateNavigationProvider>();

            Configuration.BackgroundJobs.UseHangfire();
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
