using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Bll;

namespace Mr.Box.DGM.Web.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        /// <summary>
        /// MetaTrader4
        /// </summary>
        /// <returns></returns>
        public ViewResult MetaTrader4(){ return View();}

        /// <summary>
        /// Currenex
        /// </summary>
        /// <returns></returns>
        public ViewResult Currenex() { return View(); }

        /// <summary>
        /// mt4 客户端
        /// </summary>
        /// <returns></returns>
        public ViewResult Client() { return View(); }

        /// <summary>
        /// mt4手机端
        /// </summary>
        /// <returns></returns>
        public ViewResult Phone() { return View(); }

        /// <summary>
        /// 多账户管理
        /// </summary>
        /// <returns></returns>
        public ViewResult MultipleAccounts() { return View(); }

        /// <summary>
        /// Currenex Classic
        /// </summary>
        /// <returns></returns>
        public ViewResult Classic() { return View(); }

        /// <summary>
        /// Currenex Viking
        /// </summary>
        /// <returns></returns>
        public ViewResult Viking() { return View(); }

        /// <summary>
        /// Currenex API
        /// </summary>
        /// <returns></returns>
        public ViewResult API() { return View(); }

        /// <summary>
        /// Currenex 白标
        /// </summary>
        /// <returns></returns>
        public ViewResult CNX() { return View(); }

        /// <summary>
        /// mt4白标
        /// </summary>
        /// <returns></returns>
        public ViewResult MT4() { return View(); }

        /// <summary>
        /// mt4桥接
        /// </summary>
        /// <returns></returns>
        public ViewResult Bridge() { return View(); }


        /// <summary>
        /// 申请Classic  Viking Stream  Trading
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public bool LeaveMsg(FormCollection form) {

            return new LeaveMessageBll().AddLeaveMessage(new LeaveMessage
            {
                Company = form["company"],
                Email = form["email"],
                Name = form["name"],
                LeaveDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"),
                Phone = form["phone"],
                MessageType = form["type"],
                MessageContent = form["exp"],
                Address =form["address"],
                IsEnable = "未处理"
            }); 
        }
    }
}