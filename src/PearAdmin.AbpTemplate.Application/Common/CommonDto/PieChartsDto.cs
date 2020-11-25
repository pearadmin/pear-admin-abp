namespace PearAdmin.AbpTemplate.CommonDto
{
    /// <summary>
    /// 饼状图数据Dto
    /// </summary>
    public class PieChartsDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据值
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
