using Abp.Zero.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PearAdmin.AbpTemplate.Authorization.Roles;
using PearAdmin.AbpTemplate.Authorization.Users;
using PearAdmin.AbpTemplate.EntityFrameworkCore.EntityTypeConfigurations;
using PearAdmin.AbpTemplate.MultiTenancy;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore
{
    public class AbpTemplateDbContext : AbpZeroDbContext<Tenant, Role, User, AbpTemplateDbContext>
    {
        public virtual DbSet<BinaryObjects.BinaryObject> BinaryObject { get; set; }
        public virtual DbSet<Social.Chat.ChatMessage> ChatMessage { get; set; }
        public virtual DbSet<Social.Friendships.Friendship> Friendship { get; set; }
        public virtual DbSet<Resource.DataDictionaries.DataDictionaryItem> DataDictionaryItem { get; set; }
        public virtual DbSet<TaskCenter.DailyTasks.DailyTask> DailyTask { get; set; }

        public AbpTemplateDbContext(DbContextOptions<AbpTemplateDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new DailyTaskEntityTypeConfiguration().Configure(modelBuilder.Entity<TaskCenter.DailyTasks.DailyTask>());
            new DataDictionaryItemEntityTypeConfiguration().Configure(modelBuilder.Entity<Resource.DataDictionaries.DataDictionaryItem>());
            new FriendshipEntityTypeConfiguration().Configure(modelBuilder.Entity<Social.Friendships.Friendship>());
            new ChatMessageEntityTypeConfiguration().Configure(modelBuilder.Entity<Social.Chat.ChatMessage>());
            new BinaryObjectEntityTypeConfiguration().Configure(modelBuilder.Entity<BinaryObjects.BinaryObject>());

            base.OnModelCreating(modelBuilder);
        }
    }
}
