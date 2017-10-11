using System.Collections.Generic;
using System.Data;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;
using static System.String;

namespace Mr.Box.DGM.Dal
{
    public class UserLogDal
    {
        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();

        List<string> param = new List<string>();

        /// <summary>
        /// 写入用户日志
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public bool InsertLogs(UserLog log)
        {
            string addSql = "insert into UserLog values(Id,@Account,@Explain,@LogDate,@LogIP,@Property1)";
            int num = 0;

            param.Add("[@Account],["+log.Account+"]");
            param.Add("[@Explain],["+log.Explain+"]");
            param.Add("[@LogDate],["+log.LogDate+"]");
            param.Add("[@LogIP],["+log.LogIP+"]");
            param.Add("[@Property1],[" + log.Property1 + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                num = ws_mysql.ExecuteNonQuery(param.ToArray(), "", addSql, "dgm");
            }
            catch
            { }
            finally
            {
                param.Clear();
            }
            return (num > 0);
        }

       
        public List<UserLog> GetLogsBySearch(int offset,int pageSize,string search, string stime, string etime, out int total)
        {
            List<UserLog> list = new List<UserLog>();
            string sql =$"SELECT Id,Account,`Explain`,LogDate,LogIP,Property1 FROM UserLog WHERE LogDate>'{stime}' AND LogDate<='{etime}' AND Account LIKE'%{search}%' ORDER BY LogDate DESC Limit {offset},{pageSize};";
            string _sql =$"SELECT COUNT(*) FROM UserLog WHERE LogDate>'{stime}' AND LogDate<='{etime}' AND Account LIKE'%{search}%';";

            total = 0;

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new UserLog {
                        Id = mDr["Id"].ToString(),
                        Account = mDr["Account"].ToString(),
                        Explain = mDr["Explain"].ToString(),
                        LogDate = mDr["LogDate"].ToString(),
                        LogIP = mDr["LogIP"].ToString(),
                        Property1 = mDr["Property1"].ToString()
                    });
                }
                total = int.Parse(ws_mysql.ExecuteScalar(param.ToArray(), "", _sql, "dgm"));
            }
            catch
            { }
            return list;
        }

        /// <summary>
        /// 删除用户日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteLog(int id)
        {
            string sql = "delete from UserLog where Id=" + id.ToString() + "";
            int num = 0;
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                num = ws_mysql.ExecuteNonQuery(param.ToArray(), "", sql, "dgm");
            }
            catch
            { }
            return (num > 0);
        }

        public List<UserLog> GetLogsByLogin(int pageSize, int pageOffset,string login,  string stime, string etime,out int total)
        {
            List<UserLog> list = new List<UserLog>();
            string sql = "";
            string _sql = "";
            if (!IsNullOrEmpty(stime)&&!IsNullOrEmpty(login))
            {
                sql =
                    $"select * from UserLog where Account='{login}' and LogDate>'{stime}' and LogDate<='{etime}'  order by logDate desc Limit {pageOffset},{pageSize};";
                _sql =
                    $"SELECT COUNT(*) FROM UserLog WHERE Account='{login}' AND LogDate>'{stime}' AND LogDate<='{etime}';";
            }
            else if (!IsNullOrEmpty(stime) && IsNullOrEmpty(login))
            {
                sql = $"select *  from  UserLog where  LogDate>'{stime}' and LogDate<='{etime}' order by logDate desc Limit {pageOffset},{pageSize};";
                _sql = $"SELECT COUNT(*) FROM UserLog WHERE  LogDate>'{stime}' AND LogDate<='{etime}';";
            }
            else if (IsNullOrEmpty(stime) && !IsNullOrEmpty(login))
            {
                sql =$"select top  *  from UserLog where Account='{login}' order by logDate desc Limit {pageOffset},{pageSize};";
                _sql = $"SELECT COUNT(*) FROM UserLog WHERE Account='{login}';";
            }
            else
            {
                sql =$"select *  from UserLog order by logDate desc Limit {pageOffset},{pageSize};";
                _sql = "SELECT COUNT(*) FROM UserLog;";
            }

            total = 0;

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new UserLog {
                        Id = mDr["Id"].ToString(),
                        Account = mDr["Account"].ToString(),
                        Explain = mDr["Explain"].ToString(),
                        LogDate = mDr["LogDate"].ToString(),
                        LogIP = mDr["LogIP"].ToString(),
                        Property1 = mDr["Property1"].ToString()
                    });
                }
                total= int.Parse(ws_mysql.ExecuteScalar(param.ToArray(),"", _sql,"dgm"));
            }
            catch
            { }
            return list;
        }
    }
}
