using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using PearAdmin.Abp.Authorization.Roles;
using PearAdmin.Abp.Authorization.Users;
using PearAdmin.Abp.MultiTenancy;

namespace PearAdmin.Abp.EntityFrameworkCore
{
    public class AbpDbContext : AbpZeroDbContext<Tenant, Role, User, AbpDbContext>
    {
        public virtual DbSet<Storage.BinaryObject> BinaryObject { get; set; }
        public virtual DbSet<Social.Chat.ChatMessage> ChatMessage { get; set; }
        public virtual DbSet<Social.Friendships.Friendship> Friendship { get; set; }
        public virtual DbSet<Resource.DataDictionaries.DataDictionaryItem> DataDictionaryItem { get; set; }
        public virtual DbSet<TaskCenter.DailyTasks.DailyTask> DailyTask { get; set; }

        public AbpDbContext(DbContextOptions<AbpDbContext> options)
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
