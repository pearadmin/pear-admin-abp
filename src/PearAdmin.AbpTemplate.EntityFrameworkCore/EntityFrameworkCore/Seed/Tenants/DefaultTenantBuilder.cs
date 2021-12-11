using Abp.MultiTenancy;
using Microsoft.EntityFrameworkCore;
using PearAdmin.AbpTemplate.Editions;
using PearAdmin.AbpTemplate.MultiTenancy;
using System.Linq;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore.Seed.Tenants
{
    public class DefaultTenantBuilder
    {
        private readonly AbpTemplateDbContext _context;

        public DefaultTenantBuilder(AbpTemplateDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultTenant();
        }

        private void CreateDefaultTenant()
        {
            var existedDefaultTenant = _context.Tenants.IgnoreQueryFilters().Any(t => t.TenancyName == AbpTenantBase.DefaultTenantName);
            if (existedDefaultTenant)
            {
                return;
            }

            // 创建默认租户(1)
            var defaultTenant = new Tenant(AbpTenantBase.DefaultTenantName, AbpTenantBase.DefaultTenantName);
            var defaultEdition = _context.Editions.IgnoreQueryFilters().FirstOrDefault(e => e.Name == EditionManager.DefaultEditionName);
            if (defaultEdition != null)
            {
                defaultTenant.SetEditionId(defaultEdition.Id);
            }

            _context.Tenants.Add(defaultTenant);
            _context.SaveChanges();
        }
    }
}
