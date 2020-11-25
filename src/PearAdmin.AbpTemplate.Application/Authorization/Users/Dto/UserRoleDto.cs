namespace PearAdmin.AbpTemplate.Authorization.Users.Dto
{
    /// <summary>
    /// 用户角色Dto
    /// </summary>
    public class UserRoleDto
    {
        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public bool IsAssigned { get; set; }
    }
}
