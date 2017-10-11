using System.Collections.Generic;
using System.Data;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;
using static System.String;

namespace Mr.Box.DGM.Dal
{
    public class Mt4UsersDal
    {
        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();
        List<string> param = new List<string>();

        public List<MT4_Users> GetUser(string login)
        {
            List<MT4_Users> list = new List<MT4_Users>();
            string sql = "select LOGIN,ENABLE,ENABLE_CHANGE_PASS,ENABLE_READONLY,PASSWORD_PHONE,NAME,COUNTRY, CITY,STATE,ZIPCODE,ADDRESS, PHONE, EMAIL,COMMENT,ID,STATUS,REGDATE,LASTDATE ,LEVERAGE,AGENT_ACCOUNT,TIMESTAMP,BALANCE,PREVMONTHBALANCE,PREVBALANCE,CREDIT, INTERESTRATE,TAXES,SEND_REPORTS,MQID,USER_COLOR,EQUITY,MARGIN,MARGIN_LEVEL,MARGIN_FREE,CURRENCY,MODIFY_TIME from mt4_users where login='" + login + "'";

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "mt4");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new MT4_Users {
                        LOGIN = mDr["LOGIN"].ToString(),
                        ENABLE = mDr["ENABLE"].ToString(),
                        ENABLE_CHANGE_PASS = mDr["ENABLE_CHANGE_PASS"].ToString(),
                        ENABLE_READONLY = mDr["ENABLE_READONLY"].ToString(),
                        PASSWORD_PHONE = mDr["PASSWORD_PHONE"].ToString(),
                        NAME = mDr["NAME"].ToString(),
                        COUNTRY = mDr["COUNTRY"].ToString(),
                        CITY = mDr["CITY"].ToString(),
                        STATE = mDr["STATE"].ToString(),
                        ZIPCODE = mDr["ZIPCODE"].ToString(),
                        ADDRESS = mDr["ADDRESS"].ToString(),
                        PHONE = mDr["PHONE"].ToString(),
                        EMAIL = mDr["EMAIL"].ToString(),
                        COMMENT = mDr["COMMENT"].ToString(),
                        ID = mDr["ID"].ToString(),
                        STATUS = mDr["STATUS"].ToString(),
                        REGDATE = mDr["REGDATE"].ToString(),
                        LASTDATE = mDr["LASTDATE"].ToString(),
                        LEVERAGE = mDr["LEVERAGE"].ToString(),
                        AGENT_ACCOUNT = mDr["AGENT_ACCOUNT"].ToString(),
                        TIMESTAMP = mDr["TIMESTAMP"].ToString(),
                        BALANCE = mDr["BALANCE"].ToString(),
                        PREVMONTHBALANCE = mDr["PREVMONTHBALANCE"].ToString(),
                        PREVBALANCE = mDr["PREVBALANCE"].ToString(),
                        CREDIT = mDr["CREDIT"].ToString(),
                        INTERESTRATE = mDr["INTERESTRATE"].ToString(),
                        TAXES = mDr["TAXES"].ToString(),
                        SEND_REPORTS = mDr["SEND_REPORTS"].ToString(),
                        MQID = mDr["MQID"].ToString(),
                        USER_COLOR = mDr["USER_COLOR"].ToString(),
                        EQUITY = mDr["EQUITY"].ToString(),
                        MARGIN = mDr["MARGIN"].ToString(),
                        MARGIN_LEVEL = mDr["MARGIN_LEVEL"].ToString(),
                        MARGIN_FREE = mDr["MARGIN_FREE"].ToString(),
                        CURRENCY = mDr["CURRENCY"].ToString(),
                        MODIFY_TIME = mDr["MODIFY_TIME"].ToString()
                    });
                }

            }
            catch
            { }
            return list;
        }

        public List<MT4_Users> GetUser(int pageSize, int pageOffset, string login, string type, string search, out int total)
        {
            string sql = "";
            string _sql = "";
            switch (type)
            {
                case "代理商":
                    if (IsNullOrEmpty(search))
                    {
                        sql =
                            $"SELECT * FROM mt4_users WHERE AGENT_ACCOUNT LIKE'{login}%' AND AGENT_ACCOUNT!={login} AND NAME LIKE'%COMMISSIONS%'  ORDER BY REGDATE DESC LIMIT {pageOffset},{pageSize}";
                        _sql =
                            $"SELECT COUNT(*) FROM mt4_users WHERE AGENT_ACCOUNT LIKE'{login}%' AND AGENT_ACCOUNT!={login} AND NAME LIKE'%COMMISSIONS%';";
                    }
                    else
                    {
                        sql =
                            $"SELECT * FROM mt4_users WHERE AGENT_ACCOUNT LIKE'{login}%' AND AGENT_ACCOUNT!={login} AND NAME LIKE'%COMMISSIONS%' AND LOGIN ={search}   ORDER BY REGDATE DESC LIMIT {pageOffset},{pageSize}";
                        _sql =
                            $"SELECT COUNT(*) FROM mt4_users WHERE AGENT_ACCOUNT LIKE'{login}%' AND AGENT_ACCOUNT!={login} AND NAME LIKE'%COMMISSIONS%' AND LOGIN ={search};";
                    }
                    break;
                case "直属客户":
                    if (IsNullOrEmpty(search))
                    {
                        sql =
                            $"SELECT * FROM mt4_users WHERE AGENT_ACCOUNT = {login} AND LOGIN !={login} ORDER BY REGDATE DESC LIMIT {pageOffset},{pageSize};";
                        _sql = $"SELECT COUNT(*) FROM mt4_users WHERE AGENT_ACCOUNT = {login} AND LOGIN !={login}";
                    }
                    else
                    {
                        sql =
                            $"SELECT * FROM mt4_users WHERE AGENT_ACCOUNT = {login} AND LOGIN !={login} AND LOGIN ={search} ORDER BY REGDATE DESC LIMIT {pageOffset},{pageSize};";
                        _sql =
                            $"SELECT COUNT(*) FROM mt4_users WHERE AGENT_ACCOUNT = {login} AND LOGIN !={login} AND LOGIN ={search};";
                    }

                    break;
                case "所有客户":
                    if (IsNullOrEmpty(search))
                    {
                        sql =
                            $"SELECT * FROM mt4_users WHERE LOGIN LIKE '{login}%' AND LOGIN !={login} ORDER BY REGDATE DESC LIMIT {pageOffset},{pageSize};";
                        _sql = $"SELECT COUNT(*) FROM mt4_users WHERE LOGIN LIKE '{login}%' AND LOGIN !={login};";
                    }
                    else
                    {
                        sql =
                            $"SELECT * FROM mt4_users WHERE LOGIN LIKE '{login}%' AND LOGIN !={login} AND LOGIN ={search} ORDER BY REGDATE DESC LIMIT {pageOffset},{pageSize};";
                        _sql =
                            $"SELECT COUNT(*) FROM mt4_users WHERE LOGIN LIKE '{login}%' AND LOGIN !={login} AND LOGIN ={search};";
                    }

                    break;
                default:
                    break;
            }
            List<MT4_Users> list = new List<MT4_Users>();

            total = 0;
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "mt4");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new MT4_Users {
                        LOGIN = mDr["LOGIN"].ToString(),
                        ENABLE = mDr["ENABLE"].ToString(),
                        ENABLE_CHANGE_PASS = mDr["ENABLE_CHANGE_PASS"].ToString(),
                        ENABLE_READONLY = mDr["ENABLE_READONLY"].ToString(),
                        PASSWORD_PHONE = mDr["PASSWORD_PHONE"].ToString(),
                        NAME = mDr["NAME"].ToString(),
                        COUNTRY = mDr["COUNTRY"].ToString(),
                        CITY = mDr["CITY"].ToString(),
                        STATE = mDr["STATE"].ToString(),
                        ZIPCODE = mDr["ZIPCODE"].ToString(),
                        ADDRESS = mDr["ADDRESS"].ToString(),
                        PHONE = mDr["PHONE"].ToString(),
                        EMAIL = mDr["EMAIL"].ToString(),
                        COMMENT = mDr["COMMENT"].ToString(),
                        ID = mDr["ID"].ToString(),
                        STATUS = mDr["STATUS"].ToString(),
                        REGDATE = mDr["REGDATE"].ToString(),
                        LASTDATE = mDr["LASTDATE"].ToString(),
                        LEVERAGE = mDr["LEVERAGE"].ToString(),
                        AGENT_ACCOUNT = mDr["AGENT_ACCOUNT"].ToString(),
                        TIMESTAMP = mDr["TIMESTAMP"].ToString(),
                        BALANCE = mDr["BALANCE"].ToString(),
                        PREVMONTHBALANCE = mDr["PREVMONTHBALANCE"].ToString(),
                        PREVBALANCE = mDr["PREVBALANCE"].ToString(),
                        CREDIT = mDr["CREDIT"].ToString(),
                        INTERESTRATE = mDr["INTERESTRATE"].ToString(),
                        TAXES = mDr["TAXES"].ToString(),
                        SEND_REPORTS = mDr["SEND_REPORTS"].ToString(),
                        MQID = mDr["MQID"].ToString(),
                        USER_COLOR = mDr["USER_COLOR"].ToString(),
                        EQUITY = mDr["EQUITY"].ToString(),
                        MARGIN = mDr["MARGIN"].ToString(),
                        MARGIN_LEVEL = mDr["MARGIN_LEVEL"].ToString(),
                        MARGIN_FREE = mDr["MARGIN_FREE"].ToString(),
                        CURRENCY = mDr["CURRENCY"].ToString(),
                        MODIFY_TIME = mDr["MODIFY_TIME"].ToString()
                    });
                }
                total = int.Parse(ws_mysql.ExecuteScalar(param.ToArray(), "", _sql, "mt4"));
            }
            catch
            { }
            return list;
        }
    }
}
