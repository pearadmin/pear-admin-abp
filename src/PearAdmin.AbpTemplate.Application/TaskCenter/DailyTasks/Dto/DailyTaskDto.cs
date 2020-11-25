using Abp.Application.Services.Dto;
using System;

namespace PearAdmin.AbpTemplate.TaskCenter.DailyTasks.Dto
{
    public class DailyTaskDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public string Remark { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public DateTime EndTime { get; set; } = DateTime.Now.AddDays(1);
        public string TaskStateTypeName { get; set; }
    }
}
