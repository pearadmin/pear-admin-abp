using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Timing;
using Abp.Zero;
using Abp.Zero.Configuration;
using PearAdmin.Abp.Authorization;
using PearAdmin.Abp.Authorization.Roles;
using PearAdmin.Abp.Authorization.Users;
using PearAdmin.Abp.Social.Chat;
using PearAdmin.Abp.Configuration;
using PearAdmin.Abp.Features;
using PearAdmin.Abp.Social.Friendships;
using PearAdmin.Abp.Localization;
using PearAdmin.Abp.MultiTenancy;
using PearAdmin.Abp.Notifications;
using PearAdmin.Abp.Timing;

namespace PearAdmin.Abp
{
    [DependsOn(
        typeof(AbpZeroCoreModule),
        typeof(AbpAutoMapperModule)
        )]
    public class AbpCoreModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Auditing.IsEnabledForAnonymousUsers = true;

            // Declare entity types
            Configuration.Modules.Zero().EntityTypes.Tenant = typeof(Tenant);
            Configuration.Modules.Zero().EntityTypes.Role = typeof(Role);
            Configuration.Modules.Zero().EntityTypes.User = typeof(User);

            AbpLocalizationConfigurer.Configure(Configuration.Localization);

            // Enable this line to create a multi-tenant application.
            Configuration.MultiTenancy.IsEnabled = AbpCoreConsts.MultiTenancyEnabled;

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
            IocManager.RegisterAssemblyByConvention(typeof(AbpCoreModule).GetAssembly());
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
