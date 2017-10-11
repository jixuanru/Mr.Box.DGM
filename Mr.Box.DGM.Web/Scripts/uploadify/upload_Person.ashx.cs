using Mr.Box.DGM.Bll;
using Mr.Box.DGM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;


namespace DGM.DGMFX.UI.Scripts.uploadify
{
    /// <summary>
    /// upload_Person 的摘要说明
    /// </summary>
    public class upload_Person : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            try
            {
                //string user = context.Request.QueryString["user"].ToString();//当前登陆用户
                string strFilePath = context.Request.Form["strFilePath"].ToString();
                string typeName = context.Request.Form["typeName"].ToString();
                string userName = context.Request.Form["userName"].ToString();
                HttpPostedFile file = context.Request.Files["upload_Person"];

                string uploadPath = HttpContext.Current.Server.MapPath("..\\..\\Upload\\Credentials") + "\\" + strFilePath + "\\"; //@context.Request["folder"]
                if (file != null)
                {
                    string name = file.FileName;
                    string type = name.Substring(name.LastIndexOf('.')).ToLower();
                    if (type == ".jpg" || type == ".jpeg" || type == ".png" || type == ".gif" || type == "bmp")
                    {
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
                            //FileHelper.DeleteFiles(uploadPath);
                            //string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff");
                            //string newFileName = fileName + type;
                            file.SaveAs(uploadPath + name);

                            string path = "Upload/Credentials/" + strFilePath + "/" + name;
                            bool flag = new DepositAccountBll().AddUploadImgInfo(new UploadImage
                            {
                                 AccountID="",
                                PictureTypeId = typeName,
                                UploadName = userName,
                                PictureUrl = path,
                                UPloadTime = DateTime.Now.ToString(),
                                AddressDifferent = ""
                            });
                            if (flag)
                            {
                                context.Response.Write("1");
                            }
                            else
                            {
                                context.Response.Write("0");
                            }
                            //context.Session["file"] = HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\";
                            //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失

                        }
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