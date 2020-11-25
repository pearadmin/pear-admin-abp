using System.Collections.Generic;

namespace PearAdmin.AbpTemplate.Resource.DataDictionaries.Dto
{
    /// <summary>
    /// 根据字典类型名称获取数据字典详细信息
    /// </summary>
    public class GetDataDictionaryListByTypeNamesInput
    {
        public List<string> TypeNames { get; set; }
    }
}
