using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace PearAdmin.AbpTemplate.TaskCenter.DailyTasks.Dto
{
    public class UpdateDailyTaskDto : EntityDto<Guid>
    {
        [Required]
        [StringLength(DailyTask.MaxNameLength)]
        public string Name { get; set; }

        [StringLength(DailyTask.MaxRemarkLength)]
        public string Remark { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
    }
}
