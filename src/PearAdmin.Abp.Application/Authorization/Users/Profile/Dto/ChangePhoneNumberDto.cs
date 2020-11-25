using Abp.Authorization.Users;
using System.ComponentModel.DataAnnotations;

namespace PearAdmin.Abp.Authorization.Users.Profile.Dto
{
    public class ChangePhoneNumberDto
    {
        [StringLength(AbpUserBase.MaxPhoneNumberLength)]
        public string NewPhoneNumber { get; set; }
    }
}
