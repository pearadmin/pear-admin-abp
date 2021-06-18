using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using PearAdmin.AbpTemplate.Authorization;
using PearAdmin.AbpTemplate.Authorization.Roles;
using PearAdmin.AbpTemplate.Authorization.Users;
using PearAdmin.AbpTemplate.Configuration;
using PearAdmin.AbpTemplate.Features;
using PearAdmin.AbpTemplate.Localization;
using PearAdmin.AbpTemplate.MultiTenancy;
using PearAdmin.AbpTemplate.Notifications;
using PearAdmin.AbpTemplate.Social.Chat;
using PearAdmin.AbpTemplate.Social.Friendships;
using PearAdmin.AbpTemplate.Timing;

namespace PearAdmin.AbpTemplate
{
    [DependsOn(
        typeof(AbpZeroCoreModule),
        typeof(AbpAutoMapperModule)
        )]
    public class AbpTemplateCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            AbpTemplateLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = AbpTemplateCoreConsts.MultiTenancyEnabled;

            // Configure roles
            AppRoleConfig.Configure(Configuration.Modules.Zero().RoleManagement);

            // Add setting providers
            Configuration.Settings.Providers.Add<AppSettingProvider>();

            // Add feature providers
            Configuration.Features.Providers.Add<AppFeatureProvider>();

            // Add notification providers
            Configuration.Notifications.Providers.Add<AppNotificationProvider>();

            // Add permission providers
            Configuration.Authorization.Providers.Add<AppPermissionProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpTemplateCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            IocManager.RegisterIfNot<IChatCommunicator, NullChatCommunicator>();
            IocManager.Resolve<ChatUserStateWatcher>().Initialize();
            IocManager.Resolve<AppTimes>().StartupTime = Clock.Now;
        }

        public override void Shutdown()
        {
            base.Shutdown();
        }
    }
}
