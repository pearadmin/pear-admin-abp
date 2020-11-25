using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PearAdmin.AbpTemplate.EntityFrameworkCore;
using PearAdmin.AbpTemplate.Admin.ViewResources;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace PearAdmin.AbpTemplate.Admin.Tests
{
    [DependsOn(
        typeof(AbpTemplateAdminModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class AbpTemplateWebTestModule : AbpModule
    {
        public AbpTemplateWebTestModule(AbpTemplateEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpTemplateWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(AbpTemplateAdminModule).Assembly);
        }
    }
}