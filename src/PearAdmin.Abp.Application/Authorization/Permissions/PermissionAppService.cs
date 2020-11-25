using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Abp.Linq.Extensions;
using System.Linq;
using PearAdmin.Abp.Authorization.Permissions.Dto;

namespace PearAdmin.Abp.Authorization.Permissions
{
    public class PermissionAppService : AbpApplicationServiceBase, IPermissionAppService
    {
        public PagedResultDto<PermissionDto> GetPagedPermission(GetPagedPermissionInput input)
        {
            var query = PermissionManager.GetAllPermissions()
                .WhereIf(!input.PermissionName.IsNullOrWhiteSpace(), p => p.Name.Contains(input.PermissionName));

            var totalCount = query.Count();

            var items = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return new PagedResultDto<PermissionDto>(
                totalCount,
                items.Select(item =>
                {
                    return ObjectMapper.Map<PermissionDto>(item);
                }).ToList()
            );
        }
    }
}
