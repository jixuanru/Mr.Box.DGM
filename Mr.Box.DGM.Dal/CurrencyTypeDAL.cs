using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;
using System.Web.Configuration;

namespace Mr.Box.DGM.Dal
{
    public class CurrencyTypeDAL
    {
        List<string> param = new List<string>();

        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();

        //string addSql = "insert into CurrencyType values (CurrencyTypeId,@CurrencyTypeName,@abbreviation, @Explanation );select @@identity";
        //string deleteSql = "delete from CurrencyType where CurrencyTypeId=@CurrencyTypeId";
        //string updateSql = "update CurrencyType set CurrencyTypeName = @CurrencyTypeName,abbreviation = @abbreviation,Explanation = @Explanation where CurrencyTypeId=@CurrencyTypeId";
        string selSql = "select * from CurrencyType where CurrencyTypeId=@CurrencyTypeId";
        //string sql = "select * from CurrencyType";

       
        /// <summary>
        /// 根据货币类型Id查询货币类型
        /// </summary>
        /// <param name="ClassesID"></param>
        /// <returns></returns>
        public CurrencyType GetCurrencyTypeByCurrencyTypeId(string CurrencyTypeId)
        {
            CurrencyType currencyType = null;

            param.Add("[@CurrencyTypeId],[" + CurrencyTypeId.ToString() + "]");
            //SqlParameter[] param = new SqlParameter[]
            //{
            //    new SqlParameter("@CurrencyTypeId",Convert.ToString(CurrencyTypeId))
            //};
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", selSql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    currencyType = new CurrencyType();
                    currencyType.CurrencyTypeId = CurrencyTypeId;
                    currencyType.CurrencyTypeName = mDr["CurrencyTypeName"].ToString();
                    currencyType.abbreviation = mDr["abbreviation"].ToString();
                    currencyType.Explanation = mDr["Explanation"].ToString();
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

    }
}
