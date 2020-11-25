using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;

namespace PearAdmin.Abp.Admin.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : AbpControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChatPanel()
        {
            return View();
        }
    }
}
