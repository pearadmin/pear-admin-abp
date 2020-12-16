using PearAdmin.AbpTemplate.Shared;

namespace PearAdmin.AbpTemplate.TaskCenter.DailyTasks
{
    /// <summary>
    /// 任务状态类型
    /// </summary>
    public class TaskStateType : Enumeration
    {
        public static TaskStateType ToDo = new TaskStateType(0, nameof(ToDo));
        public static TaskStateType Progressing = new TaskStateType(3, nameof(Progressing));
        public static TaskStateType Resolved = new TaskStateType(6, nameof(Resolved));
        public static TaskStateType Done = new TaskStateType(9, nameof(Done));
        public static TaskStateType Reopen = new TaskStateType(12, nameof(Reopen));
        public static TaskStateType Pending = new TaskStateType(15, nameof(Pending));
        public static TaskStateType Close = new TaskStateType(18, nameof(Close));

        private TaskStateType() : base()
        {

        }

        public TaskStateType(int id, string name) : base(id, name)
        {

        }
    }

    /// <summary>
    /// 任务操作
    /// </summary>
    public class TaskOperateTrigger : Enumeration
    {
        public static TaskOperateTrigger Progress = new TaskOperateTrigger(0, nameof(Progress));
        public static TaskOperateTrigger Resolve = new TaskOperateTrigger(3, nameof(Resolve));
        public static TaskOperateTrigger Reopen = new TaskOperateTrigger(6, nameof(Reopen));
        public static TaskOperateTrigger Qualify = new TaskOperateTrigger(9, nameof(Qualify));
        public static TaskOperateTrigger Pend = new TaskOperateTrigger(12, nameof(Pend));
        public static TaskOperateTrigger Close = new TaskOperateTrigger(15, nameof(Close));

        private TaskOperateTrigger() : base()
        {

        }

        public TaskOperateTrigger(int id, string name) : base(id, name)
        {

        }
    }
}
