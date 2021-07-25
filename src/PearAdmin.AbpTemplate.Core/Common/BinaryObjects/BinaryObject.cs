using System;
using Abp;
using Abp.Domain.Entities;

namespace PearAdmin.AbpTemplate.BinaryObjects
{
    public class BinaryObject : AggregateRoot<Guid>, IMayHaveTenant
    {
        public virtual int? TenantId { get; set; }
        public virtual byte[] Bytes { get; set; }

        public BinaryObject()
        {
            Id = SequentialGuidGenerator.Instance.Create();
        }

        public BinaryObject(int? tenantId, byte[] bytes)
            : this()
        {
            TenantId = tenantId;
            Bytes = bytes;
        }
    }
}
