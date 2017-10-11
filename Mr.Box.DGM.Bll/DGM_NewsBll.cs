using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Dal;

namespace Mr.Box.DGM.Bll
{
    public class DGM_NewsBll
    {

        public List<DGM_News> GetNews(int offset, int pageSize, string search, out int total) { return new DGM_NewsDAL().GetNews(offset,pageSize,search,out total); }

        public List<DGM_News> GetNewsById(string id) { return new DGM_NewsDAL().GetNewsById(id); }

        public DGM_News AddDGM_News(DGM_News Model) { return new DGM_NewsDAL().AddDGM_News(Model); }

       

        public bool DeleteCheckDGM_News(int DGMNewsId) { return new DGM_NewsDAL().DeleteCheckDGM_News(DGMNewsId); }

        public IList<DGM_News> GetOnlineClass(string time) { return new DGM_NewsDAL().GetOnlineClass(time); }
        public IList<DGM_News> getOnlineClassNotice() { return new DGM_NewsDAL().getOnlineClassNotice(); }


    }
}
