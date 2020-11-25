using Abp.MultiTenancy;
using PearAdmin.Abp.Authorization.Users;

namespace PearAdmin.Abp.MultiTenancy
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
