using System.Collections.Generic;

namespace PearAdmin.AbpTemplate.Admin.Models.Common
{
    /// <summary>
    /// 分页查询结果及总数
    /// </summary>
    public class ResponseParamPagedViewModel<T> : ResponseParamViewModel
    {
        public ResponseParamPagedViewModel(int count, IReadOnlyList<T> data, string msg = "", int code = 200)
            : base(msg, code)
        {
            Count = count;
            Data = data;
        }

        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }
    }
}
