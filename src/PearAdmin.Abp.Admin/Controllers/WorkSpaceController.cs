using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;

namespace PearAdmin.Abp.Admin.Controllers
{
    [AbpMvcAuthorize]
    public class WorkSpaceController : AbpControllerBase
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
