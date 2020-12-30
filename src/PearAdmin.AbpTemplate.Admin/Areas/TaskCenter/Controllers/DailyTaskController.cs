using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using PearAdmin.AbpTemplate.TaskCenter.DailyTasks;
using PearAdmin.AbpTemplate.TaskCenter.DailyTasks.Dto;
using PearAdmin.AbpTemplate.Admin.Areas.TaskCenter.Models.DailyTasks;
using PearAdmin.AbpTemplate.Admin.Controllers;
using PearAdmin.AbpTemplate.Admin.Models.Common;
using Abp.Web.Models;

namespace PearAdmin.AbpTemplate.Admin.Areas.TaskCenter.Controllers
{
    /// <summary>
    /// 日常任务控制器
    /// </summary>
    [Area("TaskCenter")]
    [AbpMvcAuthorize]
    public class DailyTaskController : AbpTemplateControllerBase
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
            return Json(new ResponseParamPagedViewModel<DailyTaskDto>(pagedDailyTaskList.TotalCount, pagedDailyTaskList.Items));
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

        [HttpPost]
        public async Task<JsonResult> ProgressDailyTask([FromBody]EntityDto<Guid> input)
        {
            await _dailyTaskAppService.ProgressDailyTask(input);

            return Json(new ResponseParamViewModel(L("ProgressDailyTaskSuccessful")));
        }

        [HttpPost]
        public async Task<JsonResult> ResolveDailyTask([FromBody]EntityDto<Guid> input)
        {
            await _dailyTaskAppService.ResolveDailyTask(input);

            return Json(new ResponseParamViewModel(L("ResolveDailyTaskSuccessful")));
        }

        [HttpPost]
        public async Task<JsonResult> ReopenDailyTask([FromBody]EntityDto<Guid> input)
        {
            await _dailyTaskAppService.ReopenDailyTask(input);

            return Json(new ResponseParamViewModel(L("ReopenDailyTaskSuccessful")));
        }

        [HttpPost]
        public async Task<JsonResult> QualifyDailyTask([FromBody]EntityDto<Guid> input)
        {
            await _dailyTaskAppService.QualifyDailyTask(input);

            return Json(new ResponseParamViewModel(L("QualifyDailyTaskSuccessful")));
        }

        [HttpPost]
        public async Task<JsonResult> PendDailyTask([FromBody]EntityDto<Guid> input)
        {
            await _dailyTaskAppService.PendDailyTask(input);

            return Json(new ResponseParamViewModel(L("PendDailyTaskSuccessful")));
        }

        [HttpPost]
        public async Task<JsonResult> CloseDailyTask([FromBody]EntityDto<Guid> input)
        {
            await _dailyTaskAppService.CloseDailyTask(input);

            return Json(new ResponseParamViewModel(L("CloseDailyTaskSuccessful")));
        }
    }
}