using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PearAdmin.AbpTemplate.Authorization.Permissions.Dto;

namespace PearAdmin.AbpTemplate.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        /// <summary>
        /// 分页筛选获取权限树结构（树形结构数据）
        /// </summary>
        /// <param name="input">分页、筛选条件</param>
        /// <returns></returns>
        PagedResultDto<PermissionDto> GetPagedPermission(GetPagedPermissionInput input);
    }
}
