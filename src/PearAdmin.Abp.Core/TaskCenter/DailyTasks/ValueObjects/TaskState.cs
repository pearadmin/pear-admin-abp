using Abp.Domain.Values;
using System;
using System.Collections.Generic;

namespace PearAdmin.Abp.TaskCenter.DailyTasks.ValueObjects
{
    /// <summary>
    /// 任务状态
    /// </summary>
    public class TaskState : ValueObject
    {
        private TaskState()
        {

        }

        public TaskState(TaskStateType taskStateType = TaskStateType.ToDo) : this()
        {
            TaskStateType = taskStateType;
        }

        public TaskStateType TaskStateType { get; private set; }

        public string TaskStateTypeName
        {
            get => Enum.GetName(typeof(TaskStateType), TaskStateType);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return TaskStateType;
        }
    }
}
