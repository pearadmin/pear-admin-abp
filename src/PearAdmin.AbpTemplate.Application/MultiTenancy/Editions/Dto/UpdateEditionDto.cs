using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace PearAdmin.AbpTemplate.MultiTenancy.Editions.Dto
{
    public class UpdateEditionDto : EntityDto<int>
    {
        /// <summary>
        /// 版本名称
        /// </summary>
        [Required]
        public string DisplayName { get; set; }

        /// <summary>
        /// 功能模块
        /// </summary>
        [Required]
        public List<NameValueDto> FeatureValues { get; set; }
    }
}