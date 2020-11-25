using Abp.AutoMapper;
using PearAdmin.AbpTemplate.Social.Chat.Dto;
using PearAdmin.AbpTemplate.Admin.Models.Common;

namespace PearAdmin.AbpTemplate.Admin.Models.Chat
{
    /// <summary>
    /// 会话分页模型
    /// </summary>
    [AutoMapTo(typeof(GetPagedSessionInput))]
    public class GetPagedSessionViewModel : PagedViewModel
    {
    }
}
