using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;
using Mr.Box.DGM.Bll;
using System.Collections;

namespace Mr.Box.DGM.Web.Controllers
{
    public class ManageController : Controller
    {
        //后台管理员登陆
        // GET: /Manage/

        #region  后台登陆
        //后台登陆页面
        public ViewResult Login() { return View(); }
        //后台管理首页EditAccountAndSendMail
        public ViewResult Index() { return View(); }
        //后台欢迎界页面
        //登陆  保存密码
        [AcceptVerbs(HttpVerbs.Post)]
        public bool ManagerLogin(string userName, string userPwd)
        {
            DmgfxBackstageAdmin flag = new DMGFXBackstageAdminBll().Login(userName, userPwd);
            if (flag != null)
            {
                //Session["ManageUserName"] = userName;
                HttpCookie cookie = new HttpCookie("ManageUser");//初使化并设置Cookie的名称
                cookie.Values.Add("userName", userName);
                cookie.Values.Add("userPwd", userPwd);
                cookie.Values.Add("IsEnable", flag.IsEnable.ToString());
                cookie.Values.Add("AdminLevel", flag.AdminLevel);
                cookie.Expires = DateTime.Now.AddDays(10);
                Response.SetCookie(cookie);
                return true;
            }
            else
            {
                DeleteCookie("ManageUser");
                return false;
            }
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public string IsLoginValid()
        {
            var requestCookie = Request.Cookies["ManageUser"];
            if (requestCookie?["userName"] != null)
            {
                return requestCookie["userName"];
            }
            return "";
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public string GetSaveCookie()
        {
            if (Request.Cookies["ManageUser"] != null)
            {
                string name = Request.Cookies["ManageUser"]["userName"];
                string pwd = Request.Cookies["ManageUser"]["userPwd"];
                return name + "#" + pwd;
            }
            return "";
        }

        #endregion

        #region 开设真实账户

        public static Account _Account;
        public static ProfessionInfo _ProfessionInfo;
        public static PersonalInformation _PersonalInformation;
        public static CurrencyType _CurrencyType;

        public ViewResult OpenAccount()
        {
            return View();
        }

        public ViewResult AccountDetails()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public string changes_dbo_Account()
        {
            DateTime dt = DateTime.Now;
            //string stime = dt.AddDays(-10).ToString("yyyy-MM-dd") + " 00:00:00";
            //string etime = dt.ToString("yyyy-MM-dd") + " 23:59:59";
            string stime = dt.AddDays(-10).ToString("yyyy-MM-dd");
            string etime = dt.AddDays(1).ToString("yyyy-MM-dd");
            return new AccountBll().cdc_Account_CT(stime, etime);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetAccount()
        {
            string search = Request.Params["search"];
            string type = Request.Params["type"];
            int offset = int.Parse(Request.Params["offset"]);
            int pagesize = int.Parse(Request.Params["pageSize"]);
            int total = 0;
            List<Account> acc = new ApplyForLiveAccountBll().GetAccount(offset, pagesize, search, type, out total);
            var gridData = new { rows = acc, total = total };
            return Json(gridData);

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void GetAccountApplicationDetails(string accountId)
        {
            _Account = new ApplyForLiveAccountBll().GetAccountByAccountID(accountId);

            _ProfessionInfo = new ApplyForLiveAccountBll().GetProfessionInfoByAccountInformationId(_Account.InformationId);

            _PersonalInformation = new ApplyForLiveAccountBll().GetPersonalInformationByProfessionInfoPersonalInformationId(_ProfessionInfo.PersonalInformationId);

            _CurrencyType = new ApplyForLiveAccountBll().GetCurrencyTypeByCurrencyTypeId(_Account.CurrencyType);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Get_Account()
        {
            return Json(_Account);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Get_ProfessionInfo()
        {
            return Json(_ProfessionInfo);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Get_PersonalInformation()
        {
            return Json(_PersonalInformation);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Get_CurrencyType()
        {
            return Json(_CurrencyType);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public bool RemoveAccount()
        {
            return new MT4AccountBll().RemoveAccount(int.Parse(Request.Form["id"]));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public bool EditAccount(Account acc)
        {
            return new MT4AccountBll().EditAccount(new Account
            {
                AccountId = acc.AccountId,
                AccountName = acc.AccountName,
                AccountPassword = acc.AccountPassword,
                Explanation = acc.Explanation,
                Agents = acc.Agents
            },false);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public bool EditAccountAndSendMail(Account acc)
        {
            Account tmpacc = new ApplyForLiveAccountBll().GetAccountByAccountID(acc.AccountId.ToString());
            tmpacc.AccountName = acc.AccountName;
            tmpacc.AccountPassword = acc.AccountPassword;
            tmpacc.Explanation = acc.Explanation;
            tmpacc.Agents = acc.Agents;
            return new MT4AccountBll().EditAccount(tmpacc, true);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public int CopyUser() { return new AccountBll().AccountCopy(int.Parse(Request.Form["id"])) ? 1 : 0; }

        #endregion

        #region 已删除的开户信息
        public ViewResult Recycle() { return View(); }


        [AcceptVerbs(HttpVerbs.Post)]
        public string DeleteRecycle(string id)
        {
            return new AccountBll().DeleteRecycle(int.Parse(id)) ? "success" : "error";
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public string RecoveryRecycle(string id)
        {
            return new AccountBll().RecoveryRecycle(int.Parse(id)) ? "success" : "error";
        }
        #endregion


        #region 取款申请管理
        public ViewResult AccountWithdraw() { return View(); }

        public ViewResult AccountWithdrawalsInfo() { return View(); }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetAccountWithdraw()
        {
            int offset = int.Parse(Request.Form["offset"]);
            int pageSize = int.Parse(Request.Form["pageSize"]);
            string dateTime = Request.Form["dateTime"];
            string search = Request.Form["search"].Trim();
            string stime = ""; string etime = "";
            if (!String.IsNullOrEmpty(dateTime))
            {
                stime = dateTime.Split('—')[0].Trim() + " 00:00:00";
                etime = dateTime.Split('—')[1].Trim() + " 23:59:59";
            }
            else { stime = "1970-01-01 00:00:00"; etime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }

            int total = 0;
            IList<AccountWithdrawals> list = new DepositAccountBll().GetAccountWithdrawals(offset, pageSize, search, stime, etime, out total);

            var gridData = new { rows = list, total = total };
            return Json(gridData);


        }
        [AcceptVerbs(HttpVerbs.Post)]
        public string DelereAccountWithdraw(string id)
        {
            LOG log = new DepositAccountBll().DeleteAccountWithdrawalsInfo(id);

            if (log != null)
            {
                return new UserLogBll().InsertLogs(new UserLog { Account = log.Account, LogIP = IPHelper.GetIp(), LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Explain = "出金状态", Property1 = "出金" + log.Money + ",申请已取消" }) ? "success" : "error";
            }
            else
            {
                return "error";
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public string UpdateAccountWithdraw(string withdrawId)
        {
            LOG log = new DepositAccountBll().UpdateAccountWithdrawalsStatus(withdrawId);
            if (log != null)
            {
                return new UserLogBll().InsertLogs(new UserLog { Account = log.Account, Explain = "出金状态", LogIP = IPHelper.GetIp(), LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Property1 = "出金" + log.Money + ",已处理" }) ? "success" : "error";
            }
            else
            {
                return "error";
            }
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetWithdrawls(string withdrawlsId)
        {
            AccountWithdrawals accountWithdrawals = new DepositAccountBll().GetAccountWithdrawalsByAccountWithdrawalsId(withdrawlsId);
            return Json(accountWithdrawals);
        }
        #endregion

        #region 平台入金管理
        public ViewResult DepositAccount() { return View(); }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetAllDepositAccount()
        {
            int offset = int.Parse(Request.Form["offset"]);
            int pageSize = int.Parse(Request.Form["pageSize"]);
            string dateTime = Request.Form["dateTime"];
            string search = Request.Form["search"].Trim();
            string stime = ""; string etime = "";
            if (!String.IsNullOrEmpty(dateTime))
            {
                stime = dateTime.Split('—')[0] + " 00:00";
                etime = dateTime.Split('—')[1] + " 23:59";
            }
            else { stime = "1971-01-01 00:00"; etime = DateTime.Now.ToString("yyyy-MM-dd HH:mm"); }

            stime = stime.Replace("-", "").Replace(":", "").Replace(" ", "").Replace("/", "");
            etime = etime.Replace("-", "").Replace(":", "").Replace(" ", "").Replace("/", "");
            int total = 0;
            IList<DepositAccount> list = new DepositAccountBll().GetDepositAccount(offset, pageSize, search, stime, etime, out total);

            var gridData = new { rows = list, total = total };
            return Json(gridData);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public string UpdateDepositAccount(string depositId, string payType)
        {
            LOG log = new DepositAccountBll().UpdateDepositAccount(depositId);
            return new UserLogBll().InsertLogs(new UserLog { Account = log.Account, Explain = "入金状态", LogIP = IPHelper.GetIp(), LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Property1 = payType + ",入金" + log.Money + ",入金成功" }) ? "success" : "error";
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public string DeleteDepositAccount(string depositId, string payType)
        {
            LOG log = new DepositAccountBll().DeleteDepositAccount(depositId);
            return new UserLogBll().InsertLogs(new UserLog { Account = log.Account, Explain = "入金状态", LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), LogIP = IPHelper.GetIp(), Property1 = payType + ",入金" + log.Money + ",订单已取消" }) ? "success" : "error";

        }
        #endregion

        #region 表格管理
        public ViewResult TableList() { return View(); }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetTableList()
        {
            int offset = int.Parse(Request.Form["offset"]);
            int pagesize = int.Parse(Request.Form["pagesize"]);
            string type = Request.Form["type"].Trim();         //表格类型
            string search = Request.Form["search"].Trim();     //账号或姓名
            //string datetime = Request.Form["datetime"];
            //string stime = "";
            //string etime = "";

            //if(!string.IsNullOrEmpty(datetime))
            //{
            //    stime = datetime.Split('—')[0];
            //    etime = datetime.Split('—')[1];
            //}
            //else
            //{
            //    stime = "1970-01-01";
            //    etime = DateTime.Now.ToString("yyyy-MM-dd");
            //}

            int total = 0;
            IList<TableList> msg = new TableListBll().GetTableList(offset, pagesize, type,search, out total);

            var data = new { rows = msg, total = total };
            return Json(data);
        }

        public string RemoveTable(string id, string DetailID, string DetailName)
        {
            if (new TableListBll().RemoveTable(id, DetailID, DetailName))
            { return "success"; }
            else
            { return "error"; }
        }
        #endregion

        #region 留言联系管理
        public ViewResult LeaveMessage() { return View(); }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetLeaveMessage()
        {
            int offset = int.Parse(Request.Form["offset"]);
            int pageSize = int.Parse(Request.Form["pageSize"]);
            string dateTime = Request.Form["dateTime"];
            string stime = ""; string etime = "";
            if (!String.IsNullOrEmpty(dateTime))
            {
                stime = dateTime.Split('—')[0];
                etime = dateTime.Split('—')[1];
            }
            else { stime = "1970-01-01"; etime = DateTime.Now.ToString("yyyy-MM-dd"); }
            int total = 0;
            IList<LeaveMessage> msg = new LeaveMessageBll().GetAllMessage(offset, pageSize, stime, etime, out total);

            var data = new { rows = msg, total = total };
            return Json(data);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public string UpdateMsgState(string id) { return new LeaveMessageBll().UpdateMsgState(int.Parse(id)) ? "success" : "error"; }
        [AcceptVerbs(HttpVerbs.Post)]
        public string DeleteMsg(string id) { return new LeaveMessageBll().DeleteMessageInfo(id) ? "success" : "error"; }
        #endregion

        #region 上传文件管理
        public ViewResult FileUpload() { return View(); }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetUpLoadImg()
        {
            string search = Request.Params["search"];
            int offset = int.Parse(Request.Params["offset"]);
            int pagesize = int.Parse(Request.Params["pageSize"]);
            int total = 0;
            IList<UploadImage> img = new FileUploadBll().GetImages(offset, pagesize, search, out total);
            var grid = new { rows = img, total = total };
            return Json(grid);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public string DeleteImg(string id)
        {
            UploadImage img = new FileUploadBll().GetUploadImageByUploadImageId(id);
            string path = Server.MapPath("../" + img.PictureUrl);
            FileHelper.DeleteFile(path);
            return new FileUploadBll().DeleteUpLoadImageInfo(id) ? "success" : "error";
        }
        #endregion

        #region 新闻活动 
        public ViewResult NewsEvents() { return View(); }

        public ViewResult NewsEventsAdd() { return View(); }
        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateInput(false)]//去除验证   
        public string InsertNews(FormCollection form)
        {
            DGM_News news = new DGM_NewsBll().AddDGM_News(new DGM_News
            {
                NewsTitle = form["title"],
                NewsType = "DGM公告",
                NewContent = form["content"],
                NewsHomeText = "",
                NewsAuthor = form["author"],
                CreateDate = DateTime.Parse(form["time"]).ToString("yyyy-MM-dd"),
                IsHomeShow = "0",
                DGMNewsImgUrl = "",
                Explanation = ""
            });

            return news != null ? "success" : "error";
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetNews()
        {
            string search = Request.Params["search"];
            int offset = int.Parse(Request.Params["offset"]);
            int pagesize = int.Parse(Request.Params["pageSize"]);
            int total = 0;
            List<DGM_News> news = new DGM_NewsBll().GetNews(offset, pagesize, search, out total);
            var grid = new { rows = news, total = total };
            return Json(grid);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public bool DeleteNews(string id) { return new DGM_NewsBll().DeleteCheckDGM_News(int.Parse(id)); }
        #endregion

        

        //#region 清除缓存
        [AcceptVerbs(HttpVerbs.Post)]
        public void ClearCache()
        {
            List<string> cacheKeys = new List<string>();
            IDictionaryEnumerator cacheEnum = HttpContext.Cache.GetEnumerator();
            while (cacheEnum.MoveNext())
            {
                cacheKeys.Add(cacheEnum.Key.ToString());
            }
            foreach (string cacheKey in cacheKeys)
            {
                HttpContext.Cache.Remove(cacheKey);
            }
        }


        //删除Cookie
        [AcceptVerbs(HttpVerbs.Post)]
        public void DeleteCookie(string cookieName)
        {
            var num = Request.Cookies.Count;
            for (var i = 0; i < num; i++)
            {
                var requestCookie = Request.Cookies[i];
                if (requestCookie != null) cookieName = requestCookie.Name;
                var aCookie = new HttpCookie(cookieName) { Expires = DateTime.Now.AddDays(-1) };
                Response.SetCookie(aCookie);
            }
        }
        //#endregion

        #region 用户日志管理
        public ViewResult Log() { return View(); }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetLog()
        {
            int offset = int.Parse(Request.Form["offset"]);
            int pageSize = int.Parse(Request.Form["pageSize"]);
            string dateTime = Request.Form["dateTime"];
            string search = Request.Form["search"].Trim();
            string stime = ""; string etime = "";
            if (!string.IsNullOrEmpty(dateTime))
            {
                stime = dateTime.Split('—')[0].Trim() + " 00:00:00";
                etime = dateTime.Split('—')[1].Trim() + " 23:59:59";
            }
            else { stime = "1970-01-01 00:00:00"; etime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); }
            int total = 0;
            List<UserLog> log = new UserLogBll().GetLogsBySearch(offset, pageSize, search, stime, etime, out total);
            var gridData = new { rows = log, total = total };
            return Json(gridData);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public bool DeleteLog(string id)
        {
            return new UserLogBll().DeleteLog(int.Parse(id));
        }
        #endregion


    }

}
