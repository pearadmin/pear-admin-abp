using Abp.Application.Services.Dto;
using PearAdmin.Abp.CommonDto;

namespace PearAdmin.Abp.Authorization.Roles.Dto
{
    /// <summary>
    /// 分页请求角色Dto
    /// </summary>
    public class GetPagedRoleInput : PagedAndFilteredInputDto
    {
        public string Name { get; set; }
    }
}

