using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PearAdmin.Abp.Authorization;
using PearAdmin.Abp.Monitoring;
using PearAdmin.Abp.Notifications;
using PearAdmin.Abp.Organizations;
using PearAdmin.Abp.Resource;
using PearAdmin.Abp.Social;
using PearAdmin.Abp.MultiTenancy;
using PearAdmin.Abp.TaskCenter;

namespace PearAdmin.Abp
{
    [DependsOn(
        typeof(AbpCoreModule),
        typeof(AbpAutoMapperModule))]
    public class AbpApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            // AutoMapper
            Configuration.Modules.AbpAutoMapper().Configurators.Add(AuthorizationMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(OrganizationMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(SocialMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(NotificationMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(MonitoringMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(MultiTenancyMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(ResourceMapper.CreateMappings);
            Configuration.Modules.AbpAutoMapper().Configurators.Add(TaskCenterMapper.CreateMappings);
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(AbpApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }
    }
}
