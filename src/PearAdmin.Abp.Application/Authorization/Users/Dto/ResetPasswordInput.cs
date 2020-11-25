using System.ComponentModel.DataAnnotations;

namespace PearAdmin.Abp.Authorization.Users.Dto
{
    /// <summary>
    /// 重置密码请求Dto
    /// </summary>
    public class ResetPasswordInput
    {
        [Required]
        public long UserId { get; set; }
    }
}
