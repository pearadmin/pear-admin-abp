using Abp.Application.Services.Dto;

namespace PearAdmin.AbpTemplate.Resource.DataDictionaries.Dto
{
    /// <summary>
    /// 数据字典项Dto
    /// </summary>
    public class DataDictionaryItemDto : EntityDto<long>
    {
        /// <summary>
        /// 业务代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 字典项名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 字典Id
        /// </summary>
        public int DataDictionaryId { get; set; }
    }
}
