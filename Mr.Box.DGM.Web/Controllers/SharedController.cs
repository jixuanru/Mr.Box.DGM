using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mr.Box.DGM.Web.Controllers
{
    public class SharedController : Controller
    {
        //
        // GET: /Shared/

        public ViewResult Foot(){return View();}
        public ViewResult Top() { return View(); }



        public ViewResult _Top() { return View(); }

        public ViewResult _Foot() { return View(); }


    }
}
