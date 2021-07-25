using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PearAdmin.AbpTemplate.Social.Chat;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore.EntityTypeConfigurations
{
    public class ChatMessageEntityTypeConfiguration : IEntityTypeConfiguration<ChatMessage>
    {
        public void Configure(EntityTypeBuilder<ChatMessage> builder)
        {
            builder.ToTable($"{AbpTemplateCoreConsts.TablePrefix_Social}_ChatMessage");

            builder.Property(a => a.Message)
                .HasMaxLength(ChatMessage.MaxMessageLength)
                .IsRequired();
        }
    }
}
