using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using PearAdmin.Abp.TaskCenter.DailyTasks;
using PearAdmin.Abp.TaskCenter.DailyTasks.Dto;
using PearAdmin.Abp.Admin.Areas.TaskCenter.Models.DailyTasks;
using PearAdmin.Abp.Admin.Controllers;
using PearAdmin.Abp.Admin.Models.Common;

namespace PearAdmin.Abp.Admin.Areas.TaskCenter.Controllers
{
    /// <summary>
    /// 日常任务控制器
    /// </summary>
    [Area("TaskCenter")]
    [AbpMvcAuthorize]
    public class DailyTaskController : AbpControllerBase
    {
        private readonly IDailyTaskAppService _dailyTaskAppService;

        public DailyTaskController(IDailyTaskAppService dailyTaskAppService)
        {
            _dailyTaskAppService = dailyTaskAppService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取日常任务列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetDailyTaskList(GetPagedDailyTaskViewModel viewModel)
        {
            var input = PagedViewModelMapToPagedInputDto<GetPagedDailyTaskViewModel, GetPagedDailyTaskInput>(viewModel);
            var pagedDailyTaskList = await _dailyTaskAppService.GetPagedDailyTask(input);
            return Json(new PagedResultViewModel<DailyTaskDto>(pagedDailyTaskList.TotalCount, pagedDailyTaskList.Items));
        }

        /// <summary>
        /// 创建或更新日常任务
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateOrUpdateDailyTask(NullableIdDto<Guid> input)
        {
            var output = await _dailyTaskAppService.GetDailyTaskForEdit(input);

            return View(output);
        }

        /// <summary>
        /// 创建日常任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreateDailyTask([FromBody]CreateDailyTaskDto input)
        {
            await _dailyTaskAppService.CreateDailyTask(input);

            return Json(new ResponseParamViewModel(L("CreateDailyTaskSuccessful")));
        }

        /// <summary>
        /// 更新日常任务
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UpdateDailyTask([FromBody]UpdateDailyTaskDto input)
        {
            await _dailyTaskAppService.UpdateDailyTask(input);

            return Json(new ResponseParamViewModel(L("UpdateDailyTaskSuccessful")));
        }

        /// <summary>
        /// 删除日常任务
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> DeleteDailyTask([FromBody]List<EntityDto<Guid>> input)
        {
            await _dailyTaskAppService.DeleteDailyTask(input);

            return Json(new ResponseParamViewModel(L("DeleteDailyTaskSuccessful")));
        }
    }
}