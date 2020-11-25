using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using PearAdmin.AbpTemplate.Authorization.Roles;
using PearAdmin.AbpTemplate.Authorization.Users;
using PearAdmin.AbpTemplate.MultiTenancy;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore
{
    public class AbpTemplateDbContext : AbpZeroDbContext<Tenant, Role, User, AbpTemplateDbContext>
    {
        public virtual DbSet<Storage.BinaryObject> BinaryObject { get; set; }
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
            #region DailyTask
            modelBuilder.Entity<TaskCenter.DailyTasks.DailyTask>()
                .OwnsOne(a => a.TaskState, vo =>
                {
                    vo.Property(p => p.TaskStateType).HasColumnName("TaskStateType");
                    vo.Ignore(p => p.TaskStateTypeName);
                });

            modelBuilder.Entity<TaskCenter.DailyTasks.DailyTask>()
                .OwnsOne(a => a.DateRange, vo =>
                {
                    vo.Property(p => p.StartTime)
                        .HasColumnName("StartTime")
                        .HasColumnType("datetime(6)");

                    vo.Property(p => p.EndTime)
                        .HasColumnName("EndTime")
                        .HasColumnType("datetime(6)");
                });
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
