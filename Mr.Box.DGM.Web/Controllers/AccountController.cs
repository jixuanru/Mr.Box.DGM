using Mr.Box.DGM.Bll;
using Mr.Box.DGM.Model;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;

namespace Mr.Box.DGM.Web.Controllers
{
    public class AccountController : Controller
    {
        //账户服务
        // GET: /Account/
        #region ViewResult
        //账户服务
        public ViewResult Account() { return View(); }

        //在线开设账户
        public ViewResult AccountApplication()
        {
            string agent = Request["Agents"] == null || Request["Agents"].ToString() == "" ? "1" : Request["Agents"].ToString();
            ViewBag.Agent = agent;
            return View();

        }


        //个人信息
        public ViewResult PersonalInfo() { return View(); }
        //职业信息
        public ViewResult ProfessionInfo() { return View(); }

        //账户类型
        public ViewResult Classification() { return View(); }

        //账户协议
        public ViewResult Agreement() { return View(); }

        //上传证件
        public ViewResult ConfirmRequest() { return View(); }

        public ViewResult Test() { return View(); }
        #endregion

        #region UserRegistion

        //开始申请
        [AcceptVerbs(HttpVerbs.Post)]
        public string Registration(string country, string Zh_Surname, string Zh_Name, string Rome_Surname, string Rome_Name, string Account_Title, string Account_Email, string Account_Agents, string Account_Password, string isAgents)
        {
            string agent = Request.UrlReferrer.Query;//?Agents=1800
            if (agent.Length > 0)
            {
                agent = agent.Substring(agent.LastIndexOf('=') + 1);
                Account_Agents = agent;
            }
            //string agent = Request["Agents"] == null || Request.QueryString["Agents"].ToString() == "" ? "1" : Request["Agents"].ToString();


            Account acc = new Account
            {
                Country = country,
                CH_Surname = Zh_Surname,
                CH_Name = Zh_Name,
                EN_Surname = Rome_Surname,
                EN_Name = Rome_Name,
                Nickname = Account_Title,
                IsAgents = isAgents,
                Email = Account_Email,
                Agents = Account_Agents,
                AccountPassword = Account_Password,
                IsBuy = "0",
                RegDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                OnlineStart = "0",
                OnlineEnd = "0",
                OnlineRoomPwd = "0",
                AccountName = ""
            };
            //Session["Account"] = acc;

            acc.Explanation = isAgents == "Y" ? acc.Explanation = acc.CH_Surname + acc.CH_Name + " IB" : acc.Explanation = acc.CH_Surname + acc.CH_Name;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, acc);
            byte[] result = new byte[ms.Length];
            result = ms.ToArray();
            string temp = Convert.ToBase64String(result);
            ms.Flush();
            ms.Close();
            HttpCookie cookie = new HttpCookie("Account");
            cookie.Expires = DateTime.Now.AddDays(1.0);
            cookie.Value = temp;
            Response.SetCookie(cookie);
            return "success";
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetRegistration()
        {
            //Account acc = Session["Account"] as Account;
            if (Request.Cookies["Account"] == null)
            {
                return null;
            }
            else
            {
                string a = Request.Cookies["Account"].Value;
                byte[] b = Convert.FromBase64String(a);
                MemoryStream ms = new MemoryStream(b, 0, b.Length);
                BinaryFormatter bf = new BinaryFormatter();
                Account account = bf.Deserialize(ms) as Account;
                if (account != null)
                {
                    return Json(account);
                }
                else
                {
                    return null;
                }
            }


        }
        //个人信息
        [AcceptVerbs(HttpVerbs.Post)]
        public string Personal(string platformType, string currencyType, string accountType, string country, string provinces, string city, string address, string code1, string tel1, string code2, string code3, string tel2, string international, string birth, string cardType, string carnNum)
        {
            string a = Request.Cookies["Account"].Value;
            byte[] b = Convert.FromBase64String(a);
            MemoryStream ms = new MemoryStream(b, 0, b.Length);
            BinaryFormatter bf = new BinaryFormatter();
            Account acc = bf.Deserialize(ms) as Account;


            //Account acc = Session["Account"] as Account;
            if (acc != null)
            {
                acc.CurrencyType = currencyType; acc.AccountBelongsPlatformType = platformType;
                acc.AccountType = accountType;
                //Session["Account"] = acc;

                BinaryFormatter bf1 = new BinaryFormatter();
                MemoryStream ms1 = new MemoryStream();
                bf1.Serialize(ms1, acc);
                byte[] result = new byte[ms.Length];
                result = ms1.ToArray();
                string temp = Convert.ToBase64String(result);
                ms1.Flush();
                ms1.Close();
                HttpCookie cookie = new HttpCookie("Account");
                cookie.Expires = DateTime.Now.AddDays(1.0);
                cookie.Value = temp;
                Response.SetCookie(cookie);
            }
            PersonalInformation p = new PersonalInformation
            {
                Birthday = birth,
                Nationality = international,
                CredentialsFromCountry = "中国 China",
                CredentialsType = cardType,
                CredentialsNumber = carnNum,
                Country = country,
                City = city,
                Province = provinces,
                LiveAddress = address,
                PhoneNumber = code1 + "-" + tel1,
                HomePhone = code2 + "-" + code3 + "-" + tel2,
                AccountBelongsPlatformType=platformType
            };

            BinaryFormatter bf3 = new BinaryFormatter();
            MemoryStream ms3 = new MemoryStream();
            bf3.Serialize(ms3, p);
            byte[] pe = new byte[ms3.Length];
            pe = ms3.ToArray();
            string temp2 = Convert.ToBase64String(pe);
            ms3.Flush();
            ms3.Close();
            HttpCookie cookie2 = new HttpCookie("PersonalInformation");
            cookie2.Expires = DateTime.Now.AddDays(1.0);
            cookie2.Value = temp2;
            Response.SetCookie(cookie2);
            //Session["PersonalInformation"] = p;



            return "success";
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetPersonal()
        {
            //PersonalInformation p = Session["PersonalInformation"] as PersonalInformation;
            if (Request.Cookies["PersonalInformation"] == null)
            {
                return null;
            }
            else
            {
                string a = Request.Cookies["PersonalInformation"].Value;
                byte[] b = Convert.FromBase64String(a);
                MemoryStream ms = new MemoryStream(b, 0, b.Length);
                BinaryFormatter bf = new BinaryFormatter();
                PersonalInformation p = bf.Deserialize(ms) as PersonalInformation;

                if (p != null)
                {
                    return Json(p);
                }
                else
                {
                    return null;
                }
            }

        }
        //个人信息补充信息
        [AcceptVerbs(HttpVerbs.Post)]
        public string Profession(string jobType, string isEnglishJob, string employerName, string country, string province, string city, string address, string zipCode, string businessNature, string post, string revenue, string totalInvestment, string firstMoney, string source, string otherSource)
        {
            ProfessionInfo p = new ProfessionInfo
            {
                IsReceiveEnglandControlWork = isEnglishJob,
                ProfessionType = jobType,
                HirerName = employerName,
                HirerCountry = country,
                HirerProvince = province,
                HirerCity = city,
                HirerAddress = address,
                ZipCode = zipCode,
                NatureOfBusiness = businessNature,
                Post = post,
                BEFOREYearRevenue = revenue,
                DepositAndInvestment = totalInvestment,
                FastIntoCapitalMoney = Convert.ToDecimal(firstMoney),
                SourceOfFunds = source,
                OtherSource = otherSource
            };

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, p);
            byte[] result = new byte[ms.Length];
            result = ms.ToArray();
            string temp = Convert.ToBase64String(result);
            ms.Flush();
            ms.Close();
            HttpCookie cookie = new HttpCookie("ProfessionInfo");
            cookie.Expires = DateTime.Now.AddDays(1.0);
            cookie.Value = temp;
            Response.SetCookie(cookie);


            //Session["ProfessionInfo"] = p;
            return "success";
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetProfession()
        {
            //ProfessionInfo p = Session["ProfessionInfo"] as ProfessionInfo;
            if (Request.Cookies["ProfessionInfo"] == null)
            {
                return null;
            }
            else
            {
                string a = Request.Cookies["ProfessionInfo"].Value;
                byte[] b = Convert.FromBase64String(a);
                MemoryStream ms = new MemoryStream(b, 0, b.Length);
                BinaryFormatter bf = new BinaryFormatter();
                ProfessionInfo p = bf.Deserialize(ms) as ProfessionInfo;

                if (p != null)
                {
                    return Json(p);
                }
                else
                {
                    return null;
                }
            }

        }

        //设置交易账户类型
        [AcceptVerbs(HttpVerbs.Post)]
        public string Classification(string Classification)
        {
            string a = Request.Cookies["Account"].Value;
            byte[] b = Convert.FromBase64String(a);
            MemoryStream ms = new MemoryStream(b, 0, b.Length);
            BinaryFormatter bf = new BinaryFormatter();
            Account acc = bf.Deserialize(ms) as Account;


            //Account acc = Session["Account"] as Account;
            if (acc != null)
            {
                acc.AccountTradeType = Classification;

                BinaryFormatter bf1 = new BinaryFormatter();
                MemoryStream ms1 = new MemoryStream();
                bf1.Serialize(ms1, acc);
                byte[] result = new byte[ms.Length];
                result = ms1.ToArray();
                string temp = Convert.ToBase64String(result);
                ms1.Flush();
                ms1.Close();
                HttpCookie cookie = new HttpCookie("Account");
                cookie.Expires = DateTime.Now.AddDays(1.0);
                cookie.Value = temp;
                Response.SetCookie(cookie);
            }

            return "success";
        }

        //获取交易账户类型
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetClassification()
        {
            if (Request.Cookies["Account"] == null)
            {
                return null;
            }
            else
            {
                string a = Request.Cookies["Account"].Value;
                byte[] b = Convert.FromBase64String(a);
                MemoryStream ms = new MemoryStream(b, 0, b.Length);
                BinaryFormatter bf = new BinaryFormatter();
                Account account = bf.Deserialize(ms) as Account;
                if (account != null)
                {
                    return Json(account);
                }
                else
                {
                    return null;
                }
            }
        }

        //提交注册申请
        [AcceptVerbs(HttpVerbs.Post)]
        public string AcceptApplication()
        {
            //ProfessionInfo profe = Session["professionInfo"] as ProfessionInfo;
            string _professionInfo = Request.Cookies["ProfessionInfo"].Value;
            byte[] professionInfo = Convert.FromBase64String(_professionInfo);
            MemoryStream ms_professionInfo = new MemoryStream(professionInfo, 0, professionInfo.Length);
            BinaryFormatter bf_professionInfo = new BinaryFormatter();
            ProfessionInfo profe = bf_professionInfo.Deserialize(ms_professionInfo) as ProfessionInfo;


            //PersonalInformation personalInfo = Session["PersonalInformation"] as PersonalInformation;
            string _personalInformation = Request.Cookies["PersonalInformation"].Value;
            byte[] personalInformation = Convert.FromBase64String(_personalInformation);
            MemoryStream ms_personalInformation = new MemoryStream(personalInformation, 0, personalInformation.Length);
            BinaryFormatter bf_personalInformation = new BinaryFormatter();
            PersonalInformation personalInfo = bf_personalInformation.Deserialize(ms_personalInformation) as PersonalInformation;

            personalInfo.Explanation = "";

            //Account account = Session["Account"] as Account;
            string _account = Request.Cookies["Account"].Value;
            byte[] b_account = Convert.FromBase64String(_account);
            MemoryStream ms_account = new MemoryStream(b_account, 0, b_account.Length);
            BinaryFormatter bf_account = new BinaryFormatter();
            Account account = bf_account.Deserialize(ms_account) as Account;

            account.Agents = account.Agents.Replace("$", "");

            if (profe != null && personalInfo != null && account != null)
            {
                int result = new ApplyForLiveAccountBll().ApplyForLiveAccount(account, profe, personalInfo);
                if (result != 0)
                {
                    DeleteCookie("professionInfo");
                    DeleteCookie("PersonalInformation");
                    DeleteCookie("account");
                    return "success";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }
        #endregion

        /// <summary>
        /// 验证用户邮箱是否存在
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Post)]
        public int EmailValidate(string Email, string Name)
        {
            bool flag = new AccountBll().EmailValidate(Email.ToLower(), Name);
            return Convert.ToInt32(flag);
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
            Session["DepositAccount"] = deposit;
            Session.Timeout = 300;
            DepositAccount acc = new DepositAccountBll().AddDepositAccount(deposit);
        }

        public void DeleteCookie(string cookieName)
        {
            HttpCookie aCookie;
            int num = Request.Cookies.Count;
            for (int i = 0; i < num; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Expires = DateTime.Now.AddDays(-1);
                Response.SetCookie(aCookie);
            }
        }
       
        
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GetOnlineClass(string time)
        {
            return Json(new DGM_NewsBll().GetOnlineClass(time));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult getOnlineClassNotice() { return Json(new DGM_NewsBll().getOnlineClassNotice()); }
    }
}
