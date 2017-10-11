using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Dal;

namespace Mr.Box.DGM.Bll
{
    public class TableListBll
    {

        public bool RemoveTable(string id,string DetailID,string DetailName)
        {
            return new TableListDal().RemoveTable(id,DetailID,DetailName);
        }
        public IList<TableList> GetTableList(int offset, int pageSize, string type,string search,out int total)
        {
            return new TableListDal().GetTableList(offset, pageSize, type,search,out total);
        }
    }
}
