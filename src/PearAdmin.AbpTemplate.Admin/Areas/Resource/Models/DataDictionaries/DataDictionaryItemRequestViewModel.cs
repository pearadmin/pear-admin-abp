namespace PearAdmin.AbpTemplate.Admin.Areas.Resource.Models.DataDictionaries
{
    /// <summary>
    /// 数据字典项请求参数视图模型
    /// </summary>
    public class DataDictionaryItemRequestViewModel
    {
        /// <summary>
        /// 数据字典项Id
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 数据字典Id
        /// </summary>
        public int? DataDictionaryId { get; set; }

        /// <summary>
        /// 过滤文本
        /// </summary>
        public string FilterText { get; set; }
    }
}
