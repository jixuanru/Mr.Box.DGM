using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Common
{
    public class LogHelper
    {
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="LogTypeName">日志类型名称</param>
        /// <param name="LogTitle">日志标题</param>
        /// <param name="errorInfo">日志内容</param>
        /// <param name="path">日志存放路径</param>
        public static void WriteSystemErrorLog(string LogTypeName, string LogTitle, string errorInfo, string path)
        {
            try
            {
                string FileName = DateTime.Now.ToString("yyyy-MM-dd");
                //string path = @"D:/WorkflowLog/SystemErrorLog";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);// 创建文件夹
                }
                //new FileStream("", FileMode.OpenOrCreate, FileShare.None);
                using (FileStream fileStream = new FileStream(path + "/Log" + FileName + ".log", FileMode.OpenOrCreate))
                {
                    StreamWriter sw = new StreamWriter(fileStream, Encoding.GetEncoding("GB2312"));
                    //sw.BaseStream.Seek(0, SeekOrigin.Begin);
                    //sw.BaseStream.Seek(0, SeekOrigin.Current);
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine(System.DateTime.Now + "(" + LogTypeName + ")" + "----" + LogTitle + ":");
                    sw.WriteLine(errorInfo);
                    sw.WriteLine("===================================================================================");
                    sw.Flush();
                    sw.Close();
                }
            }
            catch (Exception)
            {


                return;
            }
        }

        /// <summary>
        /// 读取错误日志信息
        /// </summary>
        /// <param name="FileName">要读取的文件名称带后缀全名</param>
        /// <param name="path">要读取的文件路径</param>
        /// <returns>返回每行日志信息</returns>
        public static List<string> ReaderErrorLog(string FileName, string path)
        {
            List<string> list = new List<string>();
            string strLine;
            try
            {
                //string path = @"D:/WorkflowLog";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);// 创建文件夹
                }
                FileStream aFile = new FileStream(path + "/" + FileName, FileMode.OpenOrCreate);
                StreamReader reader = new StreamReader(aFile, Encoding.GetEncoding("GB2312"));
                strLine = reader.ReadLine();
                //Read data in line by line 这个兄台看的懂吧~一行一行的读取
                while (strLine != null)
                {
                    list.Add(strLine);
                    strLine = reader.ReadLine();
                }
                reader.Close();
                return list;
            }
            catch (IOException ex)
            {
                string error = ex.ToString();
                return list;
            }


        }
    }
}
