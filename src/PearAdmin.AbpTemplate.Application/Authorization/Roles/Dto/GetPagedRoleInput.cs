using Abp.Application.Services.Dto;
using PearAdmin.AbpTemplate.CommonDto;

namespace PearAdmin.AbpTemplate.Authorization.Roles.Dto
{
    /// <summary>
    /// 分页请求角色Dto
    /// </summary>
    public class GetPagedRoleInput : PagedAndFilteredInputDto
    {
        public string Name { get; set; }
    }
}

