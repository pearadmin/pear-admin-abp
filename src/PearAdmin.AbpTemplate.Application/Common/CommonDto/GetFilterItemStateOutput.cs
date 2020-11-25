using System.Collections.Generic;

namespace PearAdmin.AbpTemplate.CommonDto
{
    /// <summary>
    /// 获取界面可筛选状态响应Dto
    /// </summary>
    public class GetFilterItemStateOutput<T>
    {
        /// <summary>
        /// 可筛选状态集合
        /// </summary>
        public List<T> FilterItemStates { get; set; }
    }
}
