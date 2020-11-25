using PearAdmin.AbpTemplate.Shared;

namespace PearAdmin.AbpTemplate.Resource.DataDictionaries
{
    /// <summary>
    /// 数据字典类型
    /// </summary>
    public class DataDictionaryType : Enumeration
    {
        public static DataDictionaryType GroupRule_FixAtive = new DataDictionaryType(1, "固定剂及使用");
        public static DataDictionaryType GroupRule_ContainerType = new DataDictionaryType(2, "容器类型");

        public DataDictionaryType(int id, string name)
            : base(id, name)
        {

        }
    }
}
