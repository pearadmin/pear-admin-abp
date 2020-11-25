using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;
using PearAdmin.Abp.Admin.Models.Common;
using PearAdmin.Abp.Auditing;
using PearAdmin.Abp.Admin.Models.AuditLogs;
using PearAdmin.Abp.Auditing.Dto;

namespace PearAdmin.Abp.Admin.Controllers
{
    /// <summary>
    /// 审计日志控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class AuditLogsController : AbpControllerBase
    {
        private readonly IAuditLogAppService _auditLogAppService;

        public AuditLogsController(IAuditLogAppService auditLogAppService)
        {
            _auditLogAppService = auditLogAppService;
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 审计日志列表
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetAuditLogList(GetPagedAuditLogViewModel viewModel)
        {
            var input = PagedViewModelMapToPagedInputDto<GetPagedAuditLogViewModel, GetPagedAuditLogsInput>(viewModel);
            var pagedAuditLogList = await _auditLogAppService.GetAuditLogList(input);

            return Json(new PagedResultViewModel<AuditLogListDto>(pagedAuditLogList.TotalCount, pagedAuditLogList.Items));
        }
    }
}
