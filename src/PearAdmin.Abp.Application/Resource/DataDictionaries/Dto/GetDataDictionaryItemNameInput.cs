using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PearAdmin.Abp.Resource.DataDictionaries.Dto
{
    /// <summary>
    /// 根据字典类型或业务代码获取字典项展示值
    /// </summary>
    public class GetDataDictionaryItemNameInput
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        [Required]
        public string TypeName { get; set; }

        /// <summary>
        /// 字典项业务代码
        /// </summary>
        [Required]
        public string ItemCode { get; set; }
    }
}
