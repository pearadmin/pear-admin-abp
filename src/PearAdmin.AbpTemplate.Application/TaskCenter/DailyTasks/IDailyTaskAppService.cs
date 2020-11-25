using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PearAdmin.AbpTemplate.TaskCenter.DailyTasks.Dto;

namespace PearAdmin.AbpTemplate.TaskCenter.DailyTasks
{
    /// <summary>
    /// 日常任务应用服务
    /// </summary>
    public interface IDailyTaskAppService : IApplicationService
    {
        /// <summary>
        /// 获取日常任务列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<DailyTaskDto> GetDailyTaskForEdit(NullableIdDto<Guid> input);

        /// <summary>
        /// 分页获取日常任务列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<DailyTaskDto>> GetPagedDailyTask(GetPagedDailyTaskInput input);

        /// <summary>
        /// 创建日常任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateDailyTask(CreateDailyTaskDto input);

        /// <summary>
        /// 更新日常任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateDailyTask(UpdateDailyTaskDto input);

        /// <summary>
        /// 删除日常任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteDailyTask(List<EntityDto<Guid>> inputs);

        /// <summary>
        /// 重新打开日常任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ReopenDailyTask(EntityDto<Guid> input);
    }
}
