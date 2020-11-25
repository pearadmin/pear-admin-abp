using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PearAdmin.AbpTemplate.Resource.DataDictionaries.Dto
{
    public class UpdateDataDictionaryItemDto : EntityDto<int>
    {
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
