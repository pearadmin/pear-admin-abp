using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PearAdmin.Abp.Auditing.Dto;
using PearAdmin.Abp.Admin.Models.Common;

namespace PearAdmin.Abp.Admin.Models.AuditLogs
{
    /// <summary>
    /// 审计日志列表分页视图模型
    /// </summary>
    [AutoMapTo(typeof(GetPagedAuditLogsInput))]
    public class GetPagedAuditLogViewModel : PagedViewModel
    {
        public string ServiceName { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
