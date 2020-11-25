using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PearAdmin.AbpTemplate.Authorization.Roles.Dto;

namespace PearAdmin.AbpTemplate.Authorization.Roles
{
    /// <summary>
    /// 角色应用服务
    /// </summary>
    public interface IRoleAppService : IApplicationService
    {
        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<RoleDto>> GetAllRole();

        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <returns></returns>
        Task<PagedResultDto<RoleDto>> GetPagedRole(GetPagedRoleInput input);

        /// <summary>
        /// 获取可编辑角色信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetRoleForEditOutput> GetRoleForEdit(NullableIdDto input);

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateRole(CreateRoleDto input);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateRole(UpdateRoleDto input);

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        Task DeleteRole(List<EntityDto<int>> inputs);
    }
}
