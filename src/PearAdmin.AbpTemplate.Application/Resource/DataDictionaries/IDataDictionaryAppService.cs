using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PearAdmin.AbpTemplate.Resource.DataDictionaries.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.Resource.DataDictionaries
{
    /// <summary>
    /// 数据字典应用服务接口
    /// </summary>
    public interface IDataDictionaryAppService : IApplicationService
    {
        /// <summary>
        /// 获取数据字典集合
        /// </summary>
        /// <returns></returns>
        ListResultDto<DataDictionaryDto> GetAllDataDictionary();

        /// <summary>
        /// 根据字典类型名称获取数据字典集合
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<DataDictionaryDto>> GetDataDictionaryListByTypeNames(GetDataDictionaryListByTypeNamesInput input);

        /// <summary>
        /// 获取数据字典列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ListResultDto<DataDictionaryItemDto>> GetAllDataDictionaryItem(GetAllDataDictionaryItemInput input);

        /// <summary>
        /// 获取数据字典项
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<DataDictionaryItemDto> GetDataDictionaryItemForEdit(NullableIdDto<int> input);

        /// <summary>
        /// 创建数据字典项
        /// </summary>
        /// <returns></returns>
        Task CreateDataDictionaryItem(CreateDataDictionaryItemDto input);

        /// <summary>
        /// 更新数据字典项
        /// </summary>
        /// <returns></returns>
        Task UpdateDataDictionaryItem(UpdateDataDictionaryItemDto input);

        /// <summary>
        /// 删除数据字典项
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task DeleteDataDictionaryItem(List<EntityDto<int>> inputs);

        /// <summary>
        /// 根据字典类型和字典项名称获取字典项值
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetDataDictionaryItemNameOutput> GetDataDictionaryItemName(GetDataDictionaryItemNameInput input);
    }
}
