namespace PearAdmin.Abp.CommonDto
{
    public class CustomColumnDto
    {
        public CustomColumnDto()
        {

        }

        public CustomColumnDto(string columnName, string caption, bool ismerge = false)
        {
            ColumnName = columnName;
            Caption = caption;
            IsMerge = ismerge;
        }

        /// <summary>
        /// 列名称
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 是否合计行
        /// </summary>
        public bool IsMerge { get; set; }
    }
}
