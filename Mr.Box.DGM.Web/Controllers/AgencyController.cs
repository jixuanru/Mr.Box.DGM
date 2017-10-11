using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using IPSVERIFYLib;
using System.Web.Security;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Mr.Box.DGM.Common;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Bll;

namespace Mr.Box.DGM.Web.Controllers
{
    public class AgencyController : Controller
    {
        //
        // GET: /Agency/

        #region 登录
        public ViewResult Login() { return View(); }

        [AcceptVerbs(HttpVerbs.Post)]
        public string AgencyLogin(string userName, string pwd, string role, string isCookie)
        {
            string result = new MT4AccountBll().DGMFXLogIn(userName, pwd, role);
            if (result == "Success")
            {
               
                DGMUserInfo userInfo = new MT4AccountBll().GetAccountInfo(userName);
               
                if (userInfo != null)
                {
                    userInfo.DGMUser_Login = userName;
                    userInfo.DGMUser_Pwd = pwd;
                    userInfo.DGMUser_Role = role;
                    userInfo.DGMUser_Account = new MT4AccountBll().GetAccountByAccountNumberLogin(userName);
                    Session["DGMUserInfo"] = userInfo;
                    if (userInfo.DGMUser_Account == null)//系统后台是否录入数据
                    {
                        InsertLogs(new UserLog { Account = userName, Explain = "登入系统", LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), LogIP = IPHelper.GetIp(), Property1 = "登录失败,账户信息不全" });
                        result = "NotPlatformAccountInfo";
                    }
                    else
                    {
                       


                        HttpCookie cookie = new HttpCookie("MT4User");//初使化并设置Cookie的名称
                        cookie.Values.Add("login", userName);
                        cookie.Values.Add("userName", userInfo.DGMUser_Name);
                        cookie.Values.Add("role", role);
                        cookie.Values.Add("IsCookie", isCookie);
                        if (Convert.ToBoolean(isCookie))
                        {
                            cookie.Expires = DateTime.Now.AddDays(1.0);
                        }
                        else
                        {
                            cookie.Expires = DateTime.Now.AddHours(2.0);
                        }
                        Response.SetCookie(cookie);
                        InsertLogs(new UserLog { Account = userName, Explain = "登入系统", LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), LogIP = IPHelper.GetIp(), Property1 = "登录成功" });
                    }
                }
                else
                {
                    InsertLogs(new UserLog { Account = userName, Explain = "登入系统", LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), LogIP = IPHelper.GetIp(), Property1 = "登录失败,账户信息不全" });
                    result = "NotPlatformAccountInfo";
                }
            }
            else
            {
                InsertLogs(new UserLog { Account = userName, Explain = "登入系统", LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), LogIP = IPHelper.GetIp(), Property1 = "登录失败,用户名或密码错误" });
                result = "Failure";
            }
            return result;
        }
        public ActionResult Index() { return View(); }

        /// <summary>
        /// 写入用户日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool InsertLogs(UserLog log)
        {
            return new UserLogBll().InsertLogs(log);
        }

        public void DeleteCookie(string cookieName)
        {
            var num = Request.Cookies.Count;
            for (var i = 0; i < num; i++)
            {
                cookieName = Request.Cookies[i].Name;
                var aCookie = new HttpCookie(cookieName) {Expires = DateTime.Now.AddDays(-1)};
                Response.SetCookie(aCookie);
            }
        }

        /// <summary>
        /// 获取当前登录用户名  判断是否过期
        /// </summary>
        /// <returns>返回用户姓名+MT4账号</returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public string GetLogin()
        {
            if (Request.Cookies["MT4User"] != null)
            {
                var result = new { userName = Request.Cookies["MT4User"]["userName"], login = Request.Cookies["MT4User"]["login"] };

                JavaScriptSerializer jss = new JavaScriptSerializer();
                return jss.Serialize(result);
            }
            else
            {
                return "";
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public void LoginOut()
        {
            Session.Abandon();
            DeleteCookie("MT4User");
        }
        #endregion

        #region 账户信息
        public ViewResult AccountInfo()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public string GetMt4User()
        {
            string login = Request.Cookies["MT4User"]["login"];
            string role = Request.Cookies["MT4User"]["role"];
            List<MT4_UsersInfo> list = new MT4AccountManagerBll().GetAccountByAgent(login, role);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            return jss.Serialize(list);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public string ShowAccountInfo(string login)
        {
            string aa = "";
            DGMUserInfo userInfo = Session["DGMUserInfo"] as DGMUserInfo;
            MT4AccountBll mt4Bll = new MT4AccountBll();
            userInfo = mt4Bll.GetAccountInfo(login);
            userInfo.DGMUser_Account = new AccountBll().GetAccountByAccountNumber(login);
            if (userInfo != null && userInfo.DGMUser_Account != null)
            {
                string AccountName = userInfo.DGMUser_Account.CH_Surname + userInfo.DGMUser_Account.CH_Name;//姓名
                string AccountLogin = userInfo.DGMUser_Login;//账户
                string AccountEmail = userInfo.DGMUser_Account.Email;//邮箱
                string pwd = userInfo.DGMUser_Account.AccountPassword;
                aa += AccountName + "#" + AccountLogin + "#" + AccountEmail + "#" + pwd;
                ApplyForLiveAccountBll applyBll = new ApplyForLiveAccountBll();
                ProfessionInfo professioninfo = applyBll.GetProfessionInfoByAccountInformationId(userInfo.DGMUser_Account.InformationId);
                PersonalInformation personal = applyBll.GetPersonalInformationByProfessionInfoPersonalInformationId(professioninfo.PersonalInformationId);
                if (personal != null)
                {
                    string AccountPhone = personal.PhoneNumber;//电话
                    string AccountAddress = personal.LiveAddress;//地址
                    //string AccountRevenue = professioninfo.BEFOREYearRevenue;//税前年收入
                    //string AccountInvested = professioninfo.DepositAndInvestment;//储蓄和投资总额
                    aa += "#" + AccountPhone + "#" + AccountAddress;
                }
            }
            return aa;
        }

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetUsers()
        {
            string type = Request.Form["type"];
            List<MT4_Users> list = null;
            int total = 0;
            string login = Request.Cookies["MT4User"]["login"];
            if (type.Trim() == "直属")
            {
                list = new Mt4UserBll().GetUser(login);
                var grid = new { rows = list, total = 1 };
                return Json(grid);
            }
            else
            {
                int pageOffset = int.Parse(Request.Form["offset"]);
                int pageSize = int.Parse(Request.Form["pageSize"]);
                string search = Request.Form["search"];
                list = new Mt4UserBll().GetUser(pageSize, pageOffset, login, type, search, out total);
                var grid = new { rows = list, total = total };
                return Json(grid);
            }

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetStatics()
        {

            string login = Request.Form["login"];
            //string accountList = GetClientUser();
            List<MT4AccountStatisticalInfo> list = new MT4AccountManagerBll().GetAccountStatisticalInfo(login, "", "");
            var grid = new { rows = list, total = list.Count };
            return Json(grid);

        }

        #region 返回登录账户的详细信息（用户信息变更）
        public JsonResult GetUserForChangingForm()
        {
            return Json(new AccountFormsBll().GetUserForChangingForm(Request.Cookies["MT4User"]["login"]));
        }
        #endregion


        #endregion

        #region  账户统计
        public ViewResult AccouontStatistics() { return View(); }
        [AcceptVerbs(HttpVerbs.Post)]
        public string GetAccountStatistics(string stime, string etime)
        {
            string accountList = GetClientUser();
            List<MT4AccountStatisticalInfo> list = new MT4AccountManagerBll().GetAccountStatisticalInfo(accountList, stime, etime);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            jss.MaxJsonLength = Int32.MaxValue;
            return jss.Serialize(list);
        }

        public string GetClientUser()
        {

            List<MT4_UsersInfo> list = new MT4AccountManagerBll().GetAccountByAgent(Request.Cookies["MT4User"]["login"], Request.Cookies["MT4User"]["role"]);
            string user = "";
            for (int i = 0; i < list.Count; i++)
            {
                user += list[i].MT4_UsersLogin + ",";
            }
            return string.IsNullOrEmpty(user) ? "" : user.Substring(0, user.Length - 1);
        }
        #endregion

        #region 订单查询
        public ViewResult Orders() { return View(); }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetOrders()
        {
            int dataStart = int.Parse(Request.Form["offset"]);
            int pageSize = int.Parse(Request.Form["pageSize"]);
            string dateTime = Request.Form["dateTime"];
            string search = Request.Form["search"];
            string stime = ""; string etime = "";
            if (!String.IsNullOrEmpty(dateTime))
            {
                stime = dateTime.Split('—')[0] + " 00:00:00";
                etime = dateTime.Split('—')[1] + " 23:59:59";
            }
            else { stime = ""; etime = ""; }

            string accountList = GetClientUser();

            int total = 0;
            List<MT4_Trades> list = new MT4AccountManagerBll().GetOrders(pageSize, dataStart, stime, etime, accountList, search, out total);
            var grid = new { total = total, rows = list };
            return Json(grid);

        }
        #endregion

        #region 账户入金
        public ViewResult AccountDepoist() { return View(); }

        [HttpGet]
        public ActionResult ReturnDepositInfo()
        {
            //接收数据
            string billno = Request["billno"];//订单编号
            string mercode = Request["mercode"];//商户编号
            string amount = Request["amount"];//订单金额
            string currency_type = Request["Currency_type"];//币种
            string mydate = Request["date"];//订单日期
            string succ = Request["succ"];//成功标示
            string msg = Request["msg"];//发卡行的返回信息
            string attach = Request["attach"];//商户数据包
            string ipsbillno = Request["ipsbillno"];//IPS订单号
            string retEncodeType = Request["retencodetype"];//交易返回签名方式
            string signature = Request["signature"];//数字签名信息
            string bankbillno = Request["bankbillno"];//银行信息

            ViewBag.Mercode = mercode;//商户名称
            ViewBag.Billno = billno;//订单编号
            ViewBag.OperatingName = "";//操作人姓名
            ViewBag.Amount = amount;//订单金额
            ViewBag.Date = mydate;//订单日期
            ViewBag.Succ = succ;//成功标志
            ViewBag.Msg = msg;//发卡行的返回信息
            ViewBag.Ipsbillno = ipsbillno;//IPS订单号
            ViewBag.Currency_Type = currency_type;//币种


            //签名原文
            //billno+【订单编号】+currencytype+【币种】+amount+【订单金额】+date+【订单日期】+succ+【成功标志】+ipsbillno+【IPS订单编号】+retencodetype +【交易返回签名方式】
            string content = "billno" + billno + "currencytype" + currency_type + "amount" + amount + "date" + mydate + "succ" + succ + "ipsbillno" + ipsbillno + "retencodetype" + retEncodeType;

            //签名是否正确
            Boolean verify = false;
            //验证方式：16-md5withRSA  17-md5
            if (retEncodeType == "16")
            {
                string pubfilename = "C:\\PubKey\\publickey.txt";
                RSAMD5Class m_RSAMD5Class = new RSAMD5Class();
                int result = m_RSAMD5Class.VerifyMessage(pubfilename, content, signature);

                //result=0   表示签名验证成功
                //result=-1  表示系统错误
                //result=-2  表示文件绑定错误
                //result=-3  表示读取公钥失败
                //result=-4  表示签名长度错
                //result=-5  表示签名验证失败
                //result=-99 表示系统锁定失败
                if (result == 0)
                {
                    verify = true;
                }
            }
            else if (retEncodeType == "17")
            {
                //登陆http://merchant.ips.com.cn/商户后台下载的商户证书内容
                //测试
                //string merchant_key = "GDgLwwdK270Qj1w4xho8lyTpRQZV9Jm5x4NwWOTThUa4fMhEBK9jOXFrKRT6xhlJuU2FEa89ov0ryyjfJuuPkcGzO5CeVx5ZIrkkt1aBlZV36ySvHOMcNv8rncRiy3DQ";
                //正式
                string merchant_key = "39623688139801517515914269939447918705664553228860304662446375879188281294146103107913225626829868403968911052533216795596255536";
                //string merchant_key = "75063883549352501246651138596975925426112202685729763141483439666432708412434945277710188303368364524301135170754715194525430514";
                //Md5摘要
                string signature1 = FormsAuthentication.HashPasswordForStoringInConfigFile(content + merchant_key, "MD5").ToLower();

                if (signature1 == signature)
                {
                    verify = true;
                }

            }

            //判断签名验证是否通过
            if (verify == true)
            {

                string _depositAccount = Request.Cookies["DepositAccount"].Value;
                byte[] _deposit = Convert.FromBase64String(_depositAccount);
                MemoryStream ms = new MemoryStream(_deposit, 0, _deposit.Length);
                BinaryFormatter bf = new BinaryFormatter();
                DepositAccount depositaccount = bf.Deserialize(ms) as DepositAccount;

                //判断交易是否成功
                if (succ != "Y")
                {
                    ViewBag.ReturnDepositInfoStatus = "汇款失败！";
                    InsertLogs(new UserLog { Account = depositaccount.AccountId, LogIP = IPHelper.GetIp(), LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Explain = "入金状态", Property1 = "入金" + depositaccount.DepositMoney + ",入金失败" });
                }
                else
                {
                    ViewBag.ReturnDepositInfoStatus = "汇款成功！";
                    InsertLogs(new UserLog { Account = depositaccount.AccountId, LogIP = IPHelper.GetIp(), LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Explain = "入金状态", Property1 = "入金" + depositaccount.DepositMoney + ",入金成功" });
                }


                if (depositaccount == null)
                {
                    string[] mer_data = attach.Split('&');
                    if (mer_data.Length != 10)
                    {
                        ViewBag.ReturnDepositInfoStatus += "获取用户信息失败,请与DGM客服联系！";
                    }
                    else
                    {
                        depositaccount = new DepositAccount();
                        depositaccount.DepositMoney = mer_data[0];
                        depositaccount.AccountId = mer_data[1];
                        depositaccount.Address = mer_data[2];
                        depositaccount.ZipCode = "";
                        depositaccount.Name = mer_data[3];
                        depositaccount.Email = mer_data[4];
                        depositaccount.Phone = mer_data[5];
                        depositaccount.Explanation = mer_data[6];
                        depositaccount.PlatformType = mer_data[7];
                        depositaccount.OrderNumber = mer_data[8];
                    }

                }
                //****************************************************************
                //比较返回的订单号和金额与您数据库中的金额是否相符			
                if (depositaccount.DepositMoney != amount)
                {
                    ViewBag.ReturnDepositInfoStatus += "从IPS返回的数据和本地记录的不符合，失败！";
                }
                if (depositaccount != null)
                {
                    ViewBag.Mercode = mercode;//商户名称
                    ViewBag.Billno = billno;//订单编号
                    ViewBag.OperatingName = depositaccount.Name;//操作人姓名
                    ViewBag.Amount = amount;//订单金额
                    ViewBag.Date = mydate;//订单日期
                    ViewBag.Succ = succ;//成功标志
                    ViewBag.Msg = msg;//发卡行的返回信息
                    ViewBag.Ipsbillno = ipsbillno;//IPS订单号
                    ViewBag.Currency_Type = currency_type;//币种


                    string message = msg;//发卡行的返回信息

                    string attachsss = attach;//商户数据包

                    depositaccount.Mercode = mercode;
                    depositaccount.Billno = billno;
                    depositaccount.OrderDate = mydate;
                    depositaccount.SymbolSuccess = succ;
                    depositaccount.BankReturnMsg = msg;
                    depositaccount.IPSBillno = ipsbillno;
                    depositaccount.Currency_Type = currency_type;
                    depositaccount.OrderAmount = amount;
                    depositaccount.Retencodetype = retEncodeType;
                    depositaccount.SignatureInfo = signature;
                    depositaccount.AttachData = attach;
                    DepositAccount deposit = new DepositAccountBll().GetDepositAccountByBillno(depositaccount.Billno);

                    if (deposit == null)
                    {
                        depositaccount = new DepositAccountBll().AddDepositAccount(depositaccount);
                    }
                    else
                    {
                        depositaccount.DepositId = deposit.DepositId;
                        depositaccount = new DepositAccountBll().ModifyCheckDepositAccount(depositaccount);
                    }
                    if (depositaccount != null)
                    {
                        ViewBag.ReturnDepositInfoStatus += "已提交到系统！";
                    }
                    else
                    {
                        ViewBag.ReturnDepositInfoStatus += "网络异常,请与DGM客服联系！";
                    }
                }
                else
                {
                    ViewBag.ReturnDepositInfoStatus += "系统异常,请与DGM客服联系！";
                }
            }
            else
            {
                ViewBag.ReturnDepositInfoStatus = "数字签名不正确！";
            }
            return View();
        }

        /// <summary>
        /// 根据登录的用户  获取用户的信息
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Mt4AccountInfo()
        {
            DGMUserInfo user = Session["DGMUserInfo"] as DGMUserInfo;
            DGMUserInfo info = new MT4AccountBll().GetAccountInfo(user.DGMUser_Login);
            return Json(info);
        }
        //提交入金数据
        [AcceptVerbs(HttpVerbs.Post)]
        public void OnlineDeposit(string money, string account, string address, string name, string email, string phone, string supplement, string platformType, string orderNumber, string billno)
        {
            DepositAccount deposit = new DepositAccount
            {
                DepositMoney = money,
                AccountId = account,
                Address = address,
                Name = name,
                Email = email,
                Phone = phone,
                Explanation = supplement,
                OrderNumber = orderNumber,
                ZipCode = "",
                PlatformType = platformType,
                AttachData = "",
                BankReturnMsg = "",
                Billno = billno,
                Currency_Type = "",
                DepositId = "",
                IPSBillno = "",
                Mercode = "",
                OrderAmount = money,
                OrderDate = "",
                Retencodetype = "",
                SignatureInfo = "",
                SymbolSuccess = "N"
            };
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, deposit);
            byte[] result = new byte[ms.Length];
            result = ms.ToArray();
            string temp = Convert.ToBase64String(result);
            ms.Flush();
            ms.Close();
            HttpCookie cookie = new HttpCookie("DepositAccount");
            cookie.Expires = DateTime.Now.AddMinutes(30.0);
            cookie.Value = temp;
            Response.SetCookie(cookie);
            DepositAccount acc = new DepositAccountBll().AddDepositAccount(deposit);
            InsertLogs(new UserLog { Account = account, LogIP = IPHelper.GetIp(), LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Explain = "入金操作", Property1 = "入金" + money + ",申请已提交,正在处理中..." });
        }
        #endregion

        #region 账户出金
        public ViewResult AccountWithdraw() { return View(); }
        public ViewResult AccountWithdrawTables() { return View(); }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetLoginWithdraw()
        {
            return Json(new DepositAccountBll().GetAccountWithdrawInfo(Request.Cookies["MT4User"]["login"]));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public int AccountWithdrawals(string HolderName, string WWTrading_account, string WWPhone, string WWEmail, string WWAddress, string WWCountry, string WWProvince,
         string WWCity, string WWZipCode, string WWMoney, string WWAccountHolder, string IsCloseAccountY, string IsCloseAccountText,
        string WWBankName, string WWBankAccount, string AccountHolderName, string WWSWIFT_Code, string WWBankAddress, string WWBankCountry,
        string WWBankProvince, string WWBankCity, string WWBankZipCode, string RadioIsTransitBankN, string WWTransitBankName,
        string WWTransitBankAcoount, string WWTransitAcoountHolderName, string WWTransitSWIFTCode, string WWTransitBankAddress,
        string WWTransitCountry, string WWTransitProvince, string WWTransitCity, string WWTransitZipCode, string WWMainAccountHolderName)
        {
            AccountWithdrawals acc = new AccountWithdrawals
            {
                WithdrawalsAccountHolder = HolderName,//账户持有人姓名
                WithdrawalsAccount = WWTrading_account, //交易账号
                Phone = WWPhone,//联系电话
                WithdrawalsEmail = WWEmail,//电子邮箱
                WithdrawalsAddress = WWAddress, //地址
                WithdrawalsCountry = WWCountry, //个人居住国家
                WithdrawalsProvince = WWProvince, //个人居住州
                WithdrawalsCity = WWCity, //个人居住城市
                WithdrawalsCityZipCode = WWZipCode,//个人居住城市邮编
                Money = WWMoney, //取款金额
                BelongsPlatform = WWAccountHolder,//平台meta trader 4
                IsCloseAccount = IsCloseAccountY,//是否关闭交易账户
                CloseReason = IsCloseAccountText,//关闭账户原因
                BankName = WWBankName,//银行名称
                BankAccount = WWBankAccount,//银行账号
                BankHolder = AccountHolderName,//银行账户持有人姓名
                SWIFT_Code = WWSWIFT_Code,//国际银行代码（SWIFT Code）
                BankAddress = WWBankAddress,//银行地址
                BankCountry = WWBankCountry,//银行所在国家
                BankProvince = WWBankProvince,//银行所在州，省
                BankCity = WWBankCity,//银行所在城市
                BankCityZipCode = WWBankZipCode,//银行所在城市邮编
                IsTransitBank = RadioIsTransitBankN,//是否有中转银行
                TransitBankName = WWTransitBankName,//中转银行名称
                TransitBankAccount = WWTransitBankAcoount,//中转银行账号
                TransitBankHolder = WWTransitAcoountHolderName,//中转银行账户持有人姓名
                TransitBankAddress = WWTransitSWIFTCode, //中转银行国际银行代码
                TransitBankSWIFICode = WWTransitBankAddress, //中转银行地址
                TransitBankCountry = WWTransitCountry, //中转银行所在国家
                TransitBankProvince = WWTransitProvince, //中转银行所在州省
                TransitBankCity = WWTransitCity,//中转银行所在城市
                TransitBankCityZipCode = WWTransitZipCode, //中转银行所在城市右邮编
                MainAccountName = WWMainAccountHolderName,//主要账户持有人姓名
                MinorAccountName = "",//次要账户持有人姓名
                WithdrawalsDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                ProcessingStatus = "未处理",
                Explanation = ""
            };
            InsertLogs(new UserLog { Account = WWTrading_account, LogIP = IPHelper.GetIp(), LogDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Explain = "出金操作", Property1 = "出金" + WWMoney + ",申请已提交,正在处理中..." });
            bool flag = new DepositAccountBll().AddAccountWithdrawals(acc);
            return Convert.ToInt32(flag);
        }
        #endregion

        #region 表格下载
        public ViewResult TableDoownload() { return View(); }
        #endregion

        #region 附加帐户申请表
        public ViewResult AccountAdditionalApplicationForm() { return View(); }
        #endregion

        #region 杠杆率变更表
        public ViewResult LeverageChangingForm() { return View(); }
        #endregion

        #region 客户信息变更表
        public ViewResult AccountInformationChangingForm()
        {
            return View();
        }

        public bool AccountInformationChangingForm(string AccountName,string AccountEmail, string AccountCountry, string AccountProvince,
            string AccountAddress, string AccountMobile, string HirerName,string HirerAddress,string HirerZipCode,string HirerCountry,string HirerProvince)
        {
            return new AccountFormsBll().AccountInformationChangingForm(AccountName,  AccountEmail,  AccountCountry, AccountProvince, AccountAddress, AccountMobile, HirerName, HirerAddress, HirerZipCode, HirerCountry, HirerProvince);
        }
        #endregion

        #region 转帐申请表
        public ViewResult MoneyTransferApplicationForm() { return View(); }
        #endregion

        #region 用户日志
        public ViewResult Logs() { return View(); }
        /// <summary>
        /// 获取登录用户的日志
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetLogs()
        {
            string datetime = Request.Form["dateTime"];
            string stime = ""; string etime = "";
            if (string.IsNullOrEmpty(datetime)) { }
            else
            {
                stime = datetime.Split('—')[0].Trim() + " 00:00:00";
                etime = datetime.Split('—')[1].Trim() + " 23:59:59";
            }
            int offset = Convert.ToInt32(Request.Form["offset"]);
            int pageSize = Convert.ToInt32(Request.Form["pagesize"]);
            string login = Request.Cookies["MT4User"]["login"];
            int total = 0;
            List<UserLog> log = new UserLogBll().GetLogsByLogin(pageSize, offset, login, stime, etime, out total);
            var data = new { rows = log, total = total };
            return Json(data);
        }
        #endregion
    }
}
