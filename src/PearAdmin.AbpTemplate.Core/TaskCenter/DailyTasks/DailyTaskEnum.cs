namespace PearAdmin.AbpTemplate.TaskCenter.DailyTasks
{
    /// <summary>
    /// 任务状态类型
    /// </summary>
    public enum TaskStateType
    {
        //待定
        ToDo = 0,

        //进行中
        Progressing = 3,

        //已解决
        Resolved = 6,

        //已完成
        Done = 9,

        //重新打开
        Reopen = 12,

        //搁置
        Pending = 15,

        //关闭
        Close = 18
    }

    /// <summary>
    /// 任务操作
    /// </summary>
    public enum TaskOperateTrigger
    {
        //执行
        Progress = 0,

        //解决
        Resolve = 3,

        //未解决
        Reopen = 6,

        //合格
        Qualify = 9,

        //搁置
        Pend = 12,

        //关闭
        Close = 15,
    }
}
