using Abp.AutoMapper;
using PearAdmin.Abp.TaskCenter.DailyTasks.Dto;
using PearAdmin.Abp.Admin.Models.Common;

namespace PearAdmin.Abp.Admin.Areas.TaskCenter.Models.DailyTasks
{
    /// <summary>
    /// 日常任务分页模型
    /// </summary>
    [AutoMapTo(typeof(GetPagedDailyTaskInput))]
    public class GetPagedDailyTaskViewModel : PagedViewModel
    {
        public string FilterText { get; set; }
    }
}
