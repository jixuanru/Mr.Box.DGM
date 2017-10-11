using Mr.Box.DGM.Bll;
using Mr.Box.DGM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mr.Box.DGM.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetNews()
        {
            int pageOffset = int.Parse(Request.Form["offset"]);
            int pageSize = int.Parse(Request.Form["pageSize"]);
            int total = 0;
            List<DGM_News> list = new DGM_NewsBll().GetNews(pageOffset, pageSize,"",out total);
            var grid = new { rows = list, total = total };
            return Json(grid);
        }


    }
}
