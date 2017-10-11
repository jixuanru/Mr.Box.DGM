using Mr.Box.DGM.Dal;
using Mr.Box.DGM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Bll
{
    public class MT4AccountManagerBll
    {
       
        public List<MT4_UsersInfo> GetAccountByAgent(string Agent,string  role) { return new MT4AccountManagerDAL().GetAccountByAgent(Agent,role); }

       
        public List<MT4_Trades> GetOrders(int pageSize, int pageOffset, string stime, string etime, string login, string search, out int total) { return new MT4AccountManagerDAL().GetOrders(pageSize, pageOffset, stime, etime, login, search, out total); }

        public List<MT4AccountStatisticalInfo> GetAccountStatisticalInfo(string accountList, string startDate, string endDate) {
            return new MT4AccountManagerDAL().GetAccountStatisticalInfo(accountList, startDate, endDate);
        }
    }
}
