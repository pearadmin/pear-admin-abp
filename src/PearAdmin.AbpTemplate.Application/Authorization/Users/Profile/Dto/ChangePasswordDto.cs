using System.ComponentModel.DataAnnotations;

namespace PearAdmin.AbpTemplate.Authorization.Users.Profile.Dto
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
