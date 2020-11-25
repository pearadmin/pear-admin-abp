using System;
using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;
using Abp.Domain.Entities;

namespace PearAdmin.Abp.Authorization.Users.Dto
{
    /// <summary>
    /// 用户信息编辑Dto
    /// </summary>
    public class UserEditDto : IPassivable
    {
        /// <summary>
        /// 根据id是否有值来判断是创建还是添加
        /// </summary>
        public long? Id { get; set; }

        [Required]
        [StringLength(AbpUserBase.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [StringLength(AbpUserBase.MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }

        public bool IsLockoutEnabled { get; set; }

        public bool IsActive { get; set; }
    }
}