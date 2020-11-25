using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PearAdmin.AbpTemplate.MultiTenancy.Editions.Dto
{
    /// <summary>
    /// 版本管理编辑Dto
    /// </summary>
    public class EditionEditDto
    {
        /// <summary>
        /// 版本Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 版本名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 功能列表
        /// </summary>
        public List<EditionFeatureDto> Features { get; set; }
    }
}