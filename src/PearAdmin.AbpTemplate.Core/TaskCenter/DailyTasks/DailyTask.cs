using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PearAdmin.AbpTemplate.TaskCenter.DailyTasks.ValueObjects;

namespace PearAdmin.AbpTemplate.TaskCenter.DailyTasks
{
    /// <summary>
    /// 中心_日常任务
    /// </summary>
    public partial class DailyTask : FullAuditedAggregateRoot<Guid>
    {
        public const int MaxNameLength = 200;
        public const int MaxRemarkLength = 1000;

        private DailyTask()
        {
            StateMachineConfigure();
        }

        private DailyTask(string name) : this()
        {
            TaskState = TaskStateType.ToDo;
            SetName(name);
        }

        public static DailyTask Create(string name)
        {
            return new DailyTask(name);
        }

        [StringLength(MaxNameLength)]
        public string Name { get; private set; }

        [StringLength(MaxRemarkLength)]
        public string Remark { get; private set; }

        public DateRange DateRange { get; private set; }
        public TaskStateType TaskState { get; private set; }

        public DailyTask SetName(string name)
        {
            Name = name;
            return this;
        }

        public DailyTask SetRemark(string remark)
        {
            Remark = remark;
            return this;
        }

        public DailyTask SetDateRange(DateTime startTime, DateTime endTime)
        {
            DateRange = new DateRange(startTime, endTime);
            return this;
        }

        public DailyTask ChangeStartTime(DateTime startTime)
        {
            DateRange = DateRange.ChangeStartTime(startTime);
            return this;
        }

        public DailyTask Progress()
        {
            _stateMachine.Fire(TaskOperateTrigger.Progress);
            return this;
        }

        public DailyTask Resolve()
        {
            _stateMachine.Fire(TaskOperateTrigger.Resolve);
            return this;
        }

        public DailyTask Reopen()
        {
            _stateMachine.Fire(TaskOperateTrigger.Reopen);
            return this;
        }

        public DailyTask Qualify()
        {
            _stateMachine.Fire(TaskOperateTrigger.Qualify);
            return this;
        }

        public DailyTask Pend()
        {
            _stateMachine.Fire(TaskOperateTrigger.Pend);
            return this;
        }

        public DailyTask Close()
        {
            _stateMachine.Fire(TaskOperateTrigger.Close);
            return this;
        }
    }
}
