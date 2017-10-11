using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;
using System.Data;

namespace Mr.Box.DGM.Dal
{
    public class AccountFormsDal
    {
        private List<string> param = new List<string>();
        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();
        com.dgmfx.ws.common.WS_Common ws_comm = new com.dgmfx.ws.common.WS_Common();

        #region 获取客户信息变更表初始信息
        public AccountInformationChangingForm GetUserForChangingForm(string Login)
        {
            AccountInformationChangingForm info = new AccountInformationChangingForm();
            string AccSQL = $"SELECT * FROM `Account` WHERE AccountName='{Login}';";
            string InfoSQL = "SELECT InformationId,Country,Province,LiveAddress,PhoneNumber FROM `personalinformation` WHERE InformationId={0}";
            string ProfSQL = "SELECT * FROM `ProfessionInfo` WHERE PersonalInformationId={0};";
            string InformationID = "";
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(AccSQL, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    info.AccountName = Login;
                    info.AccountEnName = mDr["EN_Surname"].ToString().Trim() + mDr["EN_Name"].ToString().Trim();
                    info.AccountExplanation = mDr["Explanation"].ToString().Trim();
                    info.AccountEmail = mDr["Email"].ToString().Trim();
                    InformationID = mDr["InformationID"].ToString();
                }

                InfoSQL = string.Format(InfoSQL, InformationID);
                myDataset = ws_mysql.ExecuteDataSetBySQL(InfoSQL, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    info.AccountCountry = mDr["Country"].ToString().Trim();
                    info.AccountProvince = mDr["Province"].ToString().Trim();
                    info.AccountAddress = mDr["LiveAddress"].ToString().Trim();
                    info.AccountMobile = mDr["PhoneNumber"].ToString().Trim();
                    InformationID = mDr["InformationId"].ToString();
                }
                ProfSQL = string.Format(ProfSQL, InformationID);
                myDataset = ws_mysql.ExecuteDataSetBySQL(ProfSQL, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    info.HirerName = mDr["HirerName"].ToString();
                    info.HirerCountry = mDr["HirerCountry"].ToString();
                    info.HirerAddress = mDr["HirerAddress"].ToString();
                    info.HirerProvince = mDr["HirerProvince"].ToString();
                    info.HirerZipcode = mDr["ZipCode"].ToString();
                }
            }
            catch
            { }
            return info;
        }
        #endregion

        #region 设置客户信息变更表资料
        public bool AccountInformationChangingForm(AccountInformationChangingForm Info)
        {
            try
            {
                param.Add("[@AccountName],[" + Info.AccountName + "]");
                param.Add("[@AccountEmail],[" + Info.AccountEmail + "]");
                param.Add("[@AccountCountry],[" + Info.AccountCountry + "]");
                param.Add("[@AccountProvince],[" + Info.AccountProvince + "]");
                param.Add("[@AccountAddress],[" + Info.AccountAddress + "]");
                param.Add("[@AccountMobile],[" + Info.AccountMobile + "]");
                param.Add("[@HirerName],[" + Info.HirerName + "]");
                param.Add("[@HirerAddress],[" + Info.HirerAddress + "]");
                param.Add("[@HirerZipCode],[" + Info.HirerZipcode + "]");
                param.Add("[@HirerCountry],[" + Info.HirerCountry + "]");
                param.Add("[@HirerProvince],[" + Info.HirerProvince + "]");

                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                string obj = ws_mysql.ExecuteScalar(param.ToArray(), "StoredProcedure", "UpdateAccountInformationChanging", "dgm");
                return obj == "OK";
            }
            catch
            {
                return false;
            }
            finally
            {
                param.Clear();
            }
        }
        #endregion


    }
}
