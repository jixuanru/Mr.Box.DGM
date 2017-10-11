using Mr.Box.DGM.Dal;
using Mr.Box.DGM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Bll
{
    public class DepositAccountBll
    {

        
        public DepositAccount AddDepositAccount(DepositAccount Model) { return new DepositAccountDAL().AddDepositAccount(Model); }
        public DepositAccount GetDepositAccountByBillno(string Billno) { return new DepositAccountDAL().GetDepositAccountByBillno(Billno); }
       
        public DepositAccount ModifyCheckDepositAccount(DepositAccount Model) { return new DepositAccountDAL().ModifyCheckDepositAccount(Model); }
        /// <summary>
        /// 添加账户取款信息
        /// </summary>
        /// <param name="accountWithdrawals">要添加的取款信息</param>
        /// <returns>添加取款结果</returns>
        public bool AddAccountWithdrawals(AccountWithdrawals accountWithdrawals)
        {
            bool result = false;
            try
            {
                accountWithdrawals = new AccountWithdrawalsDAL().AddAccount(accountWithdrawals);
                if (accountWithdrawals != null)
                {

                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception)
            {

                result = false;
            }


            return result;
        }
        public List<AccountWithdrawals> GetAccountWithdrawInfo(string account) { return new AccountWithdrawalsDAL().GetAccountWithdrawInfo(account); }

        /// <summary>
        /// 得到所有的取款信息
        /// </summary>
        /// <returns>返回所有的取款信息</returns>
        public IList<AccountWithdrawals> GetAccountWithdrawals(int offset, int pageSize, string search, string stime, string etime, out int total)
        {
           return new AccountWithdrawalsDAL().GetAccountWithdrawals(offset,pageSize, search,stime,etime,out total);
        }

        /// <summary>
        /// 修改取款信息处理状态
        /// </summary>
        /// <param name="WithdrawalsId"></param>
        /// <returns>返回取款交易账号</returns>
        public LOG UpdateAccountWithdrawalsStatus(string WithdrawalsId)
        {
            return new AccountWithdrawalsDAL().UpdateWithdrawalsStatus(WithdrawalsId);
        }

        /// <summary>
        /// 删除取款信息处理状态
        /// </summary>
        /// <param name="WithdrawalsId"></param>
        /// <returns>返回取款交易账号</returns>
        public LOG DeleteAccountWithdrawalsInfo(string WithdrawalsId)
        {
           return  new AccountWithdrawalsDAL().DeleteCheckAccountWithdrawals(Convert.ToInt32(WithdrawalsId));
        }

        /// <summary>
        /// 根据取款信息Id获取取款信息
        /// </summary>
        /// <param name="AccountWithdrawalsId">取款信息Id</param>
        /// <returns>返回取款信息</returns>
        public AccountWithdrawals GetAccountWithdrawalsByAccountWithdrawalsId(string AccountWithdrawalsId)
        {
            AccountWithdrawals accountWithdrawals = null;

            accountWithdrawals = new AccountWithdrawalsDAL().GetAccountWithdrawalsByWithdrawalsId(AccountWithdrawalsId);

            return accountWithdrawals;
        }


       
        /// <summary>
        /// 添加添加图片信息
        /// </summary>
        /// <param name="Model">图片信息内容</param>
        /// <returns>返回新添加的ID</returns>
        public bool AddUploadImgInfo(UploadImage Model)
        {
            return new UploadImageDAL().AddUploadImage(Model);
        }


        /// <summary>
        /// 得到所有的入金信息
        /// </summary>
        /// <returns>返回所有结果</returns>
        public IList<DepositAccount> GetDepositAccount(int offset, int pageSize, string search, string stime, string etime, out int total)
        {
          return  new DepositAccountDAL().GetDepositAccount(offset,pageSize, search,stime,etime,out total);
          
        }

        /// <summary>
        /// 取消入金订单
        /// </summary>
        /// <param name="DepositAccountId">入金信息编号</param>
        /// <returns>DepositId</returns>
        public LOG DeleteDepositAccount(string DepositAccountId)
        {
            return new DepositAccountDAL().DeleteCheckDepositAccount(Convert.ToInt32(DepositAccountId));
        }

        /// <summary>
        /// 根据入金编号修改相对应入金信息
        /// </summary>
        /// <param name="DepositAccountId">入金信息编号</param>
        /// <returns>返回入金交易账号</returns>
        public LOG UpdateDepositAccount(string DepositAccountId)
        {
            return new DepositAccountDAL().UpdateDepositAccount(Convert.ToInt32(DepositAccountId));
        }

    }
}
