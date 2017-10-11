using Mr.Box.DGM.Dal;
using Mr.Box.DGM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Bll
{
    public class Mt4UserBll
    {
        public List<MT4_Users> GetUser(string login) { return new Mt4UsersDal().GetUser(login); }

        



        public List<MT4_Users> GetUser(int pageSize, int pageOffset, string login, string type, string search, out int total) { return new Mt4UsersDal().GetUser(pageSize, pageOffset, login, type, search, out total); }
    }
}
