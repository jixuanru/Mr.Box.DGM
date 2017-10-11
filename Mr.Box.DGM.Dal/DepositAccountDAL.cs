using Mr.Box.DGM.Common;
using Mr.Box.DGM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace Mr.Box.DGM.Dal
{
    public class DepositAccountDAL
    {
        List<string> param = new List<string>();

        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();

        string addSql = "insert into DepositAccount values (DepositId,@AccountId, @DepositMoney, @OrderNumber, @Address, @ZipCode, @Name, @Email, @Phone, @Explanation,@Billno, @Mercode, @Currency_Type, @OrderAmount, @OrderDate, @SymbolSuccess, @BankReturnMsg, @AttachData, @IPSBillno, @Retencodetype, @SignatureInfo, @PlatformType);select @@identity;";
        string deleteSql = "delete from DepositAccount where DepositId=@DepositId;";
        string updateSql = "update DepositAccount set AccountId = @AccountId, DepositMoney = @DepositMoney, OrderNumber = @OrderNumber, Address = @Address, ZipCode = @ZipCode, Name = @Name, Email = @Email, Phone = @Phone, Explanation = @Explanation, Billno = @Billno, Mercode = @Mercode, Currency_Type = @Currency_Type, OrderAmount = @OrderAmount, OrderDate = @OrderDate, SymbolSuccess = @SymbolSuccess, BankReturnMsg = @BankReturnMsg, AttachData = @AttachData, IPSBillno = @IPSBillno, Retencodetype = @Retencodetype, SignatureInfo = @SignatureInfo ,PlatformType = @PlatformType where DepositId=@DepositId;";
        string selSql = "select  DepositId, AccountId, cast(round(DepositMoney,2) as decimal(18,2)) as DepositMoney , OrderNumber, Address, ZipCode, Name, Email, Phone, Explanation, Billno, Mercode, Currency_Type, cast(round(OrderAmount,2) as numeric(18,2)) as OrderAmount, OrderDate, SymbolSuccess, BankReturnMsg, AttachData, IPSBillno, Retencodetype, SignatureInfo,PlatformType  from DepositAccount where DepositId=@DepositId;";
        //string sql = "select DepositId, AccountId, cast(round(DepositMoney,2) as decimal(18,2)) as DepositMoney , OrderNumber, Address, ZipCode, Name, Email, Phone, Explanation, Billno, Mercode, Currency_Type, cast(round(OrderAmount,2) as numeric(18,2)) as OrderAmount, OrderDate, SymbolSuccess, BankReturnMsg, AttachData, IPSBillno, Retencodetype, SignatureInfo ,PlatformType from DepositAccount order by  DepositId desc;";

        /// <summary>
        /// 添加账户存款
        /// </summary>
        /// <param name="Model">账户存款</param>
        /// <returns>新添加的账户存款</returns>
        public DepositAccount AddDepositAccount(DepositAccount Model)
        {
            DepositAccount currencyType = null;

            param.Add("[@AccountId],[" + Model.AccountId + "]");
            param.Add("[@DepositMoney],[" + Model.DepositMoney + "]");
            param.Add("[@OrderNumber],[" + Model.OrderNumber + "]");
            param.Add("[@Address],[" + Model.Address + "]");
            param.Add("[@ZipCode],[" + Model.ZipCode + "]");
            param.Add("[@Name],[" + Model.Name + "]");
            param.Add("[@Email],[" + Model.Email + "]");
            param.Add("[@Phone],[" + Model.Phone + "]");
            param.Add("[@Explanation],[" + Model.Explanation + "]");
            param.Add("[@Billno],[" + Model.Billno + "]");
            param.Add("[@Mercode],[" + Model.Mercode + "]");
            param.Add("[@Currency_Type],[" + Model.Currency_Type + "]");
            param.Add("[@OrderAmount],[" + Model.OrderAmount + "]");
            param.Add("[@OrderDate],[" + Model.OrderDate + "]");
            param.Add("[@SymbolSuccess],[" + Model.SymbolSuccess + "]");
            param.Add("[@BankReturnMsg],[" + Model.BankReturnMsg + "]");
            param.Add("[@AttachData],[" + Model.AttachData + "]");
            param.Add("[@IPSBillno],[" + Model.IPSBillno + "]");
            param.Add("[@Retencodetype],[" + Model.Retencodetype + "]");
            param.Add("[@SignatureInfo],[" + Model.SignatureInfo + "]");
            param.Add("[@PlatformType],[" + Model.PlatformType + "]");
            param.Add("[@DepositId],[" + Model.DepositId + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                string obj = ws_mysql.ExecuteScalar(param.ToArray(), "", addSql, "dgm");

                if (!string.IsNullOrEmpty(obj))
                {
                    currencyType = GetDepositAccountByDepositAccountId(Convert.ToString(obj));
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
            return currencyType;
        }

        /// <summary>
        /// 取消入金订单
        /// </summary>
        /// <param name="DepositId">入金信息编号</param>
        /// <returns>返回入金的账号</returns>
        public LOG DeleteCheckDepositAccount(int DepositId)
        {
            param.Add("[@DepositId],[" + DepositId + "]");
            string _sql = "select AccountId ,cast(round(DepositMoney,2) as decimal(18,2)) as DepositMoney from DepositAccount where DepositId=@DepositId;";
            LOG log = null;

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", _sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    log = new LOG();
                    log.Account = mDr["AccountId"].ToString();
                    log.Money = mDr["DepositMoney"].ToString();
                }
                int num = ws_mysql.ExecuteNonQuery(param.ToArray(), "", deleteSql, "dgm");
            }
            catch
            { }
            finally
            {
                param.Clear();
            }
            return log;
        }

        /// <summary>
        /// 修改入金状态
        /// </summary>
        /// <param name="DepositId"></param>
        /// <returns>返回入金交易账号</returns>
        public LOG UpdateDepositAccount(int DepositId)
        {
            string UpdateSuccessSql = "update DepositAccount set Billno = Left(OrderNumber,12), SymbolSuccess = 'Y' where DepositId = @DepositId;";
            param.Add("[@DepositId],[" + DepositId.ToString() + "]");
            string _sql = "select AccountId,cast(round(DepositMoney,2) as decimal(18,2)) as DepositMoney from DepositAccount where DepositId=@DepositId;";
            LOG log = null;

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", _sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    log = new LOG();
                    log.Account = mDr["AccountId"].ToString();
                    log.Money = mDr["DepositMoney"].ToString();
                }
                int num = ws_mysql.ExecuteNonQuery(param.ToArray(), "", UpdateSuccessSql, "dgm");
            }
            catch
            { }
            finally
            {
                param.Clear();
            }
            return log;
        }

        /// <summary>
        /// 根据账户ID修改账户存款
        /// </summary>
        /// <param name="Model">要修改的账户存款</param>
        /// <returns>修改完成的账户存款</returns>
        public DepositAccount ModifyCheckDepositAccount(DepositAccount Model)
        {
            DepositAccount depositAccount = null;

            param.Add("[@AccountId],[" + Model.AccountId + "]");
            param.Add("[@DepositMoney],[" + Model.DepositMoney + "]");
            param.Add("[@OrderNumber],[" + Model.OrderNumber + "]");
            param.Add("[@Address],[" + Model.Address + "]");
            param.Add("[@ZipCode],[" + Model.ZipCode + "]");
            param.Add("[@Name],[" + Model.Name + "]");
            param.Add("[@Email],[" + Model.Email + "]");
            param.Add("[@Phone],[" + Model.Phone + "]");
            param.Add("[@Explanation],[" + Model.Explanation + "]");
            param.Add("[@Billno],[" + Model.Billno + "]");
            param.Add("[@Mercode],[" + Model.Mercode + "]");
            param.Add("[@Currency_Type],[" + Model.Currency_Type + "]");
            param.Add("[@OrderAmount],[" + Model.OrderAmount + "]");
            param.Add("[@OrderDate],[" + Model.OrderDate + "]");
            param.Add("[@SymbolSuccess],[" + Model.SymbolSuccess + "]");
            param.Add("[@BankReturnMsg],[" + Model.BankReturnMsg + "]");
            param.Add("[@AttachData],[" + Model.AttachData + "]");
            param.Add("[@IPSBillno],[" + Model.IPSBillno + "]");
            param.Add("[@Retencodetype],[" + Model.Retencodetype + "]");
            param.Add("[@SignatureInfo],[" + Model.SignatureInfo + "]");
            param.Add("[@PlatformType],[" + Model.PlatformType + "]");
            param.Add("[@DepositId],[" + Model.DepositId + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                int num = ws_mysql.ExecuteNonQuery(param.ToArray(), "", updateSql, "dgm");
                if (num > 0)
                {
                    depositAccount = GetDepositAccountByDepositAccountId(Model.DepositId);
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
            return depositAccount;
        }

        /// <summary>
        /// 根据账户存款Id查询账户存款
        /// </summary>
        /// <param name="ClassesID"></param>
        /// <returns></returns>
        public DepositAccount GetDepositAccountByDepositAccountId(string DepositId)
        {
            DepositAccount depositAccount = null;
            param.Add("[@DepositId],[" + DepositId + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", selSql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    depositAccount = new DepositAccount();
                    depositAccount.DepositId = DepositId;
                    depositAccount.AccountId = mDr["AccountId"].ToString();
                    depositAccount.DepositMoney = mDr["DepositMoney"].ToString();
                    depositAccount.OrderNumber = mDr["OrderNumber"].ToString();
                    depositAccount.Address = mDr["Address"].ToString();
                    depositAccount.ZipCode = mDr["ZipCode"].ToString();
                    depositAccount.Name = mDr["Name"].ToString();
                    depositAccount.Email = mDr["Email"].ToString();
                    depositAccount.Phone = mDr["Phone"].ToString();
                    depositAccount.Explanation = mDr["Explanation"].ToString();
                    depositAccount.Billno = mDr["Billno"].ToString();
                    depositAccount.Mercode = mDr["Mercode"].ToString();
                    depositAccount.Currency_Type = mDr["Currency_Type"].ToString();
                    depositAccount.OrderAmount = mDr["OrderAmount"].ToString();
                    depositAccount.OrderDate = mDr["OrderDate"].ToString();
                    depositAccount.SymbolSuccess = mDr["SymbolSuccess"].ToString();
                    depositAccount.BankReturnMsg = mDr["BankReturnMsg"].ToString();
                    depositAccount.AttachData = mDr["AttachData"].ToString();
                    depositAccount.IPSBillno = mDr["IPSBillno"].ToString();
                    depositAccount.Retencodetype = mDr["Retencodetype"].ToString();
                    depositAccount.SignatureInfo = mDr["SignatureInfo"].ToString();
                    depositAccount.PlatformType = mDr["PlatformType"].ToString();
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
            return depositAccount;
        }

        /// <summary>
        /// 根据账户存款Id查询账户存款
        /// </summary>
        /// <param name="ClassesID"></param>
        /// <returns></returns>
        public DepositAccount GetDepositAccountByBillno(string Billno)
        {

            string selSqlBillno = "select  DepositId, AccountId, cast(round(DepositMoney,2) as decimal(18,2)) as DepositMoney , OrderNumber, Address, ZipCode, Name, Email, Phone, Explanation, Billno, Mercode, Currency_Type, cast(round(OrderAmount,2) as decimal(18,2)) as OrderAmount, OrderDate, SymbolSuccess, BankReturnMsg, AttachData, IPSBillno, Retencodetype, SignatureInfo,PlatformType  from DepositAccount where Billno=@Billno;";
            DepositAccount depositAccount = null;
            param.Add("[@Billno],[" + Billno + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", selSqlBillno, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    depositAccount = new DepositAccount();

                    depositAccount.DepositId = mDr["DepositId"].ToString();
                    depositAccount.AccountId = mDr["AccountId"].ToString();
                    depositAccount.DepositMoney = mDr["DepositMoney"].ToString();
                    depositAccount.OrderNumber = mDr["OrderNumber"].ToString();
                    depositAccount.Address = mDr["Address"].ToString();
                    depositAccount.ZipCode = mDr["ZipCode"].ToString();
                    depositAccount.Name = mDr["Name"].ToString();
                    depositAccount.Email = mDr["Email"].ToString();
                    depositAccount.Phone = mDr["Phone"].ToString();
                    depositAccount.Explanation = mDr["Explanation"].ToString();
                    depositAccount.Billno = mDr["Billno"].ToString();
                    depositAccount.Mercode = mDr["Mercode"].ToString();
                    depositAccount.Currency_Type = mDr["Currency_Type"].ToString();
                    depositAccount.OrderAmount = mDr["OrderAmount"].ToString();
                    depositAccount.OrderDate = mDr["OrderDate"].ToString();
                    depositAccount.SymbolSuccess = mDr["SymbolSuccess"].ToString();
                    depositAccount.BankReturnMsg = mDr["BankReturnMsg"].ToString();
                    depositAccount.AttachData = mDr["AttachData"].ToString();
                    depositAccount.IPSBillno = mDr["IPSBillno"].ToString();
                    depositAccount.Retencodetype = mDr["Retencodetype"].ToString();
                    depositAccount.SignatureInfo = mDr["SignatureInfo"].ToString();
                    depositAccount.PlatformType = mDr["PlatformType"].ToString();
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
            return depositAccount;
        }


        public IList<DepositAccount> GetDepositAccount(int offset, int pageSize, string search, string stime, string etime, out int total)
        {
            IList<DepositAccount> list = new List<DepositAccount>();
            string sql = string.Format("SELECT DepositId,AccountId,Name,Email,Phone,OrderAmount,SymbolSuccess,Billno,Explanation,PayType FROM DepositAccount WHERE Billno >'{0}' AND Billno<= '{1}' AND( Name LIKE'%{2}%' OR AccountId LIKE'%{3}%' OR Billno LIKE'%{4}%') ORDER BY DepositId DESC Limit {5},{6};", stime, etime, search, search, search, offset, pageSize);
            string _sql = string.Format("SELECT COUNT(Billno) FROM DepositAccount  WHERE Billno >'{0}' AND Billno<= '{1}' AND( Name LIKE'%{2}%' OR AccountId LIKE'%{3}%' OR Billno LIKE'%{4}%');", stime, etime, search, search, search);

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new DepositAccount
                    {
                        DepositId = mDr["DepositId"].ToString(),
                        AccountId = mDr["AccountId"].ToString(),
                        //DepositMoney = String.Format("{0:F}", mDr["DepositMoney"]),
                        //OrderNumber = Convert.ToString(mDr["OrderNumber"]),
                        //Address = Convert.ToString(mDr["Address"]),
                        //ZipCode = Convert.ToString(mDr["ZipCode"]),
                        Name = mDr["Name"].ToString(),
                        Email = mDr["Email"].ToString(),
                        Phone = mDr["Phone"].ToString(),
                        Explanation = mDr["Explanation"].ToString(),
                        Billno = mDr["Billno"].ToString(),
                        //Mercode = Convert.ToString(mDr["Mercode"]),
                        //Currency_Type = Convert.ToString(mDr["Currency_Type"]),
                        OrderAmount = String.Format("{0:F}", mDr["OrderAmount"]),
                        //OrderDate = Convert.ToString(mDr["OrderDate"]),
                        SymbolSuccess = mDr["SymbolSuccess"].ToString(),
                        //BankReturnMsg = Convert.ToString(mDr["BankReturnMsg"]),
                        //AttachData = Convert.ToString(mDr["AttachData"]),
                        //IPSBillno = Convert.ToString(mDr["IPSBillno"]),
                        //Retencodetype = Convert.ToString(mDr["Retencodetype"]),
                        //SignatureInfo = Convert.ToString(mDr["SignatureInfo"]),
                        //PlatformType = Convert.ToString(mDr["PlatformType"]),
                        PayType = Convert.ToString(mDr["PayType"])
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
