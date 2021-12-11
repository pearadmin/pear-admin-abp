using Abp.MultiTenancy;
using PearAdmin.AbpTemplate.EntityFrameworkCore.Seed.Common;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore.Seed.Tenants
{
    public class InitialTenantDbBuilder
    {
        private readonly AbpTemplateDbContext _context;

        public InitialTenantDbBuilder(AbpTemplateDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            //默认租户初始化(Id为1)
            DefaultTenant();

            _context.SaveChanges();
        }

        private void DefaultTenant()
        {
            new DefaultTenantBuilder(_context).Create();
            new TenantRoleAndUserBuilder(_context, MultiTenancyConsts.DefaultTenantId).Create();
            new DefaultLanguagesCreator(_context, MultiTenancyConsts.DefaultTenantId).Create();
            new DefaultSettingsCreator(_context, MultiTenancyConsts.DefaultTenantId).Create();
            new TenantSettingsCreator(_context, MultiTenancyConsts.DefaultTenantId).Create();
        }
    }
}
