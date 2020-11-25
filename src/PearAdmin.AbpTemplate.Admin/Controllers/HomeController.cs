using Microsoft.AspNetCore.Mvc;
using Abp.AspNetCore.Mvc.Authorization;

namespace PearAdmin.AbpTemplate.Admin.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : AbpTemplateControllerBase
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
