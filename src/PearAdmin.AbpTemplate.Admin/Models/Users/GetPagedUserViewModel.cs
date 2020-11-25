using Abp.AutoMapper;
using PearAdmin.AbpTemplate.Authorization.Users.Dto;
using PearAdmin.AbpTemplate.Admin.Models.Common;

namespace PearAdmin.AbpTemplate.Admin.Models.Users
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