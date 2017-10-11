using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Mr.Box.DGM.Common
{
    public class PublicConst
    {
        //获取webservice的用户名和密码
        private static byte[] key = Encoding.UTF32.GetBytes("DGM_WS");
        public readonly static string WS_USERNAME = WebConfigurationManager.AppSettings["UserName"];
        public readonly static string WS_USERPWD = MD5Helper.DESDecrypt(WebConfigurationManager.AppSettings["UserPwd"].ToString(), key).Replace("\0", "");

        //登录MT4
        public readonly static string MasterMT4Server = WebConfigurationManager.AppSettings["MasterMT4Server"].ToString();
        public readonly static string SlaveMT4Server = WebConfigurationManager.AppSettings["SlaveMT4Server"].ToString();

        //发送邮件使用
        public readonly static string EmailSendAccount = WebConfigurationManager.AppSettings["EmailSendAccount"];
        public readonly static string EmailSendAccountPwd = MD5Helper.DESDecrypt(WebConfigurationManager.AppSettings["EmailSendAccountPwd"],key).Replace("\0", "");
        public readonly static string EmailSmtpServer = WebConfigurationManager.AppSettings["EmailSmtpServer"];
        public readonly static int EmailSmtpServerPort =int.Parse(WebConfigurationManager.AppSettings["EmailSmtpServerPort"]);
        public readonly static bool EmailIsSmtpAuth = bool.Parse(WebConfigurationManager.AppSettings["EmailIsSmtpAuth"]);
        public readonly static bool EmailEnableSsl = bool.Parse(WebConfigurationManager.AppSettings["EmailEnableSsl"]);

    }
}
