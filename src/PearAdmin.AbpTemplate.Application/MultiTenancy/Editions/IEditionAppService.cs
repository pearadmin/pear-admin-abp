using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PearAdmin.AbpTemplate.MultiTenancy.Editions.Dto;

namespace PearAdmin.AbpTemplate.MultiTenancy.Editions
{
    /// <summary>
    /// 版本管理应用服务接口
    /// </summary>
    public interface IEditionAppService : IApplicationService
    {
        /// <summary>
        /// 获取全部版本列表
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<EditionDto>> GetAllEdition();

        /// <summary>
        /// 获取版本用于编辑
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<EditionEditDto> GetEditionForEdit(NullableIdDto input);

        /// <summary>
        /// 创建版本
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task CreateEdition(CreateEditionDto input);

        /// <summary>
        /// 更新版本
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateEdition(UpdateEditionDto input);

        /// <summary>
        /// 删除版本
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task DeleteEdition(EntityDto input);

        /// <summary>
        /// 租户更换版本
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task MoveTenantsToAnotherEdition(MoveTenantsToAnotherEditionDto input);
    }
}
