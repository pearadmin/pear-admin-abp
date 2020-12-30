namespace PearAdmin.AbpTemplate.Admin.Models.Common
{
    /// <summary>
    /// 返回单条数据
    /// </summary>
    public class ResponseParamSingleViewModel<T> : ResponseParamViewModel
    {
        public T Data { get; set; }

        public ResponseParamSingleViewModel(T data, string msg = "", int code = 200)
        {
            Data = data;
            Code = code;
            Msg = msg;
        }
    }
}
