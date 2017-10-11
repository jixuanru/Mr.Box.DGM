using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;
using System.Web.Configuration;

namespace Mr.Box.DGM.Dal
{
    public class AccountWithdrawalsDAL
    {
        List<string> param = new List<string>();

        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();

        string addSql = "insert into AccountWithdrawals values (WithdrawalsId,@WithdrawalsAccountHolder, @WithdrawalsAccount,@WithdrawalsDate, @Phone, @WithdrawalsEmail, @WithdrawalsAddress, @WithdrawalsCountry, @WithdrawalsProvince, @WithdrawalsCity, @WithdrawalsCityZipCode, @Money, @BelongsPlatform, @IsCloseAccount, @CloseReason, @BankName, @BankAccount, @BankHolder, @BankAddress, @SWIFT_Code, @BankCountry, @BankProvince, @BankCity, @BankCityZipCode, @IsTransitBank, @TransitBankName, @TransitBankAccount, @TransitBankHolder, @TransitBankAddress, @TransitBankSWIFICode, @TransitBankCountry, @TransitBankProvince, @TransitBankCity, @TransitBankCityZipCode, @MainAccountName, @MinorAccountName, @Explanation,@ProcessingStatus);select @@identity";
        string deleteSql = "delete from AccountWithdrawals where WithdrawalsId=@WithdrawalsId";
        //string updateSql = "update AccountWithdrawals set  WithdrawalsAccount = @WithdrawalsAccountHolder, WithdrawalsAccount = @WithdrawalsAccount,WithdrawalsDate = @WithdrawalsDate, Phone = @Phone,  WithdrawalsEmail = @WithdrawalsEmail,  WithdrawalsAddress = @WithdrawalsAddress,  WithdrawalsCountry = @WithdrawalsCountry,  WithdrawalsProvince = @WithdrawalsProvince,  WithdrawalsCity = @WithdrawalsCity,  WithdrawalsCityZipCode = @WithdrawalsCityZipCode,  Money = @Money,  BelongsPlatform = @BelongsPlatform,  IsCloseAccount = @IsCloseAccount,  CloseReason = @CloseReason,  BankName = @BankName,  BankAccount = @BankAccount,  BankHolder = @BankHolder,  BankAddress = @BankAddress,  SWIFT_Code = @SWIFT_Code,  BankCountry = @BankCountry,  BankProvince = @BankProvince,  BankCity = @BankCity,  BankCityZipCode = @BankCityZipCode,  IsTransitBank = @IsTransitBank,  TransitBankName = @TransitBankName,  TransitBankAccount = @TransitBankAccount,  TransitBankHolder = @TransitBankHolder,  TransitBankAddress = @TransitBankAddress,  TransitBankSWIFICode = @TransitBankSWIFICode,  TransitBankCountry = @TransitBankCountry,  TransitBankProvince = @TransitBankProvince,  TransitBankCity = @TransitBankCity,  TransitBankCityZipCode = @TransitBankCityZipCode,  MainAccountName = @MainAccountName,  MinorAccountName = @MinorAccountName,  Explanation = @Explanation,ProcessingStatus = @ProcessingStatus where WithdrawalsId=@WithdrawalsId";
        string selSql = "select * from AccountWithdrawals where WithdrawalsId=@WithdrawalsId";
        //string sql = "select * from AccountWithdrawals order by  WithdrawalsId desc";

        string updateStatusSql = "update AccountWithdrawals set ProcessingStatus ='已处理' where WithdrawalsId = @WithdrawalsId";

        /// <summary>
        /// 根据取款信息ID修改
        /// </summary>
        /// <param name="WithdrawalsId">取款信息ID</param>
        /// <returns>返回取款交易账号</returns>
        public LOG UpdateWithdrawalsStatus(string WithdrawalsId)
        {
            LOG log = new LOG();
            param.Add("[@WithdrawalsId],[" + WithdrawalsId + "]");
            string _sql = "select WithdrawalsAccount,Money from AccountWithdrawals where WithdrawalsId=@WithdrawalsId";

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", _sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    log.Account = mDr["WithdrawalsAccount"].ToString();
                    log.Money = mDr["Money"].ToString();
                }
                int num = ws_mysql.ExecuteNonQuery(param.ToArray(), "", updateStatusSql, "dgm");
            }
            catch
            {
                log = null;
            }
            finally
            {
                param.Clear();
            }
            return log;
        }



        /// <summary>
        /// 根据当前登录用户，查询最近一次出金记录  有记录就返回 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public List<AccountWithdrawals> GetAccountWithdrawInfo(string account)
        {
            List<AccountWithdrawals> list = new List<AccountWithdrawals>();
            string _sql = $"select * from AccountWithdrawals where WithdrawalsAccount='{account}' order by WithdrawalsId desc limit 1;";

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", _sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new AccountWithdrawals
                    {
                        WithdrawalsId = mDr["WithdrawalsId"].ToString(),
                        WithdrawalsAccountHolder = mDr["WithdrawalsAccountHolder"].ToString(),
                        WithdrawalsAccount = mDr["WithdrawalsAccount"].ToString(),
                        WithdrawalsDate = mDr["WithdrawalsDate"].ToString(),
                        Phone = mDr["Phone"].ToString(),
                        WithdrawalsEmail = mDr["WithdrawalsEmail"].ToString(),
                        WithdrawalsAddress = mDr["WithdrawalsAddress"].ToString(),
                        WithdrawalsCountry = mDr["WithdrawalsCountry"].ToString(),
                        WithdrawalsProvince = mDr["WithdrawalsProvince"].ToString(),
                        WithdrawalsCity = mDr["WithdrawalsCity"].ToString(),
                        WithdrawalsCityZipCode = mDr["WithdrawalsCityZipCode"].ToString(),
                        Money = mDr["Money"].ToString(),
                        BelongsPlatform = mDr["BelongsPlatform"].ToString(),
                        IsCloseAccount = mDr["IsCloseAccount"].ToString(),
                        CloseReason = mDr["CloseReason"].ToString(),
                        BankName = mDr["BankName"].ToString(),
                        BankAccount = mDr["BankAccount"].ToString(),
                        BankHolder = mDr["BankHolder"].ToString(),
                        BankAddress = mDr["BankAddress"].ToString(),
                        SWIFT_Code = mDr["SWIFT_Code"].ToString(),
                        BankCountry = mDr["BankCountry"].ToString(),
                        BankProvince = mDr["BankProvince"].ToString(),
                        BankCity = mDr["BankCity"].ToString(),
                        BankCityZipCode = mDr["BankCityZipCode"].ToString(),
                        IsTransitBank = mDr["IsTransitBank"].ToString(),
                        TransitBankName = mDr["TransitBankName"].ToString(),
                        TransitBankAccount = mDr["TransitBankAccount"].ToString(),
                        TransitBankHolder = mDr["TransitBankHolder"].ToString(),
                        TransitBankAddress = mDr["TransitBankAddress"].ToString(),
                        TransitBankSWIFICode = mDr["TransitBankSWIFICode"].ToString(),
                        TransitBankCountry = mDr["TransitBankCountry"].ToString(),
                        TransitBankProvince = mDr["TransitBankProvince"].ToString(),
                        TransitBankCity = mDr["TransitBankCity"].ToString(),
                        TransitBankCityZipCode = mDr["TransitBankCityZipCode"].ToString(),
                        MainAccountName = mDr["MainAccountName"].ToString(),
                        MinorAccountName = mDr["MinorAccountName"].ToString(),
                        Explanation = mDr["Explanation"].ToString(),
                        ProcessingStatus = mDr["ProcessingStatus"].ToString(),
                    });
                }
            }
            catch
            { }
            return list;
        }


        /// <summary>
        /// 添加取款信息
        /// </summary>
        /// <param name="Model">取款信息</param>
        /// <returns>新添加的取款信息</returns>
        public AccountWithdrawals AddAccount(AccountWithdrawals Model)
        {
            AccountWithdrawals accountWithdrawals = null;

            param.Add("[@WithdrawalsAccountHolder],[" + Model.WithdrawalsAccountHolder + "]");
            param.Add("[@WithdrawalsAccount],[" + Model.WithdrawalsAccount + "]");
            param.Add("[@WithdrawalsDate],[" + Model.WithdrawalsDate + "]");
            param.Add("[@Phone],[" + Model.Phone + "]");
            param.Add("[@WithdrawalsEmail],[" + Model.WithdrawalsEmail + "]");
            param.Add("[@WithdrawalsAddress],[" + Model.WithdrawalsAddress + "]");
            param.Add("[@WithdrawalsCountry],[" + Model.WithdrawalsCountry + "]");
            param.Add("[@WithdrawalsProvince],[" + Model.WithdrawalsProvince + "]");
            param.Add("[@WithdrawalsCity],[" + Model.WithdrawalsCity + "]");
            param.Add("[@WithdrawalsCityZipCode],[" + Model.WithdrawalsCityZipCode + "]");
            param.Add("[@Money],[" + Model.Money + "]");
            param.Add("[@BelongsPlatform],[" + Model.BelongsPlatform + "]");
            param.Add("[@IsCloseAccount],[" + Model.IsCloseAccount + "]");
            param.Add("[@CloseReason],[" + Model.CloseReason + " ]");
            param.Add("[@BankName],[" + Model.BankName + "]");
            param.Add("[@BankAccount],[" + Model.BankAccount + "]");
            param.Add("[@BankHolder],[" + Model.BankHolder + "]");
            param.Add("[@BankAddress],[" + Model.BankAddress + "]");
            param.Add("[@SWIFT_Code],[" + Model.SWIFT_Code + "]");
            param.Add("[@BankCountry],[" + Model.BankCountry + "]");
            param.Add("[@BankProvince],[" + Model.BankProvince + "]");
            param.Add("[@BankCity],[" + Model.BankCity + "]");
            param.Add("[@BankCityZipCode],[" + Model.BankCityZipCode + "]");
            param.Add("[@IsTransitBank],[" + Model.IsTransitBank + "]");
            param.Add("[@TransitBankName],[" + Model.TransitBankName + "]");
            param.Add("[@TransitBankAccount],[" + Model.TransitBankAccount + "]");
            param.Add("[@TransitBankHolder],[" + Model.TransitBankHolder + "]");
            param.Add("[@TransitBankAddress],[" + Model.TransitBankAddress + "]");
            param.Add("[@TransitBankSWIFICode],[" + Model.TransitBankSWIFICode + "]");
            param.Add("[@TransitBankCountry],[" + Model.TransitBankCountry + "]");
            param.Add("[@TransitBankProvince],[" + Model.TransitBankProvince + "]");
            param.Add("[@TransitBankCity],[" + Model.TransitBankCity + "]");
            param.Add("[@TransitBankCityZipCode],[" + Model.TransitBankCityZipCode + "]");
            param.Add("[@MainAccountName],[" + Model.MainAccountName + "]");
            param.Add("[@MinorAccountName],[" + Model.MinorAccountName + "]");
            param.Add("[@Explanation],[" + Model.Explanation + "]");
            param.Add("[@ProcessingStatus],[" + Model.ProcessingStatus + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                string obj = ws_mysql.ExecuteScalar(param.ToArray(), "", addSql, "dgm");
                if (!string.IsNullOrEmpty(obj))
                {
                    accountWithdrawals = GetAccountWithdrawalsByWithdrawalsId(Convert.ToString(obj));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                param.Clear();
            }
            return accountWithdrawals;
        }

        /// <summary>
        /// 根据取款信息ID删除取款信息
        /// </summary>
        /// <param name="WithdrawalsId">取款信息ID</param>
        /// <returns>返回取款交易账号</returns>
        public LOG DeleteCheckAccountWithdrawals(int WithdrawalsId)
        {
            param.Add("[@WithdrawalsId],[" + WithdrawalsId + "]");

            string _sql = "select WithdrawalsAccount,Money from AccountWithdrawals where WithdrawalsId=@WithdrawalsId";

            LOG log = new LOG();

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", _sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    log.Account = mDr["WithdrawalsAccount"].ToString();
                    log.Money = mDr["Money"].ToString();
                }
                int num = ws_mysql.ExecuteNonQuery(param.ToArray(), "", deleteSql, "dgm");
            }
            catch
            {

            }
            finally
            {
                param.Clear();
            }
            return log;
        }


        /// <summary>
        /// 根据取款信息Id查询取款信息
        /// </summary>
        /// <param name="ClassesID"></param>
        /// <returns></returns>
        public AccountWithdrawals GetAccountWithdrawalsByWithdrawalsId(string WithdrawalsId)
        {
            AccountWithdrawals accountWithdrawals = null;
            param.Add("[@WithdrawalsId],[" + WithdrawalsId + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", selSql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    accountWithdrawals = new AccountWithdrawals();

                    accountWithdrawals.WithdrawalsId = WithdrawalsId;
                    accountWithdrawals.WithdrawalsAccountHolder = mDr["WithdrawalsAccountHolder"].ToString();
                    accountWithdrawals.WithdrawalsAccount = mDr["WithdrawalsAccount"].ToString();
                    accountWithdrawals.WithdrawalsDate = mDr["WithdrawalsDate"].ToString();
                    accountWithdrawals.Phone = mDr["Phone"].ToString();
                    accountWithdrawals.WithdrawalsEmail = mDr["WithdrawalsEmail"].ToString();
                    accountWithdrawals.WithdrawalsAddress = mDr["WithdrawalsAddress"].ToString();
                    accountWithdrawals.WithdrawalsCountry = mDr["WithdrawalsCountry"].ToString();
                    accountWithdrawals.WithdrawalsProvince = mDr["WithdrawalsProvince"].ToString();
                    accountWithdrawals.WithdrawalsCity = mDr["WithdrawalsCity"].ToString();
                    accountWithdrawals.WithdrawalsCityZipCode = mDr["WithdrawalsCityZipCode"].ToString();
                    accountWithdrawals.Money = mDr["Money"].ToString();
                    accountWithdrawals.BelongsPlatform = mDr["BelongsPlatform"].ToString();
                    accountWithdrawals.IsCloseAccount = mDr["IsCloseAccount"].ToString();
                    accountWithdrawals.CloseReason = mDr["CloseReason"].ToString();
                    accountWithdrawals.BankName = mDr["BankName"].ToString();
                    accountWithdrawals.BankAccount = mDr["BankAccount"].ToString();
                    accountWithdrawals.BankHolder = mDr["BankHolder"].ToString();
                    accountWithdrawals.BankAddress = mDr["BankAddress"].ToString();
                    accountWithdrawals.SWIFT_Code = mDr["SWIFT_Code"].ToString();
                    accountWithdrawals.BankCountry = mDr["BankCountry"].ToString();
                    accountWithdrawals.BankProvince = mDr["BankProvince"].ToString();
                    accountWithdrawals.BankCity = mDr["BankCity"].ToString();
                    accountWithdrawals.BankCityZipCode = mDr["BankCityZipCode"].ToString();
                    accountWithdrawals.IsTransitBank = mDr["IsTransitBank"].ToString();
                    accountWithdrawals.TransitBankName = mDr["TransitBankName"].ToString();
                    accountWithdrawals.TransitBankAccount = mDr["TransitBankAccount"].ToString();
                    accountWithdrawals.TransitBankHolder = mDr["TransitBankHolder"].ToString();
                    accountWithdrawals.TransitBankAddress = mDr["TransitBankAddress"].ToString();
                    accountWithdrawals.TransitBankSWIFICode = mDr["TransitBankSWIFICode"].ToString();
                    accountWithdrawals.TransitBankCountry = mDr["TransitBankCountry"].ToString();
                    accountWithdrawals.TransitBankProvince = mDr["TransitBankProvince"].ToString();
                    accountWithdrawals.TransitBankCity = mDr["TransitBankCity"].ToString();
                    accountWithdrawals.TransitBankCityZipCode = mDr["TransitBankCityZipCode"].ToString();
                    accountWithdrawals.MainAccountName = mDr["MainAccountName"].ToString();
                    accountWithdrawals.MinorAccountName = mDr["MinorAccountName"].ToString();
                    accountWithdrawals.Explanation = mDr["Explanation"].ToString();
                    accountWithdrawals.ProcessingStatus = mDr["ProcessingStatus"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                param.Clear();
            }
            return accountWithdrawals;
        }


        public IList<AccountWithdrawals> GetAccountWithdrawals(int offset, int pageSize, string search, string stime, string etime, out int total)
        {
            IList<AccountWithdrawals> list = new List<AccountWithdrawals>();

            string sql = string.Format("SELECT WithdrawalsId,WithdrawalsAccountHolder,WithdrawalsAccount,Money,WithdrawalsDate,ProcessingStatus FROM AccountWithdrawals WHERE WithdrawalsDate >'{0}' AND WithdrawalsDate<= '{1}' AND(WithdrawalsAccountHolder LIKE'%{2}%' OR WithdrawalsAccount LIKE'%{3}%') ORDER BY WithdrawalsId DESC Limit {4},{5};", stime, etime, search, search, offset, pageSize);
            string _sql = string.Format("SELECT COUNT(WithdrawalsDate) FROM AccountWithdrawals  WHERE WithdrawalsDate >'{0}' AND WithdrawalsDate<= '{1}' AND(WithdrawalsAccountHolder LIKE '%{2}%' OR WithdrawalsAccount LIKE '%{3}%');", stime, etime, search, search);

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new AccountWithdrawals
                    {
                        WithdrawalsId = mDr["WithdrawalsId"].ToString(),
                        WithdrawalsAccountHolder = mDr["WithdrawalsAccountHolder"].ToString(),
                        WithdrawalsAccount = mDr["WithdrawalsAccount"].ToString(),
                        WithdrawalsDate = mDr["WithdrawalsDate"].ToString(),
                        //accountWithdrawals.Phone = Convert.ToString(reader["Phone"]);
                        //accountWithdrawals.WithdrawalsEmail = Convert.ToString(reader["WithdrawalsEmail"]);
                        //accountWithdrawals.WithdrawalsAddress = Convert.ToString(reader["WithdrawalsAddress"]);
                        //accountWithdrawals.WithdrawalsCountry = Convert.ToString(reader["WithdrawalsCountry"]);
                        //accountWithdrawals.WithdrawalsProvince = Convert.ToString(reader["WithdrawalsProvince"]);
                        //accountWithdrawals.WithdrawalsCity = Convert.ToString(reader["WithdrawalsCity"]);
                        //accountWithdrawals.WithdrawalsCityZipCode = Convert.ToString(reader["WithdrawalsCityZipCode"]);
                        Money = String.Format("{0:F}", mDr["Money"].ToString()),
                        //accountWithdrawals.BelongsPlatform = Convert.ToString(reader["BelongsPlatform"]);
                        //accountWithdrawals.IsCloseAccount = Convert.ToString(reader["IsCloseAccount"]);
                        //accountWithdrawals.CloseReason = Convert.ToString(reader["CloseReason"]);
                        //accountWithdrawals.BankName = Convert.ToString(reader["BankName"]);
                        //accountWithdrawals.BankAccount = Convert.ToString(reader["BankAccount"]);
                        //accountWithdrawals.BankHolder = Convert.ToString(reader["BankHolder"]);
                        //accountWithdrawals.BankAddress = Convert.ToString(reader["BankAddress"]);
                        //accountWithdrawals.SWIFT_Code = Convert.ToString(reader["SWIFT_Code"]);
                        //accountWithdrawals.BankCountry = Convert.ToString(reader["BankCountry"]);
                        //accountWithdrawals.BankProvince = Convert.ToString(reader["BankProvince"]);
                        //accountWithdrawals.BankCity = Convert.ToString(reader["BankCity"]);
                        //accountWithdrawals.BankCityZipCode = Convert.ToString(reader["BankCityZipCode"]);
                        //accountWithdrawals.IsTransitBank = Convert.ToString(reader["IsTransitBank"]);
                        //accountWithdrawals.TransitBankName = Convert.ToString(reader["TransitBankName"]);
                        //accountWithdrawals.TransitBankAccount = Convert.ToString(reader["TransitBankAccount"]);
                        //accountWithdrawals.TransitBankHolder = Convert.ToString(reader["TransitBankHolder"]);
                        //accountWithdrawals.TransitBankAddress = Convert.ToString(reader["TransitBankAddress"]);
                        //accountWithdrawals.TransitBankSWIFICode = Convert.ToString(reader["TransitBankSWIFICode"]);
                        //accountWithdrawals.TransitBankCountry = Convert.ToString(reader["TransitBankCountry"]);
                        //accountWithdrawals.TransitBankProvince = Convert.ToString(reader["TransitBankProvince"]);
                        //accountWithdrawals.TransitBankCity = Convert.ToString(reader["TransitBankCity"]);
                        //accountWithdrawals.TransitBankCityZipCode = Convert.ToString(reader["TransitBankCityZipCode"]);
                        //accountWithdrawals.MainAccountName = Convert.ToString(reader["MainAccountName"]);
                        //accountWithdrawals.MinorAccountName = Convert.ToString(reader["MinorAccountName"]);
                        //accountWithdrawals.Explanation = Convert.ToString(reader["Explanation"]);
                        ProcessingStatus = mDr["ProcessingStatus"].ToString(),
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

    }
}
