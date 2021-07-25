using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PearAdmin.AbpTemplate.Social.Friendships;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore.EntityTypeConfigurations
{
    public class FriendshipEntityTypeConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.ToTable($"{AbpTemplateCoreConsts.TablePrefix_Social}_Friendship");

            builder.Property(a => a.FriendUserName)
                .HasMaxLength(Friendship.MaxUserNameLength)
                .IsRequired();
        }
    }
}
