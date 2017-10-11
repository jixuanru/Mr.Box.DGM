using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web;

namespace Mr.Box.DGM.Common
{
    public class EmailHelper
    {
        private MailMessage mMailMessage;   //主要处理发送邮件的内容（如：收发人地址、标题、主体、图片等等）
        private SmtpClient mSmtpClient; //主要处理用smtp方式发送此邮件的配置信息（如：邮件服务器、发送端口号、验证方式等等）
        private int mSenderPort=PublicConst.EmailSmtpServerPort;   //发送邮件所用的端口号（htmp协议默认为25）
        private string mSenderServerHost=PublicConst.EmailSmtpServer;    //发件箱的邮件服务器地址（IP形式或字符串形式均可）
        private string mSenderPassword=PublicConst.EmailSendAccountPwd;    //发件箱的密码
        private string mSenderUsername=PublicConst.EmailSendAccount;   //发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）
        private bool mEnableSsl=PublicConst.EmailEnableSsl;    //是否对邮件内容进行socket层加密传输
        private bool mEnablePwdAuthentication=PublicConst.EmailIsSmtpAuth;  //是否对发件人邮箱进行密码验证


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="server">发件箱的邮件服务器地址</param>
        /// <param name="toMail">收件人地址（可以是多个收件人，程序中是以“;"进行区分的）</param>
        /// <param name="fromMail">发件人地址</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="emailBody">邮件内容（可以以html格式进行设计）</param>
        /// <param name="username">发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）</param>
        /// <param name="password">发件人邮箱密码</param>
        /// <param name="port">发送邮件所用的端口号（htmp协议默认为25）</param>
        /// <param name="sslEnable">true表示对邮件内容进行socket层加密传输，false表示不加密</param>
        /// <param name="pwdCheckEnable">true表示对发件人邮箱进行密码验证，false表示不对发件人邮箱进行密码验证</param>
        //public EmailHelper(string server, string toMail, string fromMail, string subject, string emailBody, string username, string password, string port, bool sslEnable, bool pwdCheckEnable)
        public EmailHelper(string toMail,string subject, string emailBody, Encoding bodyEncoding,string UserName,string Account,string AccountPwd)
        {
            try
            {

                Encoding encoding = Encoding.UTF8;
                string senderDisplayName = "DGMFX";//这个配置的是发件人的要显示在邮件的名称   
                //创建mailMessage对象   
                mMailMessage = new MailMessage();
                string[] toMailList = toMail.Split(';');
                if (toMailList != null)
                {
                    for (int i = 0; i < toMailList.Length; i++)
                    {
                        if (toMailList[i] != null && toMailList[i] != "")
                        {
                            mMailMessage.To.Add(toMailList[i]);
                        }
                    }
                }

                mMailMessage.From = new MailAddress(this.mSenderUsername, senderDisplayName, encoding);//设置发送者名称
                mMailMessage.Subject = subject;



                if (!string.IsNullOrEmpty(emailBody))
                {
                    mMailMessage.Body = emailBody;
                }
                else
                {
                    string strEmailTemplateFile = HttpContext.Current.Server.MapPath("/content/Templet/mailbody.htm");
                    if (FileHelper.IsExistFile(strEmailTemplateFile))
                    {
                        using (StreamReader reader = new StreamReader(strEmailTemplateFile))
                        {
                            mMailMessage.Body = reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        mMailMessage.Body = "";
                    }
                }
                mMailMessage.Body=mMailMessage.Body.Replace("[@Name]", UserName);
                mMailMessage.Body=mMailMessage.Body.Replace("[@Account]", Account);
                mMailMessage.Body= mMailMessage.Body.Replace("[@AccountPwd]", AccountPwd);
                mMailMessage.IsBodyHtml = true;
                mMailMessage.BodyEncoding = bodyEncoding;
                //mMailMessage.BodyEncoding = Encoding.UTF8;
                mMailMessage.Priority = MailPriority.Normal;
                //Attachment attachmentTDC = new Attachment(@"D:\wj.fang\WebSiteImg\DGMTwodimensionalCode.jpg");
                //Attachment attachmentLog = new Attachment(@"D:\wj.fang\WebSiteImg\EmailLog.jpg");
                //Attachment attachmentTDC = new Attachment(HttpContext.Current.Server.MapPath("~/Content/image/")+ "logo.png");
                //mMailMessage.Attachments.Add(attachmentTDC);
                //                mMailMessage.Attachments.Add(attachmentLog);
                //mMailMessage.Body += "<p style ='margin-top:245px;'><img src=\"cid:" + attachmentLog.ContentId + "\"/>";
                //mMailMessage.Body += "<img  height ='85px' src='http://cn.dgmfx.com/content/image/logo.png'/></div>";
                //mMailMessage.Body += "<div>DGMFX亚洲客服部24小时客服电话：400 895 8686</p>";
                //mMailMessage.Body += "<div>上海客服部联系电话：021 50835084</div>";
                //mMailMessage.Body += "<div>客服邮箱：info@dgmfx.com</div>";
                //mMailMessage.Body += "<div>网址：http://www.dgmfx.com</div>";
                //mMailMessage.Body += "<div>地址：上海浦东新区郭守敬路498号28幢101</div>";
                //mMailMessage.Body += "<div>New Zealand Office:</div>";
                //mMailMessage.Body += "<div>TEL:+649 8890628</div>";
                //mMailMessage.Body += "<div>FAX: +64 9 927 4999</div>";
                //mMailMessage.Body += "<div>Address: 8 Normanby Road, Mount Eden, Auckland, New Zealand 1024</div>";

                //mMailMessage.Attachments.Remove(attachmentTDC);
                //this.mSenderServerHost = server;
                //this.mSenderUsername = username;
                //this.mSenderPassword = password;
                //this.mSenderPort = Convert.ToInt32(port);
                //this.mEnableSsl = sslEnable;
                //this.mEnablePwdAuthentication = pwdCheckEnable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        /// <summary>
        /// 添加附件
        /// </summary>
        /// <param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        public void AddAttachments(string attachmentsPath)
        {
            try
            {
                string[] path = attachmentsPath.Split(';'); //以什么符号分隔可以自定义
                Attachment data;
                ContentDisposition disposition;
                for (int i = 0; i < path.Length; i++)
                {
                    data = new Attachment(path[i], MediaTypeNames.Application.Octet);
                    disposition = data.ContentDisposition;
                    int index = path[i].LastIndexOf('/');
                    disposition.FileName = path[i].Substring(index + 1, path[i].Length - index - 1);
                    //disposition.CreationDate = File.GetCreationTime(path[i]);
                    //disposition.ModificationDate = File.GetLastWriteTime(path[i]);
                    //disposition.ReadDate = File.GetLastAccessTime(path[i]);
                    mMailMessage.Attachments.Add(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        /// <summary>
        /// 邮件的发送
        /// </summary>
        public string Send()
        {
            string reuslt = "Success!";
            try
            {

                if (mMailMessage != null)
                {
                    mSmtpClient = new SmtpClient();
                    //mSmtpClient.Host = "smtp." + mMailMessage.From.Host;
                    mSmtpClient.Host = this.mSenderServerHost;
                    mSmtpClient.Port = this.mSenderPort;
                    mSmtpClient.UseDefaultCredentials = false;
                    mSmtpClient.EnableSsl = this.mEnableSsl;
                    if (this.mEnablePwdAuthentication)
                    {
                        System.Net.NetworkCredential nc = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //NTLM: Secure Password Authentication in Microsoft Outlook Express
                        mSmtpClient.Credentials = nc.GetCredential(mSmtpClient.Host, mSmtpClient.Port, "NTLM");
                    }
                    else
                    {
                        mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                    }
                    mSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    mSmtpClient.Send(mMailMessage);

                    foreach (Attachment attachment in mMailMessage.Attachments)
                    {
                        attachment.Dispose();
                    }
                }
                return reuslt;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return ex.ToString();
            }
        }
    }

    public class EmailSend
    {
        private MailMessage mailMessage;
        private SmtpClient smtpClient;
        private string password;//发件人密码  


        /// <summary>  
        /// 处审核后类的实例  
        /// </summary>  
        /// <param name="To">收件人地址</param>  
        /// <param name="From">发件人地址</param>  
        /// <param name="Body">邮件正文</param>  
        /// <param name="Title">邮件的主题</param>  
        /// <param name="Password">发件人密码</param>  
        public EmailSend(string To, string From, string Body, string Title, string Password)  
        {
            string[] to = To.Split(';');  
            mailMessage = new MailMessage();
            for (int i = 0; i < to.Length; i++)
            {
                 mailMessage.To.Add(to[i]);  
            }
            mailMessage.From = new System.Net.Mail.MailAddress(From);  
            mailMessage.Subject = Title;  
            mailMessage.Body = Body;
            //string path1 = System.Web.HttpContext.Current.Server.MapPath("../Images/DGMTwodimensionalCode.jpg");
            string path2 = System.Web.HttpContext.Current.Server.MapPath("../Images/EmailLog.jpg");
            //Attachment attachmentTDC = new Attachment(path1);
            Attachment attachmentLog = new Attachment(path2);
            //mailMessage.Attachments.Add(attachmentTDC);
            mailMessage.Attachments.Add(attachmentLog);
            mailMessage.Body += "<p style ='margin-top:245px;'><img src=\"cid:" + attachmentLog.ContentId + "\"/>";
            //mailMessage.Body += "<img  height ='85px' src=\"cid:" + attachmentTDC.ContentId + "\"/></div>";
            mailMessage.Body += "<div>24 hours service tel of DGMFX Asia Customer Service Department：400 895 8686</p>";
            mailMessage.Body += "<div>Service tel of Shanghai：021 50835084</div>";
            mailMessage.Body += "<div>Service email：<a href='mailto:info@dgmfx.com'>info@dgmfx.com</a></div>";
            mailMessage.Body += "<div>Website：<a href='http://www.dgmfx.com'>http://www.dgmfx.com</a></div>";
            mailMessage.Body += "<div>Address：Room101,Building28,No.498 Guoshoujing Road,Pudong New District, Shanghai,China</div>";
            mailMessage.Body += "<div>New Zealand Office:</div>";
            mailMessage.Body += "<div>TEL:+649 889 0628</div>";
            mailMessage.Body += "<div>FAX: +64 9 927 4999</div>";
            mailMessage.Body += "<div>Address: 8 Normanby Road, Mount Eden, Auckland, New Zealand 1024</div>";

            mailMessage.IsBodyHtml = true;  
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;  
            mailMessage.Priority = System.Net.Mail.MailPriority.High;  
            this.password = Password;  
        }

        /// <summary>  
        /// 添加附件  
        /// </summary>  
        public void Attachments(string Path)
        {
            string[] path = Path.Split(';');
            Attachment data;
            ContentDisposition disposition;
            for (int i = 0; i < path.Length; i++)
            {
                data = new Attachment(path[i], MediaTypeNames.Application.Octet);//实例化 附件  
                disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(path[i]);//获取 附件的创建日期  
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(path[i]);// 获取附件的修改日期  
                disposition.ReadDate = System.IO.File.GetLastAccessTime(path[i]);//获取附 件的读取日期  
                mailMessage.Attachments.Add(data);//添加到附件中  
            }
        }
        public void Send()
        {
            if (mailMessage != null)
            {
                smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, password);//设置发件人身份的票据  
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Host = "smtp." + mailMessage.From.Host;
                smtpClient.Send(mailMessage);
            }
        }
        public void Send(string host)
        {
            if (mailMessage != null)
            {
                smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, password);//设置发件人身份的票据  
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Host = host;
                smtpClient.Send(mailMessage);
            }
        }
    }
}
