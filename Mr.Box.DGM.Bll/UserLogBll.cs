using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Dal;

namespace Mr.Box.DGM.Bll
{
    public class UserLogBll
    {
        public bool InsertLogs(UserLog log) { return new UserLogDal().InsertLogs(log); }


       
        public List<UserLog> GetLogsBySearch(int offset, int pageSize, string search, string stime, string etime, out int total)
        {
            return new UserLogDal().GetLogsBySearch(offset,pageSize,search,stime,etime,out total);
        }

        public List<UserLog> GetLogsByLogin(int pageSize, int pageOffset, string login, string stime, string etime, out int total) {
            return new UserLogDal().GetLogsByLogin(pageSize, pageOffset, login, stime, etime, out total);
        }
        public bool DeleteLog(int id) { return new UserLogDal().DeleteLog(id); }
    }
}
