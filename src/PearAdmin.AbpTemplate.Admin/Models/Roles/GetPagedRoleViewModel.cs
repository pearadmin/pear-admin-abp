using Abp.AutoMapper;
using PearAdmin.AbpTemplate.Authorization.Roles.Dto;
using PearAdmin.AbpTemplate.Admin.Models.Common;

namespace PearAdmin.AbpTemplate.Admin.Models.Roles
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
