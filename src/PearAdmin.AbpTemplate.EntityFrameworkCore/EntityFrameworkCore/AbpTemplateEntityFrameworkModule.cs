using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using PearAdmin.AbpTemplate.EntityFrameworkCore.Seed;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore
{
    [DependsOn(
        typeof(AbpTemplateCoreModule),
        typeof(AbpZeroCoreEntityFrameworkCoreModule))]
    public class AbpTemplateEntityFrameworkModule : AbpModule
    {
        // 集成测试中使用，跳过DbContext注册和基础数据初始化
        // 集成测试时使用内存数据库
        public bool SkipDbContextRegistration { get; set; }
        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (SkipDbContextRegistration)
            {
                return;
            }

            Configuration.Modules.AbpEfCore().AddDbContext<AbpTemplateDbContext>(options =>
            {
                if (options.ExistingConnection != null)
                {
                    AbpTemplateDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                }
                else
                {
                    AbpTemplateDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                }
            });
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpTemplateEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (SkipDbSeed)
            {
                return;
            }

            SeedHelper.SeedHostDb(IocManager);
        }
    }
}
