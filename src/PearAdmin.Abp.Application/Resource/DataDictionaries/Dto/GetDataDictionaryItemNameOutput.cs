using System;
using System.Collections.Generic;
using System.Text;

namespace PearAdmin.Abp.Resource.DataDictionaries.Dto
{
    /// <summary>
    /// 返回指定字典类型和指定名称后的值
    /// </summary>
    public class GetDataDictionaryItemNameOutput
    {
        /// <summary>
        /// 字典项展示值
        /// </summary>
        public string ItemName { get; set; }
    }
}
