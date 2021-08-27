using System;
using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Dependency;
using Abp.Hangfire;
using Abp.Json;
using Hangfire;
using Hangfire.MemoryStorage;
using LogDashboard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using PearAdmin.AbpTemplate.Admin.Configuration;
using PearAdmin.AbpTemplate.Admin.Extensions;
using PearAdmin.AbpTemplate.Admin.SignalR;
using PearAdmin.AbpTemplate.Authorization;
using PearAdmin.AbpTemplate.Identity;

namespace PearAdmin.AbpTemplate.Admin
{
    public class Startup
    {
        private readonly IConfigurationRoot Configuration;

        public Startup(IWebHostEnvironment env)
        {
            Configuration = env.GetAppConfiguration();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
                })
                .AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new AbpMvcContractResolver(IocManager.Instance)
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                });

            services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters = null;//修改用户名验证规则
            });

            IdentityRegistrar.Register(services);

            services.AddSignalR();

            services.AddHangfire(config =>
            {
#if DEBUG
                config.UseMemoryStorage();
#else
                var redisConnectionString = _appConfiguration.GetConnectionString(AbpTemplateCoreConsts.RedisConnectionStringName);
                config.UseRedisStorage(redisConnectionString);
#endif
            });

            services.AddLogDashboard();

            return services.AddAbp<AbpTemplateAdminModule>(AbpBootstrapperOptionsExtension.GetOptions(Configuration));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHangfireServer();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[]
                {
                    new AbpHangfireAuthorizationFilter(AppPermissionNames.Pages_SystemManagement_HangfireDashboard)
                }
            });

            app.UseLogDashboard();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AbpCommonHub>("/signalr");
                endpoints.MapHub<ChatHub>("/signalr-chat");
                endpoints.MapControllerRoute("defaultWithArea", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
