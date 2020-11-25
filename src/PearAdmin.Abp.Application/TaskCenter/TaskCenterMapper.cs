using AutoMapper;
using PearAdmin.Abp.TaskCenter.DailyTasks;
using PearAdmin.Abp.TaskCenter.DailyTasks.Dto;

namespace PearAdmin.Abp.TaskCenter
{
    internal static class TaskCenterMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<DailyTask, DailyTaskDto>()
                .ForMember(d => d.StartTime, options => options.MapFrom(t => t.DateRange.StartTime))
                .ForMember(d => d.EndTime, options => options.MapFrom(t => t.DateRange.EndTime))
                .ForMember(d => d.TaskStateTypeName, options => options.MapFrom(t => t.TaskState.TaskStateTypeName));
        }
    }
}
