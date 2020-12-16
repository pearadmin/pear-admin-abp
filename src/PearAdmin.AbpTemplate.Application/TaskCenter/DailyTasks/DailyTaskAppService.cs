using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using PearAdmin.AbpTemplate.Notifications;
using PearAdmin.AbpTemplate.TaskCenter.DailyTasks.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.TaskCenter.DailyTasks
{
    public class DailyTaskAppService : AbpTemplateApplicationServiceBase, IDailyTaskAppService
    {
        private readonly IRepository<DailyTask, Guid> _dailyTaskRepository;
        private readonly IAppNotifier _appNotifier;

        public DailyTaskAppService(
            IRepository<DailyTask, Guid> dailyTaskRepository,
            IAppNotifier appNotifier)
        {
            _dailyTaskRepository = dailyTaskRepository;
            _appNotifier = appNotifier;
        }

        public async Task CreateDailyTask(CreateDailyTaskDto input)
        {
            var dailyTask = DailyTask.Create(input.Name)
                .SetRemark(input.Remark)
                .SetDateRange(input.StartTime, input.EndTime);

            await _dailyTaskRepository.InsertAsync(dailyTask);
            await _appNotifier.NewDailyTaskAsync();
        }

        public async Task UpdateDailyTask(UpdateDailyTaskDto input)
        {
            var dailyTask = await _dailyTaskRepository.GetAsync(input.Id);
            dailyTask.SetName(input.Name)
                .SetRemark(input.Remark)
                .SetDateRange(input.StartTime, input.EndTime);

            await _dailyTaskRepository.UpdateAsync(dailyTask);
        }

        public async Task DeleteDailyTask(List<EntityDto<Guid>> inputs)
        {
            foreach (var input in inputs)
            {
                await _dailyTaskRepository.DeleteAsync(input.Id);
            }
        }

        public async Task<PagedResultDto<DailyTaskDto>> GetPagedDailyTask(GetPagedDailyTaskInput input)
        {
            var query = _dailyTaskRepository.GetAll()
                 .WhereIf(!input.FilterText.IsNullOrWhiteSpace(), t => t.Name.Contains(input.FilterText) || t.Remark.Contains(input.FilterText))
                 .WhereIf(input.StartTime.HasValue, t => t.DateRange.StartTime > input.StartTime.Value)
                 .WhereIf(input.EndTime.HasValue, t => t.DateRange.EndTime < input.EndTime.Value);

            var totalCount = await query.CountAsync();
            var items = await query.PageBy(input).ToListAsync();

            return new PagedResultDto<DailyTaskDto>(totalCount,
                items.Select(item =>
                {
                    var dto = ObjectMapper.Map<DailyTaskDto>(item);
                    dto.Triggers = item.GetPermittedTriggers().ToList();
                    return dto;
                })
                .ToList());
        }

        public async Task<DailyTaskDto> GetDailyTaskForEdit(NullableIdDto<Guid> input)
        {
            if (input.Id.HasValue && input.Id.Value != Guid.Empty)
            {
                var dailyTask = await _dailyTaskRepository.GetAsync(input.Id.Value);
                var dailyTaskDto = ObjectMapper.Map<DailyTaskDto>(dailyTask);
                return dailyTaskDto;
            }
            else
            {
                return new DailyTaskDto();
            }
        }

        public async Task ProgressDailyTask(EntityDto<Guid> input)
        {
            var dailyTask = await _dailyTaskRepository.GetAsync(input.Id);
            dailyTask.Progress();
            await _dailyTaskRepository.UpdateAsync(dailyTask);
        }

        public async Task ReopenDailyTask(EntityDto<Guid> input)
        {
            var dailyTask = await _dailyTaskRepository.GetAsync(input.Id);
            dailyTask.Reopen();
            await _dailyTaskRepository.UpdateAsync(dailyTask);
        }

        public async Task ResolveDailyTask(EntityDto<Guid> input)
        {
            var dailyTask = await _dailyTaskRepository.GetAsync(input.Id);
            dailyTask.Resolve();
            await _dailyTaskRepository.UpdateAsync(dailyTask);
        }

        public async Task QualifyDailyTask(EntityDto<Guid> input)
        {
            var dailyTask = await _dailyTaskRepository.GetAsync(input.Id);
            dailyTask.Qualify();
            await _dailyTaskRepository.UpdateAsync(dailyTask);
        }

        public async Task PendDailyTask(EntityDto<Guid> input)
        {
            var dailyTask = await _dailyTaskRepository.GetAsync(input.Id);
            dailyTask.Pend();
            await _dailyTaskRepository.UpdateAsync(dailyTask);
        }

        public async Task CloseDailyTask(EntityDto<Guid> input)
        {
            var dailyTask = await _dailyTaskRepository.GetAsync(input.Id);
            dailyTask.Close();
            await _dailyTaskRepository.UpdateAsync(dailyTask);
        }
    }
}
