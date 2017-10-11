using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Text;
using Mr.Box.DGM.Log;

namespace Mr.Box.DGM.Web.Controllers
{
    public class SoapController : Controller
    {
        // GET: Soap
        public ActionResult Index()
        {
            StringBuilder soap = new StringBuilder();
            soap.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            //soap.Append("<soapenv:Envelope xmlns: soapenv = \"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:xsd = \"http://www.w3.org/2001/XMLSchema\" xmlns: xsi = \"http://www.w3.org/2001/XMLSchema-instance\">");

            soap.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:shar=\"http://currenex.com/webservice/shared\">");
            soap.Append("<soapenv:Header/>");
            soap.Append("<soapenv:Body>");
            //soap.Append("<loginRequest xmlns=\"http://currenex.com/webservice/shared\">");
            soap.Append("<loginRequest>");
            soap.Append("<userID>DGMFXS008</shar:userID>");
            soap.Append("<password>zhangjie6100</shar:password>");
            soap.Append("<loginType>GetOrCreateSession</shar:loginType>");
            soap.Append("</loginRequest>");
            soap.Append("</soapenv:Body>");
            soap.Append("</soapenv:Envelope>");
            LogUtil.Info(soap.ToString());
            //发起请求
            Uri uri = new Uri("https://pret-dl.currenex.com/webservice/request/LogonHistoryService");
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.ContentType = "application/soap+xml; charset=utf-8";
            webRequest.Method = "POST";
            using (Stream requestStream = webRequest.GetRequestStream())
            {
                byte[] paramBytes = Encoding.UTF8.GetBytes(soap.ToString());
                requestStream.Write(paramBytes, 0, paramBytes.Length);
            }

            //响应
            WebResponse webResponse;
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (WebException ex)
            {
                webResponse = ex.Response;
            }
            using (StreamReader myStreamReader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
            {
                LogUtil.Info(myStreamReader.ReadToEnd());
            }

            //new loginRequest
            //{
            //    userID = "DGMFXT008",
            //    password = "zhangjie6100",
            //    loginType = loginRequestLoginType.GetOrCreateSession
            //};


            //com.currenex.dret.MarginRate market = new MarginRate();
            //string a = market.term;
            //LogUtil.Info(a);
            return View();
        }
    }
}