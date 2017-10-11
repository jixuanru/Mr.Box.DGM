using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Mr.Box.DGM.Web.Controllers
{
    public class AboutController : Controller
    {
        //
        // GET: /About/
        public ViewResult Index() { return View(); }//关于我们


        public ViewResult Risk() { return View(); }//风险披露

        public ViewResult PrivacyProtection() { return View(); }//隐私政策

        public ViewResult PendingStatement() { return View(); }//挂单声明

        public ViewResult WebsiteDisclaimer() { return View(); }//网站免责

        public ViewResult ComplaintsProposals() { return View(); }//投诉建议
        
        public ViewResult FAQ() { return View(); }//常见问题
    }
}
