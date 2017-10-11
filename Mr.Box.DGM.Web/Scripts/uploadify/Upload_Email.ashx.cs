using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace DGM.DGMFX.UI.Scripts.uploadify
{
    /// <summary>
    /// Upload_Email 的摘要说明
    /// </summary>
    public class Upload_Email : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                //string user = context.Request.QueryString["user"].ToString();//当前登陆用户
                string strFilePath = context.Request.Form["strFilePath"].ToString();
                HttpPostedFile file = context.Request.Files["Upload_Email"];

                string uploadPath = HttpContext.Current.Server.MapPath("..\\..\\Upload\\") + strFilePath + "\\"; //@context.Request["folder"]
                if (file != null)
                {
                    string name = file.FileName;
                    string type = name.Substring(name.LastIndexOf('.')).ToLower();
                    if (File.Exists(uploadPath + name))
                    {
                        context.Response.Write(name);
                    }
                    else
                    {
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }
                        
                        file.SaveAs(uploadPath + name);
                        string path = "../Upload/" + strFilePath + "/" + name;
                        string _name = path + "#";
                        string sessionMail = context.Session["Email"].ToString();
                        sessionMail += _name;
                        context.Session["Email"] = sessionMail;
                        context.Response.Write("0");

                        //context.Session["file"] = HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\";
                        //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失

                    }
                }
                else
                {
                    context.Response.Write("0");
                }
            }
            catch (Exception e)
            {
                context.Response.Write(e.Message);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}