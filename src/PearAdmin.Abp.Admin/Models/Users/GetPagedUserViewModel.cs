using Abp.AutoMapper;
using PearAdmin.Abp.Authorization.Users.Dto;
using PearAdmin.Abp.Admin.Models.Common;

namespace PearAdmin.Abp.Admin.Models.Users
{
    /// <summary>
    /// 用户分页模型
    /// </summary>
    [AutoMapTo(typeof(GetPagedUserInput))]
    public class GetPagedUserViewModel : PagedViewModel
    {
        public string FilterText { get; set; }
    }
}