using System.Collections.Generic;

namespace PearAdmin.Abp.Admin.Models.Common
{
    /// <summary>
    /// 封装Layui要求的响应参数及封装集合
    /// </summary>
    public class ResponseParamListViewModel<T>
    {
        public ResponseParamListViewModel(IReadOnlyList<T> data, string msg = "", int code = 200)
        {
            Data = data;
            Code = code;
            Msg = msg;
        }

        public IReadOnlyList<T> Data { get; set; }

        public int Code { get; set; }

        public string Msg { get; set; }
    }
}
