using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PearAdmin.AbpTemplate.MultiTenancy.Tenants.Dto;
using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.MultiTenancy.Tenants
{
    /// <summary>
    /// 租户应用服务
    /// </summary>
    public interface ITenantAppService : IApplicationService
    {
        /// <summary>
        /// 获取全部租户
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<TenantDto>> GetAllTenant();

        /// <summary>
        /// 获取单个租户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<TenantDto> GetTenantForEdit(EntityDto<int> input);

        /// <summary>
        /// 移动当前租户版本到其它版本
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task ChangeTenantEdition(ChangeTenantEditionDto input);
    }
}

