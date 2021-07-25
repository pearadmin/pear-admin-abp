using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PearAdmin.AbpTemplate.BinaryObjects;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore.EntityTypeConfigurations
{
    public class BinaryObjectEntityTypeConfiguration : IEntityTypeConfiguration<BinaryObject>
    {
        public void Configure(EntityTypeBuilder<BinaryObject> builder)
        {
            builder.ToTable($"{AbpTemplateCoreConsts.TablePrefix_Common}_BinaryObject");

            builder.Property(a => a.Bytes)
                .IsRequired();
        }
    }
}
