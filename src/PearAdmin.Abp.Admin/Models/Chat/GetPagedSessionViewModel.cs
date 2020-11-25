using Abp.AutoMapper;
using PearAdmin.Abp.Social.Chat.Dto;
using PearAdmin.Abp.Admin.Models.Common;

namespace PearAdmin.Abp.Admin.Models.Chat
{
    /// <summary>
    /// 会话分页模型
    /// </summary>
    [AutoMapTo(typeof(GetPagedSessionInput))]
    public class GetPagedSessionViewModel : PagedViewModel
    {
    }
}
