using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Dal;

namespace Mr.Box.DGM.Bll
{
    public class MT4AccountBll
    {
        public string DGMFXLogIn(string login, string password, string userRole)
        {
            return new MT4AccountDAL().DGMFXLogIn(login, password, userRole);
        }

        public DGMUserInfo GetAccountInfo(string AccountID)
        {
            return new MT4AccountDAL().GetAccountInfo(AccountID);
        }

        public Account GetAccountByAccountNumberLogin(string AccountNumber)
        { return new AccountDAL().GetAccountByAccountNumberLogin(AccountNumber); }

       

        public bool EditAccount(Account acc,bool SendMail)
        {
            return new AccountDAL().EditAccount(acc,SendMail);
        }

        public bool RemoveAccount(int id)
        {
            return new AccountDAL().RemoveAccount(id);
        }
    }
}
