using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using PearAdmin.Abp.Loggings;

namespace PearAdmin.Abp.Admin.Controllers
{
    /// <summary>
    /// 系统维护控制器
    /// </summary>
    [AbpMvcAuthorize]
    public class MaintenanceController : AbpControllerBase
    {
        private readonly IWebSiteLogAppService _webSiteLogAppService;

        public MaintenanceController(IWebSiteLogAppService webSiteLogAppService)
        {
            _webSiteLogAppService = webSiteLogAppService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetLatestWebLogs()
        {
            var getLatestWebLogsOuput = _webSiteLogAppService.GetLatestWebLogs();
            return Json(getLatestWebLogsOuput);
        }

        public IActionResult DownloadWebLogs()
        {
            var webLogFileDto = _webSiteLogAppService.DownloadWebLogs();
            return Json(webLogFileDto);
        }
    }
}
