using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace PearAdmin.AbpTemplate.Resource.DataDictionaries.Dto
{
    /// <summary>
    /// 数据字典Dto
    /// </summary>
    public class DataDictionaryDto : EntityDto<int>
    {
        /// <summary>
        /// 字典类型名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 该字典类型下的字典项集合
        /// </summary>
        public List<DataDictionaryItemDto> DataDictionaryItems { get; set; }
    }
}
