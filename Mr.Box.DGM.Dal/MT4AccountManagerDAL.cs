using System;
using System.Collections.Generic;
using System.Data;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;

namespace Mr.Box.DGM.Dal
{
    /// <summary>
    /// 用户管理DAL
    /// </summary>
    public class MT4AccountManagerDAL
    {
        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();
        List<string> param = new List<string>();

        /// <summary>
        /// 根据代理商账户获取MT4账户信息
        /// </summary>
        /// <param name="Agent">代理商账户</param>
        /// <returns>账户信息</returns>
        public List<MT4_UsersInfo> GetAccountByAgent(string Agent, string role)
        {
            List<MT4_UsersInfo> list = new List<MT4_UsersInfo>();
            string sql = "";

            if (role == "Manager")
            {
                sql += "select login ,MAX(Name) as accountName,(select Count(login) from mt4_users where AGENT_ACCOUNT = main.login) as AgentCount,sum(BALANCE) as BALANCE,MAX(PHONE) as PHONE,MAX(EMAIL) as EMAIL from mt4_users as main where login in(select login from mt4_users where AGENT_ACCOUNT like '" + Agent + "%' ) group by login order by login ";
                //sql+= "select login ,MAX(Name) as accountName,(select Count(login) from mt4_users where AGENT_ACCOUNT = main.login) as AgentCount,sum(BALANCE) as BALANCE,MAX(PHONE) as PHONE,MAX(EMAIL) as EMAIL from mt4_users as main where login in(select login from mt4_users where (AGENT_ACCOUNT = '" + Agent + "' and login != '" + Agent + "') or (ZIPCODE = '" + Agent + "' and login != '" + Agent + "')) group by login order by login ";
            }
            else//if (role=="User")
            {
                sql += "select login ,MAX(Name) as accountName,(select Count(login) from mt4_users where AGENT_ACCOUNT = main.login) as AgentCount,sum(BALANCE) as BALANCE,MAX(PHONE) as PHONE,MAX(EMAIL) as EMAIL from mt4_users as main where login in(select login from mt4_users where  login = '" + Agent + "' OR AGENT_ACCOUNT like '" + Agent + "%' ) group by login order by login ";

            }
            //if (role=="Tree")
            //{
            //    sql += "select login ,MAX(Name) as accountName,(select Count(login) from mt4_users where AGENT_ACCOUNT = main.login) as AgentCount,sum(BALANCE) as BALANCE,MAX(PHONE) as PHONE,MAX(EMAIL) as EMAIL from mt4_users as main where login in(select login from mt4_users where AGENT_ACCOUNT like '" + Agent + "%' and login!='" + Agent + "') group by login order by login";
            //}
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "mt4");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new MT4_UsersInfo {
                        MT4_UsersLogin = mDr["login"].ToString(),//账号
                        MT4_UsersName = mDr["accountName"].ToString(),//账户名称
             //   if (reader["AgentCount"].ToString() == null || reader["AgentCount"].ToString() == "")
                        MT4_UsersRoleCount = (string.IsNullOrEmpty(mDr["AgentCount"].ToString() + string.Empty)) ? "0" : mDr["AgentCount"].ToString(),
                        MT4_UsersBALANCE = mDr["BALANCE"].ToString(),//余额
                        MT4_UsersPhone = mDr["PHONE"].ToString(),//电话
                        MT4_UsersEmail = mDr["EMAIL"].ToString()//邮箱
                    });
                }
            }
            catch
            { }
            return list;
        }
        
        public List<MT4_Trades> GetOrders(int pageSize, int pageOffset, string stime, string etime, string login, string search, out int total)
        {
            string sql = "";
            string _sql = "";
            if (!String.IsNullOrEmpty(search) && !String.IsNullOrEmpty(stime))//按照搜索条件和时间查询
            {
                sql = string.Format("SELECT * FROM mt4_trades WHERE LOGIN IN({0}) AND OPEN_TIME>='{1}' AND CLOSE_TIME<'{2}' AND(LOGIN = '{3}' OR TICKET = '{4}') AND CMD IN(0,1) ORDER BY TICKET DESC  LIMIT {5},{6};", login, stime, etime, search, search, pageOffset, pageSize);
                _sql = string.Format("SELECT COUNT(*) FROM mt4_trades WHERE LOGIN IN({0}) AND OPEN_TIME>='{1}' AND CLOSE_TIME<'{2}' AND (LOGIN='{3}' OR TICKET='{4}') AND CMD IN(0,1);", login, stime, etime, search, search);
            }
            else if (!String.IsNullOrEmpty(search) && String.IsNullOrEmpty(stime))//按照搜索条件查询
            {
                sql = string.Format("SELECT * FROM mt4_trades WHERE LOGIN IN({0})  AND(LOGIN = '{1}' OR TICKET = '{2}') AND CMD IN(0,1) ORDER BY TICKET DESC  LIMIT {3},{4};", login, search, search, pageOffset, pageSize);
                _sql = string.Format("SELECT COUNT(*) FROM mt4_trades WHERE LOGIN IN({0}) AND (LOGIN='{1}' OR TICKET='{2}') AND CMD IN(0,1);", login, search, search);
            }
            else if (String.IsNullOrEmpty(search) && !String.IsNullOrEmpty(stime))//按照搜时间查询
            {
                sql = string.Format("SELECT * FROM mt4_trades WHERE LOGIN IN({0}) AND OPEN_TIME>='{1}' AND CLOSE_TIME<'{2}' AND CMD IN(0,1) ORDER BY TICKET DESC  LIMIT {3},{4};", login, stime, etime, pageOffset, pageSize);
                _sql = string.Format("SELECT COUNT(*) FROM mt4_trades WHERE LOGIN IN({0}) AND OPEN_TIME>='{1}' AND CLOSE_TIME<'{2}' AND CMD IN(0,1);", login, stime, etime);
            }
            else//默认查询所有
            {
                sql = string.Format("SELECT * FROM mt4_trades WHERE LOGIN IN({0}) AND CMD IN(0,1) ORDER BY TICKET DESC  LIMIT {1},{2};", login, pageOffset, pageSize);
                _sql = string.Format("SELECT COUNT(*) FROM mt4_trades WHERE LOGIN IN({0}) AND CMD IN(0,1);", login);
            }
            List<MT4_Trades> list = new List<MT4_Trades>();
            MT4_Trades trades = null;

            total = 0;

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "mt4");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    trades = new MT4_Trades
                    {
                        TICKET = mDr["TICKET"].ToString(),//票据
                        LOGIN = mDr["LOGIN"].ToString(),//账户
                        SYMBOL = mDr["SYMBOL"].ToString(),//货币对
                        DIGITS = mDr["DIGITS"].ToString(),//位，数字，指
                        VOLUME = (Convert.ToDouble(mDr["VOLUME"].ToString()) / 100).ToString(),//交易量/手数
                        OPEN_TIME = mDr["OPEN_TIME"].ToString(),//下单时间
                        OPEN_PRICE = mDr["OPEN_PRICE"].ToString(),//开始价格
                        SL = mDr["SL"].ToString(),//止损
                        TP = mDr["TP"].ToString(),//获利
                        CLOSE_TIME = mDr["CLOSE_TIME"].ToString(),//结束时间
                        EXPIRATION = mDr["EXPIRATION"].ToString(),//截止时间
                        REASON = mDr["REASON"].ToString(),//原因
                        CONV_RATE1 = mDr["CONV_RATE1"].ToString(),//未知
                        CONV_RATE2 = mDr["CONV_RATE2"].ToString(),//未知
                        COMMISSION = mDr["COMMISSION"].ToString(),//佣金
                        COMMISSION_AGENT = mDr["COMMISSION_AGENT"].ToString(),//佣金代理人
                        SWAPS = mDr["SWAPS"].ToString(),//利息
                        CLOSE_PRICE = mDr["CLOSE_PRICE"].ToString(),//收盘价
                        PROFIT = mDr["PROFIT"].ToString(),//利润
                        TAXES = mDr["TAXES"].ToString(),//税
                        COMMENT = mDr["COMMENT"].ToString(),//注解
                        INTERNAL_ID = mDr["INTERNAL_ID"].ToString(),//内部ID
                        MARGIN_RATE = mDr["MARGIN_RATE"].ToString(),//保证金率
                        TIMESTAMP = mDr["TIMESTAMP"].ToString(),//时间戳
                        MODIFY_TIME = mDr["MODIFY_TIME"].ToString()//修改时间
                    };
                    
                    //类型
                    switch (mDr["CMD"].ToString())
                    {
                        case "0":
                            trades.CMD = "buy";
                            break;
                        case "1":
                            trades.CMD = "sell";
                            break;
                        case "2":
                            trades.CMD = "buy limit";
                            break;
                        case "3":
                            trades.CMD = "sell limit";
                            break;
                        case "4":
                            trades.CMD = "buy stop";
                            break;
                        case "5":
                            trades.CMD = "sell stop";
                            break;
                        case "6":
                            trades.CMD = "balance";
                            break;
                        case "7":
                            trades.CMD = "credit";
                            break;
                    }
                    list.Add(trades);
                }
                total = int.Parse(ws_mysql.ExecuteScalar(param.ToArray(), "", _sql, "mt4"));
            }
            catch
            { }
            return list;
        }

        /// <summary>
        /// 根据账户获取账户统计信息
        /// </summary>
        /// <param name="accountList">账户集合</param>
        /// <returns>账户统计信息</returns>
        public List<MT4AccountStatisticalInfo> GetAccountStatisticalInfo(string accountList, string startDate, string endDate)
        {
            List<MT4AccountStatisticalInfo> list = new List<MT4AccountStatisticalInfo>();
            if (accountList == null || accountList == "")
            {
                return null;
            }
            if (startDate == null || startDate == "")
            {
                startDate = "1970-01-01 00:00:00";
            }
            if (endDate == null || endDate == "")
            {
                endDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            string sql = "select LOGIN,sum(case when CMD=6 and COMMENT='Deposit' then PROFIT end) as DepositCount,sum(case when CMD=6 and comment='Withdrawal' then PROFIT end) as WithdrawalCount,sum(case when CMD in (0,1) then VOLUME end) as VOLUMECount,sum(COMMISSION) as COMMISSION,(select EQUITY from mt4_users where LOGIN = mt4T.LOGIN ) AS EQUITY,sum(SWAPS)as SWAPS,(select BALANCE from mt4_users where LOGIN = mt4T.LOGIN ) AS BALANCE,(select CREDIT from mt4_users where LOGIN = mt4T.LOGIN ) AS CREDIT from mt4_trades as mt4T where login IN (" + accountList + ") and OPEN_TIME > '" + startDate + "' and OPEN_TIME <= '" + endDate + "' group by login order by login desc";
            //string sql = "select LOGIN,sum(case when COMMENT='Deposit' then PROFIT end) as DepositCount,sum(case when COMMENT='Withdrawal' then PROFIT end) as WithdrawalCount,sum(case when CMD in (0,1) then VOLUME end) as VOLUMECount,sum(COMMISSION) as COMMISSION,(select EQUITY from mt4_users where LOGIN = mt4T.LOGIN ) AS EQUITY,sum(SWAPS)as SWAPS,(select BALANCE from mt4_users where LOGIN = mt4T.LOGIN ) AS BALANCE,(select CREDIT from mt4_users where LOGIN = mt4T.LOGIN ) AS CREDIT from mt4_trades as mt4T where login IN (" + accountList + ") and OPEN_TIME > '" + startCountDate + "' and OPEN_TIME < '" + endCountDate + "' group by login order by login desc";

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "mt4");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new MT4AccountStatisticalInfo {
                        //账户
                        LOGIN = mDr["LOGIN"].ToString(),
                        //总入金数                                
                        DepositCount = (string.IsNullOrEmpty(mDr["DepositCount"].ToString() + string.Empty)) ? "0" : mDr["DepositCount"].ToString(),
                        //总出金数
                        WithdrawalCount = (string.IsNullOrEmpty(mDr["WithdrawalCount"].ToString() + string.Empty)) ? "0" : mDr["WithdrawalCount"].ToString(),
                        //已经成交总交易量
                        VOLUMECount = (string.IsNullOrEmpty(mDr["VOLUMECount"].ToString() + string.Empty)) ? "0" : Convert.ToString(Convert.ToDouble(mDr["VOLUMECount"].ToString()) / 100),
                        //总佣金
                        COMMISSION = (string.IsNullOrEmpty(mDr["COMMISSION"].ToString() + string.Empty)) ? "0" : mDr["COMMISSION"].ToString(),
                        //账户净值
                        EQUITY = (string.IsNullOrEmpty(mDr["EQUITY"].ToString() + string.Empty)) ? "0" : mDr["EQUITY"].ToString(),
                        //交易总利息
                        SWAPS = (string.IsNullOrEmpty(mDr["SWAPS"].ToString() + string.Empty)) ? "0" : mDr["SWAPS"].ToString(),
                        //账户余额
                        BALANCE = (string.IsNullOrEmpty(mDr["BALANCE"].ToString() + string.Empty)) ? "0" : mDr["BALANCE"].ToString(),
                        //账户信用额度
                        CREDIT = (string.IsNullOrEmpty(mDr["CREDIT"].ToString() + string.Empty)) ? "0" : mDr["CREDIT"].ToString()
                    });
                }
            }
            catch
            { }
            return list;
        }

    }
}
