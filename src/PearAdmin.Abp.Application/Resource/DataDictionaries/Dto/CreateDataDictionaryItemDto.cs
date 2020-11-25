using System.ComponentModel.DataAnnotations;

namespace PearAdmin.Abp.Resource.DataDictionaries.Dto
{
    public class CreateDataDictionaryItemDto
    {
        /// <summary>
        /// 字典Id
        /// </summary>
        [Required]
        public int DataDictionaryId { get; set; }

        /// <summary>
        /// 业务代码
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// 字典项名称
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
