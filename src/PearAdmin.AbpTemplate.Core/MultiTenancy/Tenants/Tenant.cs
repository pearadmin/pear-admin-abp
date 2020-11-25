using Abp.MultiTenancy;
using PearAdmin.AbpTemplate.Authorization.Users;

namespace PearAdmin.AbpTemplate.MultiTenancy
{
    public class Tenant : AbpTenant<User>
    {
        public Tenant()
        {            
        }

        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}
