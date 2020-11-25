using PearAdmin.Abp.TaskCenter.DailyTasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace PearAdmin.Abp.TaskCenter.DailyTasks.Dto
{
    public class CreateDailyTaskDto
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
