namespace PearAdmin.AbpTemplate.Admin.Models.Common
{
    /// <summary>
    /// 封装Layui要求的响应参数
    /// </summary>
    public class ResponseParamViewModel
    {
        public ResponseParamViewModel(string msg = "", int code = 200)
        {
            Code = code;
            Msg = msg;
        }

        public int Code { get; set; }

        public string Msg { get; set; }
    }
}
