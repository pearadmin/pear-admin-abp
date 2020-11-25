using Abp.Auditing;
using System;
using System.Collections.Generic;
using System.Text;
using PearAdmin.AbpTemplate.Authorization.Users;

namespace PearAdmin.AbpTemplate.Auditing.Dto
{
    /// <summary>
    ///     A helper class to store an <see cref="AuditLog" /> and a <see cref="User" /> object.
    /// </summary>
    public class AuditLogAndUser
    {
        /// <summary>
        /// 审计日志
        /// </summary>
        public AuditLog AuditLogInfo { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public User UserInfo { get; set; }
    }
}
