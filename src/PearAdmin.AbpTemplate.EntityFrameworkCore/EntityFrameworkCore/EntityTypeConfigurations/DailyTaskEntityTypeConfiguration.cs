using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PearAdmin.AbpTemplate.TaskCenter.DailyTasks;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore.EntityTypeConfigurations
{
    public class DailyTaskEntityTypeConfiguration : IEntityTypeConfiguration<DailyTask>
    {
        public void Configure(EntityTypeBuilder<DailyTask> builder)
        {
            builder.ToTable($"{AbpTemplateCoreConsts.TablePrefix_TaskCenter}_DailyTask");

            builder.OwnsOne(a => a.TaskState, vo =>
            {
                vo.Property(p => p.Id).HasColumnName("TaskStateType");
                vo.Ignore(p => p.Name);
            });

            builder.OwnsOne(a => a.DateRange, vo =>
            {
                vo.Property(p => p.StartTime)
                    .HasColumnName("StartTime")
                    .HasColumnType("datetime(6)");

                vo.Property(p => p.EndTime)
                    .HasColumnName("EndTime")
                    .HasColumnType("datetime(6)");
            });

            builder.Property(a => a.Name)
                .HasMaxLength(DailyTask.MaxNameLength)
                .IsRequired();

            builder.Property(a => a.Remark)
                .HasMaxLength(DailyTask.MaxRemarkLength);
        }
    }
}
