using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PearAdmin.AbpTemplate.Authorization;
using PearAdmin.AbpTemplate.Monitoring;
using PearAdmin.AbpTemplate.Notifications;
using PearAdmin.AbpTemplate.Organizations;
using PearAdmin.AbpTemplate.Resource;
using PearAdmin.AbpTemplate.Social;
using PearAdmin.AbpTemplate.MultiTenancy;
using PearAdmin.AbpTemplate.TaskCenter;

namespace PearAdmin.AbpTemplate
{
    [DependsOn(
        typeof(AbpTemplateCoreModule),
        typeof(AbpAutoMapperModule))]
    public class AbpTemplateApplicationModule : AbpModule
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
            var thisAssembly = typeof(AbpTemplateApplicationModule).GetAssembly();

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
