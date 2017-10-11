using Mr.Box.DGM.Bll;
using Mr.Box.DGM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mr.Box.DGM.Web.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Details()
        {
            string id = Request["id"].ToString();
            List<DGM_News> news = new DGM_NewsBll().GetNewsById(id);
            ViewBag.Title = news[0].NewsTitle;
            ViewBag.NewsType = news[0].NewsType;
            return View(news);
        }

        /// <summary>
        /// 20141008  平台上线
        /// </summary>
        /// <returns></returns>
        public ViewResult news141008() { return View(); }

        

        /// <summary>
        /// 元旦会议 20160101
        /// </summary>
        /// <returns></returns>
        public ViewResult news160101() { return View(); }
        
        
        /// <summary>
        /// 香港展会 20160127
        /// </summary>
        /// <returns></returns>
        public ViewResult news160127() { return View(); }
        
        /// <summary>
        /// 金嘉宜培训
        /// </summary>
        /// <returns></returns>
        public ViewResult news160920() { return View(); }

        /// <summary>
        /// DGM首场承诺培训大会
        /// </summary>
        /// <returns></returns>
        public ViewResult news20170410(){return View();}

        public ViewResult news20170411()
        {
            return View();
        }

        public ViewResult news20170414()
        {
            return View();
        }
    }
}