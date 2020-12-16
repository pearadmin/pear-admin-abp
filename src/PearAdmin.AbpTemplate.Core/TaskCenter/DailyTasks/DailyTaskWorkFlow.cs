using System.Collections.Generic;
using Stateless;
using Stateless.Graph;

namespace PearAdmin.AbpTemplate.TaskCenter.DailyTasks
{
    public partial class DailyTask
    {
        private StateMachine<TaskStateType, TaskOperateTrigger> _stateMachine;

        /// <summary>
        /// 流程配置
        /// </summary>
        private void StateMachineConfigure()
        {
            _stateMachine = new StateMachine<TaskStateType, TaskOperateTrigger>(() => TaskState, s => TaskState = s);

            _stateMachine.Configure(TaskStateType.ToDo)
                .OnEntry(() => ToDotGraph())
                .Permit(TaskOperateTrigger.Progress, TaskStateType.Progressing)
                .Permit(TaskOperateTrigger.Pend, TaskStateType.Pending)
                .Permit(TaskOperateTrigger.Close, TaskStateType.Close);

            _stateMachine.Configure(TaskStateType.Progressing)
                .Permit(TaskOperateTrigger.Resolve, TaskStateType.Resolved)
                .Permit(TaskOperateTrigger.Pend, TaskStateType.Pending)
                .Permit(TaskOperateTrigger.Close, TaskStateType.Close);

            _stateMachine.Configure(TaskStateType.Resolved)
                .Permit(TaskOperateTrigger.Reopen, TaskStateType.Reopen)
                .Permit(TaskOperateTrigger.Qualify, TaskStateType.Done);

            _stateMachine.Configure(TaskStateType.Reopen)
                .Permit(TaskOperateTrigger.Pend, TaskStateType.Pending)
                .Permit(TaskOperateTrigger.Close, TaskStateType.Close);

            _stateMachine.Configure(TaskStateType.Pending)
                .Permit(TaskOperateTrigger.Close, TaskStateType.Close);
        }

        public IEnumerable<TaskOperateTrigger> GetPermittedTriggers()
        {
            var triggers = _stateMachine.GetPermittedTriggers();
            return triggers;
        }

        public string ToDotGraph()
        {
            return UmlDotGraph.Format(_stateMachine.GetInfo());
        }
    }
}
