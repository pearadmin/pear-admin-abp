using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using PearAdmin.Abp.EntityFrameworkCore;
using PearAdmin.Abp.Admin.ViewResources;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace PearAdmin.Abp.Admin.Tests
{
    [DependsOn(
        typeof(AbpAdminModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class AbpWebTestModule : AbpModule
    {
        public AbpWebTestModule(AbpEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(AbpAdminModule).Assembly);
        }
    }
}