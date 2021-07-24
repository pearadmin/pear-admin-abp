using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using PearAdmin.AbpTemplate.Admin.Models.AuditLogs;
using PearAdmin.AbpTemplate.Admin.Models.Common;
using PearAdmin.AbpTemplate.Auditing;
using PearAdmin.AbpTemplate.Auditing.Dto;

namespace PearAdmin.AbpTemplate.Admin.Controllers
{
    /// <summary>
    /// 审计日志控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class AuditLogsController : AbpTemplateControllerBase
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

            return Json(new ResponseParamPagedViewModel<AuditLogListDto>(pagedAuditLogList.TotalCount, pagedAuditLogList.Items));
        }
    }
}