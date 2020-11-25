using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PearAdmin.Abp.Authorization.Users.Dto;

namespace PearAdmin.Abp.Authorization.Users
{
    /// <summary>
    /// 用户应用服务接口
    /// </summary>
    public interface IUserAppService : IApplicationService
    {
        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<UserDto>> GetPagedUser(GetPagedUserInput input);

        /// <summary>
        /// 获取可编辑的用户信息(包括组织机构和角色)
        /// </summary>
        /// <param name="input">用户Id</param>
        /// <returns>用户存在下返回用户信息，否则返回新用户，角色列表、用户角色列表、组织机构列表、用户组织机构列表</returns>
        Task<GetUserForEditOutput> GetUserForEdit(NullableIdDto<long> input);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">用户信息、加入的组织机构信息、选择的角色信息</param>
        /// <returns></returns>
        Task CreateUser(CreateUserDto input);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateUser(UpdateUserDto input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="input">批量用户Id</param>
        /// <returns></returns>
        Task DeleteUser(List<EntityDto<long>> inputs);

        /// <summary>
        /// 重置用户密码为默认密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<bool> ResetPassword(ResetPasswordInput input);
    }
}