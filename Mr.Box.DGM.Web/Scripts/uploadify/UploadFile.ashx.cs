using Mr.Box.DGM.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;//Session


namespace SmartGIS.JMElect.Web.Scripts.uploadify
{


    /// <summary>
    /// UploadFile 的摘要说明
    /// </summary>

    public class UploadFile : IHttpHandler, System.Web.SessionState.IRequiresSessionState//IRequiresSessionState 试用Session  就是这样显示的实现一下，不用实现什么方法
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            //string user = context.Request.QueryString["user"].ToString();//当前登陆用户
            string strFilePath = context.Request.Form["strFilePath"].ToString();

            HttpPostedFile file = context.Request.Files["upload_Excel"];
            string uploadPath = HttpContext.Current.Server.MapPath("..\\..\\Upload") + "\\" + strFilePath + "\\"; 
            
            if (file != null)
            {
                string name = file.FileName;
                string type = name.Substring(name.LastIndexOf('.')).ToLower();
                if (type == ".xls" || type == ".xlsx")
                {
                    FileHelper.DeleteFiles(uploadPath);
                    if (!Directory.Exists(uploadPath))
                    {
                        Directory.CreateDirectory(uploadPath);
                    }
                    file.SaveAs(uploadPath + name);
                    
                    //context.Session["file"] = HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\";
                    //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                    context.Response.Write("1");
                }
                else
                {
                    context.Response.Write("0");
                }
            }
            else
            {
                context.Response.Write("0");
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