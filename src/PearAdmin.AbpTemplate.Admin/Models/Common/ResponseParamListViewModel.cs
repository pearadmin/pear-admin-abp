using System.Collections.Generic;

namespace PearAdmin.AbpTemplate.Admin.Models.Common
{
    /// <summary>
    /// 返回集合结果
    /// </summary>
    public class ResponseParamListViewModel<T> : ResponseParamViewModel
    {
        public ResponseParamListViewModel(IReadOnlyList<T> data, string msg = "", int code = 200)
        {
            Data = data;
            Code = code;
            Msg = msg;
        }

        public IReadOnlyList<T> Data { get; set; }
    }
}
