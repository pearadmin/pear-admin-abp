using Abp.AutoMapper;
using PearAdmin.AbpTemplate.TaskCenter.DailyTasks.Dto;
using PearAdmin.AbpTemplate.Admin.Models.Common;

namespace PearAdmin.AbpTemplate.Admin.Areas.TaskCenter.Models.DailyTasks
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
