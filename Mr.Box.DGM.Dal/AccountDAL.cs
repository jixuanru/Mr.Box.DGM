using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using Mr.Box.DGM.Common;
using Mr.Box.DGM.Model;
using System.Web.Configuration;

namespace Mr.Box.DGM.Dal
{
    public class AccountDAL
    {
        List<string> param = new List<string>();

        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();
        com.dgmfx.ws.common.WS_Common ws_comm = new com.dgmfx.ws.common.WS_Common();

        // string updateSql = "update Account set OnlineStart = @OnlineStart, AccountName = @AccountName, AccountPassword = @AccountPassword, Nickname = @Nickname, Country = @Country, CH_Surname = @CH_Surname, EN_Surname = @EN_Surname, CH_Name = @CH_Name, EN_Name = @EN_Name, RegDate = @RegDate, Email = @Email, Agents = @Agents, IsBuy = @IsBuy, OnlineRoomPwd = @OnlineRoomPwd, CurrencyTypeId = @CurrencyTypeId, AccountType = @AccountType, OnlineEnd = @OnlineEnd, InformationId = @InformationId, Explanation = @Explanation , AccountBelongsPlatformType = @AccountBelongsPlatformType where AccountId=@AccountId";


        /// <summary>
        /// 添加账户信息
        /// </summary>
        /// <param name="Model">账户信息</param>
        /// <returns>新添加的用户信息</returns>
        public Account AddAccount(Account Model)
        {
            string addSql = @"insert into Account values (AccountId,@AccountName, @AccountPassword, @Nickname, @Country, @CH_Surname, @EN_Surname, @CH_Name, @EN_Name, @Email, @Agents, @IsBuy, @OnlineRoomPwd,@RegDate,@OnlineStart, @OnlineEnd, @CurrencyTypeId, @AccountType,@AccountTradeType, @InformationId, @Explanation ,0, @AccountBelongsPlatformType,'');
                              select @@identity";

            param.Add("[@AccountName],[" + Model.AccountName + "]");
            param.Add("[@AccountPassword],[" + Model.AccountPassword + "]");
            param.Add("[@Nickname],[" + Model.Nickname + "]");
            param.Add("[@Country],[" + Model.Country + "]");
            param.Add("[@CH_Surname],[" + Model.CH_Surname + "]");
            param.Add("[@EN_Surname],[" + Model.EN_Surname + "]");
            param.Add("[@CH_Name],["+Model.CH_Name+"]");
            param.Add("[@EN_Name],["+Model.EN_Name+"]");
            param.Add("[@Email],[" + Model.Email + "]");
            param.Add("[@Agents],[" + Model.Agents + "]");
            param.Add("[@IsBuy],[" + Model.IsBuy + "]");
            param.Add("[@OnlineRoomPwd],[" + Model.OnlineRoomPwd + "]");
            param.Add("[@RegDate],[" + Model.RegDate + "]");
            param.Add("[@OnlineStart],[" + Model.OnlineStart + "]");
            param.Add("[@OnlineEnd],[" + Model.OnlineEnd + "]");
            param.Add("[@CurrencyTypeId],[" + Model.CurrencyType + "]");
            param.Add("[@AccountType],[" + Model.AccountType + "]");
            param.Add("[@AccountTradeType],["+Model.AccountTradeType+"]");
            param.Add("[@InformationId],[" + Model.InformationId + "]");
            param.Add("[@Explanation],[" + Model.Explanation + "]");
            param.Add("[@AccountBelongsPlatformType],[" + Model.AccountBelongsPlatformType + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME,PublicConst.WS_USERPWD);
                string obj = ws_mysql.ExecuteScalar(param.ToArray(), "", addSql, "dgm");
                if (string.IsNullOrEmpty(obj))
                {
                    return null;
                }
                else
                {
                    Account AccountInfo = GetAccountByAccountID(Int32.Parse(obj));
                    return AccountInfo;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                param.Clear();
            }
        }


        /// <summary>
        /// 复制当前内容
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public bool AccountCopy(int accId)
        {
            string sql =$@"INSERT INTO Account(AccountName,AccountPassword,Nickname,Country,CH_Surname,EN_Surname,CH_Name,EN_Name,Email,Agents,IsBuy,OnlineRoomPwd,RegDate,OnlineStart,OnlineEnd,CurrencyTypeId,AccountType,AccountTradeType,InformationId,Explanation,IsDisable,AccountBelongsPlatformType,WeChat) 
                                       SELECT AccountName,AccountPassword,Nickname,Country,CH_Surname,EN_Surname,CH_Name,EN_Name,Email,Agents,IsBuy,OnlineRoomPwd,now() as RegDate,OnlineStart,OnlineEnd,CurrencyTypeId,AccountType,AccountTradeType,InformationId,Explanation,IsDisable,AccountBelongsPlatformType,WeChat FROM Account where AccountId={accId}";
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                return ws_mysql.ExecuteNonQuery(param.ToArray(), "", sql, "dgm") > 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据账户Id查询账户
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public Account GetAccountByAccountID(int AccountID)
        {
            string sql = $"select * from Account where AccountId={AccountID}";
            Account AccountInfo = null;
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    AccountInfo = new Account
                    {
                        AccountId = AccountID,
                        OnlineStart = mDr["OnlineStart"].ToString(),
                        AccountName = mDr["AccountName"].ToString(),
                        AccountPassword = mDr["AccountPassword"].ToString(),
                        Nickname = mDr["Nickname"].ToString(),
                        Country = mDr["Country"].ToString(),
                        CH_Surname = mDr["CH_Surname"].ToString(),
                        EN_Surname = mDr["EN_Surname"].ToString(),
                        CH_Name = mDr["CH_Name"].ToString(),
                        EN_Name = mDr["EN_Name"].ToString(),
                        RegDate = mDr["RegDate"].ToString(),
                        Email = mDr["Email"].ToString(),
                        Agents = mDr["Agents"].ToString(),
                        IsBuy = mDr["IsBuy"].ToString(),
                        OnlineRoomPwd = mDr["OnlineRoomPwd"].ToString(),
                        CurrencyType = mDr["CurrencyTypeId"].ToString(),
                        AccountType = mDr["AccountType"].ToString(),
                        AccountTradeType=mDr["AccountTradeType"].ToString(),
                        OnlineEnd = mDr["OnlineEnd"].ToString(),
                        InformationId = mDr["InformationId"].ToString(),
                        Explanation = mDr["Explanation"].ToString(),
                        AccountBelongsPlatformType = mDr["AccountBelongsPlatformType"].ToString()
                    };
                }

            }
            catch
            {
                
            }
            return AccountInfo;
        }

        /// <summary>
        /// 根据账户Name查询账户
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <returns></returns>
        public Account GetAccountByAccountNumberLogin(string AccountNumber)
        {
            Account AccountInfo = null;
            string sql = $"select * from Account where AccountName ='{AccountNumber}' ";
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    AccountInfo = new Account
                    {
                        AccountId =Int32.Parse(mDr["AccountID"].ToString()),
                        OnlineStart = mDr["OnlineStart"].ToString(),
                        AccountName = mDr["AccountName"].ToString(),
                        AccountPassword = mDr["AccountPassword"].ToString(),
                        Nickname = mDr["Nickname"].ToString(),
                        Country = mDr["Country"].ToString(),
                        CH_Surname = mDr["CH_Surname"].ToString(),
                        EN_Surname = mDr["EN_Surname"].ToString(),
                        CH_Name = mDr["CH_Name"].ToString(),
                        EN_Name = mDr["EN_Name"].ToString(),
                        RegDate = mDr["RegDate"].ToString(),
                        Email = mDr["Email"].ToString(),
                        Agents = mDr["Agents"].ToString(),
                        IsBuy = mDr["IsBuy"].ToString(),
                        OnlineRoomPwd = mDr["OnlineRoomPwd"].ToString(),
                        CurrencyType = mDr["CurrencyTypeId"].ToString(),
                        AccountType = mDr["AccountType"].ToString(),
                        AccountTradeType=mDr["AccountTradeType"].ToString(),
                        OnlineEnd = mDr["OnlineEnd"].ToString(),
                        InformationId = mDr["InformationId"].ToString(),
                        Explanation = mDr["Explanation"].ToString(),
                        AccountBelongsPlatformType = mDr["AccountBelongsPlatformType"].ToString()
                    };
                }
            }
            catch
            {

            }
            return AccountInfo;
        }


        /// <summary>
        /// 根据账户Id查询账户
        /// </summary>
        /// <param name="AccountNumber"></param>
        /// <returns></returns>
        public Account GetAccountByAccountNumber(string AccountNumber)
        {
            Account AccountInfo = null;
          
            try
            {
                string selSqlByaccount = "select * from Account where AccountName = '" + AccountNumber + "'";
                //string selSqlByaccount = "select * from [dbo].[Account] where AccountName like '%" + AccountNumber + "%'";

                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(selSqlByaccount, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    AccountInfo = new Account
                    {
                        AccountId = Int32.Parse(mDr["AccountID"].ToString()),
                        OnlineStart = mDr["OnlineStart"].ToString(),
                        AccountName = mDr["AccountName"].ToString(),
                        AccountPassword = mDr["AccountPassword"].ToString(),
                        Nickname = mDr["Nickname"].ToString(),
                        Country = mDr["Country"].ToString(),
                        CH_Surname = mDr["CH_Surname"].ToString(),
                        EN_Surname = mDr["EN_Surname"].ToString(),
                        CH_Name = mDr["CH_Name"].ToString(),
                        EN_Name = mDr["EN_Name"].ToString(),
                        RegDate = mDr["RegDate"].ToString(),
                        Email = mDr["Email"].ToString(),
                        Agents = mDr["Agents"].ToString(),
                        IsBuy = mDr["IsBuy"].ToString(),
                        OnlineRoomPwd = mDr["OnlineRoomPwd"].ToString(),
                        CurrencyType = mDr["CurrencyTypeId"].ToString(),
                        AccountType = mDr["AccountType"].ToString(),
                        AccountTradeType=mDr["AccountTradeType"].ToString(),
                        OnlineEnd = mDr["OnlineEnd"].ToString(),
                        InformationId = mDr["InformationId"].ToString(),
                        Explanation = mDr["Explanation"].ToString(),
                        AccountBelongsPlatformType = mDr["AccountBelongsPlatformType"].ToString()
                    };
                }
            }
            catch
            {

            }
            return AccountInfo;
        }


        public List<Account> GetAccount(int offset, int pageSize, string search, string type, out int total)
        {
            List<Account> list = new List<Account>();
            string sql = string.Empty;
            string _sql = string.Empty;

            if (String.IsNullOrEmpty(search))
            {
                if (type == "回收站")
                {
                    sql =
                        $"SELECT AccountId,AccountName,AccountPassword,Email,Agents,Explanation,IsDisable,RegDate,AccountBelongsPlatformType FROM Account WHERE IsDisable=1 ORDER BY AccountId DESC Limit {offset},{pageSize};";
                    _sql = " SELECT COUNT(IsDisable) FROM Account WHERE IsDisable=1;";
                }
                else
                {
                    sql =
                        $"SELECT AccountId,AccountName,AccountPassword,Email,Agents,Explanation,IsDisable,RegDate,AccountBelongsPlatformType FROM Account WHERE IsDisable=0 ORDER BY AccountId DESC Limit {offset},{pageSize};";
                    _sql = " SELECT COUNT(IsDisable) FROM Account WHERE IsDisable=0;";
                }
            }
            else
            {
                if (type == "回收站")
                {
                    sql =
                        $"SELECT AccountId,AccountName,AccountPassword,Email,Agents,Explanation,IsDisable,RegDate,AccountBelongsPlatformType FROM Account WHERE IsDisable=1  AND (AccountName LIKE '%{search}%' OR Explanation LIKE '%{search}%') ORDER BY AccountId DESC Limit {offset},{pageSize};";
                    _sql =
                        $" SELECT COUNT(IsDisable) FROM Account WHERE IsDisable=1 AND (AccountName LIKE '%{search}%' OR Explanation LIKE '%{search}%');";
                }
                else
                {
                    sql =
                        $"SELECT AccountId,AccountName,AccountPassword,Email,Agents,Explanation,IsDisable,RegDate,AccountBelongsPlatformType FROM Account WHERE IsDisable=0  AND (AccountName LIKE '%{search}%' OR Explanation LIKE '%{search}%') ORDER BY AccountId DESC Limit {offset},{pageSize};";
                    _sql =
                        $" SELECT COUNT(IsDisable) FROM Account WHERE IsDisable=0 AND (AccountName LIKE '%{search}%' OR Explanation LIKE '%{search}%');";
                }
            }

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new Account {
                        AccountId = int.Parse(mDr["AccountId"].ToString()),
                        AccountName = mDr["AccountName"].ToString(),
                        AccountPassword = mDr["AccountPassword"].ToString(),
                        Email = mDr["Email"].ToString(),
                        Agents = mDr["Agents"].ToString(),
                        Explanation = mDr["Explanation"].ToString(),
                        RegDate = mDr["RegDate"].ToString(),
                        AccountBelongsPlatformType = mDr["AccountBelongsPlatformType"].ToString()
                    });
                }
                total = int.Parse(ws_mysql.ExecuteScalar(param.ToArray(), "", _sql, "dgm"));
            }
            catch
            {
                total = 0;
            }
            return list;
        }

        public bool EditAccount(Account acc,bool SendMail)
        {
            string sql = $"UPDATE Account SET AccountName='{acc.AccountName}',AccountPassword='{acc.AccountPassword}',Explanation='{acc.Explanation}',Agents='{acc.Agents}' WHERE AccountId={acc.AccountId}";
            string strAccountPWD = "";
            string strEName = "";
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                ws_comm.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);

                if (SendMail && !string.IsNullOrEmpty(acc.Email + string.Empty))
                {
                    strAccountPWD = acc.EN_Surname.Substring(0, 1).ToLower() + ws_comm.GetSpellCode(acc.CH_Name).ToLower() + "123456";
                    strEName = acc.EN_Surname + acc.EN_Name;
                    EmailHelper email = new EmailHelper(acc.Email, "< Decoder Global Markets ltd > User Account Activation Notification", "", Encoding.UTF8, strEName, acc.AccountName, strAccountPWD);
                    email.Send();
                }
                return ws_mysql.ExecuteNonQuery(param.ToArray(), "", sql, "dgm") > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveAccount(int id)
        {
            string sql = $"UPDATE Account SET IsDisable=1 WHERE AccountId={id}";
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                return ws_mysql.ExecuteNonQuery(param.ToArray(), "", sql, "dgm") > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool RecoveryRecycle(int id)
        {
            string _sql = $"update Account set IsDisable=0 where AccountId={id}";
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                return ws_mysql.ExecuteNonQuery(param.ToArray(), "", _sql, "dgm") > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteRecycle(int id)
        {
            string _sql = $"delete from  Account where AccountId={id}";
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                return ws_mysql.ExecuteNonQuery(param.ToArray(), "", _sql, "dgm") > 0;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 开设账户时  验证邮箱是否存在
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool EmailValidate(string email, string name)
        {
            string sql = string.Format("SELECT COUNT(EMAIL) FROM Account WHERE TRIM(LOWER(EMAIL))='{0}'", email);
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                bool flag = int.Parse(ws_mysql.ExecuteScalar(param.ToArray(), "", sql, "dgm")) > 0;
                if(flag)
                {
                    string _sql = string.Format("SELECT COUNT(Email) FROM Account WHERE TRIM(LOWER(Email))='{0}' AND Explanation LIKE '%{1}%'", email, name);
                    return int.Parse(ws_mysql.ExecuteScalar(param.ToArray(), "", _sql, "dgm")) > 0;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 获取一定时间段内新增的客户
        /// </summary>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <returns></returns>
        public string cdc_Account_CT(string stime, string etime)
        {
            string user = "";
            string strSQL = $"SELECT * FROM `Account` WHERE `IsDisable`=0 AND `AccountName`='' AND `RegDate` between '{stime}' AND '{etime}';";

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(strSQL, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    if(string.IsNullOrEmpty(mDr["AccountName"].ToString()))
                    {
                        user += "[" + mDr["Explanation"] + "]  ";
                    }
                }
            }
            catch
            { }
            return user;
        }

    }
}
