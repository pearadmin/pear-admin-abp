using PearAdmin.Abp.CommonDto;

namespace PearAdmin.Abp.Authorization.Users.Dto
{
    /// <summary>
    /// 分页、筛选请求获取用户Dto
    /// </summary>
    public class GetPagedUserInput : PagedAndFilteredInputDto
    {
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 启用/禁用
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
