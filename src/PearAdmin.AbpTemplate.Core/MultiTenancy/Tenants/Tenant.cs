using Abp;
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

        public Tenant SetEditionId(int editionId)
        {
            if (editionId <= 0)
            {
                throw new AbpException();
            }

            EditionId = editionId;

            return this;
        }
    }
}
