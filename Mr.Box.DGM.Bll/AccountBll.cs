using Mr.Box.DGM.Dal;
using Mr.Box.DGM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Mr.Box.DGM.Bll
{
    public class AccountBll
    {
        public Account GetAccountByAccountNumber(string AccountNumber) {
            return new AccountDAL().GetAccountByAccountNumber(AccountNumber);
        }


        public bool AccountCopy(int accId) { return new AccountDAL().AccountCopy(accId);}

        public bool RecoveryRecycle(int id) { return new AccountDAL().RecoveryRecycle(id); }

        public bool DeleteRecycle(int id) { return new AccountDAL().DeleteRecycle(id); }

        public bool EmailValidate(string email, string name) { return new AccountDAL().EmailValidate(email, name); }


        /// <summary>
        /// 获取一定时间段内新增的客户
        /// </summary>
        /// <param name="stime"></param>
        /// <param name="etime"></param>
        /// <param name="operation">
        /// __$operation=1的情况，表示删除
        /// __$operation=2的情况，表示新增
        /// __$operation=3或者4，表示更新，3表示旧值，4表示新值
        /// </param>
        /// <returns></returns>
        public string cdc_Account_CT(string stime, string etime)
        {
            return new AccountDAL().cdc_Account_CT(stime,etime);
        }
    }
}
