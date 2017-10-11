using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mr.Box.DGM.Model;
using System.Data;
using Mr.Box.DGM.Common;

namespace Mr.Box.DGM.Dal
{
    public class TableListDal
    {
        List<string> param = new List<string>();

        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();

        public bool RemoveTable(string id, string DetailID, string DetailName)
        {
            string strRemoveTableSql = $"DELETE FROM TableList WHERE ID={id};";
            string strRemoveDetailSql = $"DELETE FROM {DetailName} WHERE ID={id};";
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                if (int.Parse(ws_mysql.ExecuteScalar(param.ToArray(), "", strRemoveDetailSql, "dgm")) > 0)
                {
                    return int.Parse(ws_mysql.ExecuteScalar(param.ToArray(), "", strRemoveTableSql, "dgm")) > 0;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public IList<TableList> GetTableList(int offset, int pageSize, string type,string search,out int total)
        {
            //type:表格类型     search:账号或姓名
            string strSqlCondition = $" WHERE (1=1)";        //SQL的条件
            string strSql = "SELECT * FROM TableList";
            string strSqlCount = "SELECT COUNT(*) FROM TableList";

            IList<TableList> list = new List<TableList>();

            if(type!="所有表格")
            {
                strSqlCondition += $"AND(TableType='{type}')";
            }

            if (!string.IsNullOrEmpty(search))
            {
                strSqlCondition += $"AND((AccountName='{search}')OR(Explanation='{search}'))";
            }

            strSql += strSqlCondition;
            strSql += $" ORDER BY ID DESC LIMIT {offset},{pageSize};";

            strSqlCount += strSqlCondition + ";";

            total = 0;

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(strSql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new TableList
                    {
                        ID = mDr["ID"].ToString().Trim(),
                        AccountName = mDr["AccountName"].ToString().Trim(),
                        Explanation = mDr["Explanation"].ToString().Trim(),
                        PhoneNumber = mDr["PhoneNumber"].ToString().Trim(),
                        Email = mDr["Email"].ToString().Trim(),
                        CreateTime = DateTime.Parse(mDr["CreateTime"].ToString()).ToString("yyyy-MM-dd hh:mm:ss"),
                        UpdateTime = string.IsNullOrEmpty(mDr["UpdateTime"].ToString()) ? "" : DateTime.Parse(mDr["UpdateTime"].ToString()).ToString("yyyy-MM-dd hh:mm:ss"),
                        TableType = mDr["TableType"].ToString().Trim(),
                        TableDetailID = mDr["TableDetailID"].ToString().Trim(),
                        TableDetailName = mDr["TableDetailName"].ToString().Trim()
                    });
                }
                total = int.Parse(ws_mysql.ExecuteScalar(param.ToArray(), "", strSqlCount, "dgm"));
            }
            catch
            { }
            return list;
        }
    }
}
