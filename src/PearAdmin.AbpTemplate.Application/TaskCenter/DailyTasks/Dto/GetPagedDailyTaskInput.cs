using PearAdmin.AbpTemplate.CommonDto;
using System;

namespace PearAdmin.AbpTemplate.TaskCenter.DailyTasks.Dto
{
    /// <summary>
    /// 分页、筛选请求获取日常任务Dto
    /// </summary>
    public class GetPagedDailyTaskInput : PagedAndFilteredInputDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
