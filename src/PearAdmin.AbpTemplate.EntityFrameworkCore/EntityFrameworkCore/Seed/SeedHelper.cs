using System;
using System.Transactions;
using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Uow;
using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using PearAdmin.AbpTemplate.EntityFrameworkCore.Seed.Host;
using PearAdmin.AbpTemplate.EntityFrameworkCore.Seed.Tenants;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore.Seed
{
    public static class SeedHelper
    {
        public static void SeedHostDb(IIocResolver iocResolver)
        {
            WithDbContext<AbpTemplateDbContext>(iocResolver, SeedHostDb);
        }

        public static void SeedHostDb(AbpTemplateDbContext context)
        {
            context.SuppressAutoSetTenantId = true;

            // 宿主基础数据初始化
            new InitialHostDbBuilder(context).Create();
            new InitialTenantDbBuilder(context).Create();
        }

        private static void WithDbContext<TDbContext>(IIocResolver iocResolver, Action<TDbContext> contextAction)
            where TDbContext : DbContext
        {
            using (var uowManager = iocResolver.ResolveAsDisposable<IUnitOfWorkManager>())
            {
                using (var uow = uowManager.Object.Begin(TransactionScopeOption.Suppress))
                {
                    var context = uowManager.Object.Current.GetDbContext<TDbContext>(MultiTenancySides.Host);

                    contextAction(context);

                    uow.Complete();
                }
            }
        }
    }
}
