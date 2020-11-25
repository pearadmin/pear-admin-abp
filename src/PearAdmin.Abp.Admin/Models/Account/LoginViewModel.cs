using System.ComponentModel.DataAnnotations;
using Abp.Auditing;

namespace PearAdmin.Abp.Admin.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        public string UsernameOrEmailAddress { get; set; }

        [Required]
        [DisableAuditing]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; } = "";

        public string ReturnUrlHash { get; set; } = "";
    }
}
