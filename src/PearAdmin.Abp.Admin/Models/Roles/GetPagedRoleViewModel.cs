using Abp.AutoMapper;
using PearAdmin.Abp.Authorization.Roles.Dto;
using PearAdmin.Abp.Admin.Models.Common;

namespace PearAdmin.Abp.Admin.Models.Roles
{
    /// <summary>
    /// 角色分页模型
    /// </summary>
    [AutoMapTo(typeof(GetPagedRoleInput))]
    public class GetPagedRoleViewModel : PagedViewModel
    {
        public string Name { get; set; }
    }
}
