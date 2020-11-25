using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;

namespace PearAdmin.AbpTemplate.Admin.Controllers
{
    [AbpMvcAuthorize]
    public class WorkSpaceController : AbpTemplateControllerBase
    {
        public ActionResult TenantConsole()
        {
            return View();
        }

        public ActionResult HostConsole()
        {
            return View();
        }
    }
}
