using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mr.Box.DGM.Common
{
    public class WebServiceHelper
    {
        public static string connectionString { get { return connectionStringDGMFX; } }

        private static readonly string connectionStringDGMFX = ConfigurationManager.ConnectionStrings["DGMFX"].ConnectionString;

        public DataSet ExecuteDataSet(string[] paras, string commandType, string commandText)
        {
            try
            {
                SqlConnection con = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand(commandText, con);
                CommandType cmdType = CommandType.Text;

                switch (commandType)
                {
                    case "StoredProcedure": { cmdType = CommandType.StoredProcedure; break; }
                    case "TableDirect": { cmdType = CommandType.TableDirect; break; }
                    default: break;
                }
                cmd.CommandType = cmdType;


                SqlParameter[] param = ListToSqlParameter(paras);
                if (param != null && param.Length > 0)
                {
                    cmd.Parameters.AddRange(param);
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch { }
            return null;
        }

        private static SqlParameter[] ListToSqlParameter(string[] Params)
        {
            List<SqlParameter> lstParam = new List<SqlParameter>();
            string strKey = "";
            string strKeyValue = "";
            List<string> lst = new List<string>(Params);
            lst.ForEach(p =>
            {
                strKey = p.Substring(0, p.IndexOf(","));
                strKeyValue = p.Replace(strKey, "").Replace(",[", "").Replace("]", "");
                strKey = strKey.Replace("[", "").Replace("]", "");

                lstParam.Add(new SqlParameter(strKey, strKeyValue));
            }
            );

            return lstParam.ToArray();
        }

    }
}
