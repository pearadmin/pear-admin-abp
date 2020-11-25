namespace PearAdmin.AbpTemplate.Admin.Models.Common
{
    /// <summary>
    /// 封装Layui要求的响应参数
    /// </summary>
    public class ResponseParamSingleViewModel<T>
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }

        public ResponseParamSingleViewModel(T data, string msg = "", int code = 200)
        {
            Data = data;
            Code = code;
            Msg = msg;
        }
    }
}
