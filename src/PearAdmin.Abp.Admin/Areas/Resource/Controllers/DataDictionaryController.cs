using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Mvc;
using PearAdmin.Abp.Resource.DataDictionaries;
using PearAdmin.Abp.Resource.DataDictionaries.Dto;
using PearAdmin.Abp.Admin.Controllers;
using PearAdmin.Abp.Admin.Models.Common;
using PearAdmin.Abp.Admin.Areas.Resource.Models.DataDictionaries;

namespace PearAdmin.Abp.Admin.Areas.Resource.Controllers
{
    /// <summary>
    /// 数据字典控制器
    /// </summary>
    [Area("Resource")]
    [AbpMvcAuthorize]
    public class DataDictionaryController : AbpControllerBase
    {
        private readonly IDataDictionaryAppService _dataDictionaryAppService;

        public DataDictionaryController(IDataDictionaryAppService dataDictionaryAppService)
        {
            _dataDictionaryAppService = dataDictionaryAppService;
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
        /// 获取数据字典列表
        /// </summary>
        /// <returns></returns>
        [DontWrapResult]
        public JsonResult GetDataDictionaryList()
        {
            var dataDictionaryDtos = _dataDictionaryAppService.GetAllDataDictionary();
            return Json(new ResponseParamListViewModel<DataDictionaryDto>(dataDictionaryDtos.Items));
        }

        /// <summary>
        /// 获取数据字典项列表
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetDataDictionaryItemList(DataDictionaryItemRequestViewModel viewModel)
        {
            var dataDictionaryItemDtos = await _dataDictionaryAppService.GetAllDataDictionaryItem(new GetAllDataDictionaryItemInput()
            {
                DataDictionaryId = viewModel.DataDictionaryId,
                FilterText = viewModel.FilterText
            });

            return Json(new ResponseParamListViewModel<DataDictionaryItemDto>(dataDictionaryItemDtos.Items));
        }

        /// <summary>
        /// 根据字典类型获取字典项
        /// </summary>
        /// <param name="type">字典类型</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetDataDictionaryItemListByTypeNames([FromBody]GetDataDictionaryListByTypeNamesInput input)
        {
            var dataDictionaryItemDtos = await _dataDictionaryAppService.GetDataDictionaryListByTypeNames(input);
            return Json(new ResponseParamListViewModel<DataDictionaryDto>(dataDictionaryItemDtos.Items));
        }

        /// <summary>
        /// 创建或更新数据字典项
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateOrUpdateDataDictionaryItem(DataDictionaryItemRequestViewModel viewModel)
        {
            if (!viewModel.DataDictionaryId.HasValue && !viewModel.Id.HasValue)
            {
                throw new UserFriendlyException(L("请求参数有误"));
            }

            var output = await _dataDictionaryAppService.GetDataDictionaryItemForEdit(new NullableIdDto<int>(viewModel.Id));

            if (!viewModel.Id.HasValue)
            {
                output.DataDictionaryId = viewModel.DataDictionaryId.Value;
            }

            return View(output);
        }

        /// <summary>
        /// 创建数据字典项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CreateDataDictionaryItem([FromBody]CreateDataDictionaryItemDto input)
        {
            await _dataDictionaryAppService.CreateDataDictionaryItem(input);

            return Json(new ResponseParamViewModel(L("CreateDataDictionaryItemSuccessful")));
        }

        /// <summary>
        /// 更新数据字典项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> UpdateDataDictionaryItem([FromBody]UpdateDataDictionaryItemDto input)
        {
            await _dataDictionaryAppService.UpdateDataDictionaryItem(input);

            return Json(new ResponseParamViewModel(L("UpdateDataDictionaryItemSuccessful")));
        }

        /// <summary>
        /// 删除数据字典项
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> DeleteDataDictionaryItem([FromBody]List<EntityDto<int>> input)
        {
            await _dataDictionaryAppService.DeleteDataDictionaryItem(input);

            return Json(new ResponseParamViewModel(L("DeleteDataDictionaryItemSuccessful")));
        }
    }
}