using System.ComponentModel.DataAnnotations;
using Abp.Auditing;
using Abp.Authorization.Users;
using Abp.Runtime.Validation;

namespace PearAdmin.Abp.Authorization.Users.Dto
{
    public class CreateUserDto : IShouldNormalize
    {
        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(AbpUserBase.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        public string[] AssignedRoleNames { get; set; }

        public long[] AssignedOrganizationUnitIds { get; set; }

        public void Normalize()
        {
            if (AssignedRoleNames == null)
            {
                AssignedRoleNames = new string[0];
            }

            if (AssignedOrganizationUnitIds == null)
            {
                AssignedOrganizationUnitIds = new long[0];
            }
        }
    }
}
