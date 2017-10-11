using System.Data;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;
using System.Text;
using System.Web.Configuration;
using System;

namespace Mr.Box.DGM.Dal
{
    public class DmgfxBackstageAdminDal
    {
        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();

        public DmgfxBackstageAdmin Login(string name, string pwd)
        {
            DmgfxBackstageAdmin users = null;
            string sql = $"SELECT * FROM DMGFXBackstageAdmin WHERE AdminAccount='{name}' AND AdminPassword='{MD5Helper.GetMD532(pwd).ToUpper()}' AND IsEnable=1";

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    users = new DmgfxBackstageAdmin
                    {
                        AdminId = mDr["AdminId"].ToString(),
                        AdminAccount = mDr["AdminAccount"].ToString(),
                        AdminPassword = mDr["AdminPassword"].ToString(),
                        AdminLevel = mDr["AdminLevel"].ToString(),
                        AdminExplanation = mDr["AdminExplanation"].ToString(),
                        IsEnable = int.Parse(mDr["IsEnable"].ToString())
                    };
                }
            }
            catch(Exception ex)
            { }
            return users;
        }

    }
}
