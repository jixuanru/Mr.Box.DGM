using System;
using System.Collections.Generic;
using System.Data;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;

namespace Mr.Box.DGM.Dal
{
    public class ProfessionInfoDAL
    {
        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();

        List<string> param = new List<string>();

        string addSql = "insert into ProfessionInfo values (ProfessionInfoId,@PersonalInformationId, @ProfessionType, @IsReceiveEnglandControlWork, @HirerName, @HirerCountry, @HirerAddress, @HirerCity, @HirerProvince, @ZipCode, @NatureOfBusiness, @Post, @BEFOREYearRevenue, @DepositAndInvestment, @FastIntoCapitalMoney, @SourceOfFundsID, @OtherSource);select @@identity";
        //string deleteSql = "delete from ProfessionInfo where ProfessionInfoId=@ProfessionInfoId";
        //string updateSql = "update ProfessionInfo set PersonalInformationId = @PersonalInformationId, ProfessionType = @ProfessionType, IsReceiveEnglandControlWork = @IsReceiveEnglandControlWork, HirerName = @HirerName, HirerCountry = @HirerCountry, HirerAddress = @HirerAddress, HirerCity = @HirerCity, HirerProvince = @HirerProvince, ZipCode = @ZipCode, NatureOfBusiness = @NatureOfBusiness, Post = @Post, BEFOREYearRevenue = @BEFOREYearRevenue, DepositAndInvestment = @DepositAndInvestment, FastIntoCapitalMoney = @FastIntoCapitalMoney, SourceOfFundsID = @SourceOfFundsID ,OtherSource = @OtherSource where ProfessionInfoId=@ProfessionInfoId";
        string selSql = "select * from ProfessionInfo where ProfessionInfoId=@ProfessionInfoId";
        //string sql = "select * from ProfessionInfo";

        /// <summary>
        /// 添加 开户者职业信息
        /// </summary>
        /// <param name="Model"> 开户者职业信息</param>
        /// <returns>新添加的 开户者职业信息</returns>
        public ProfessionInfo AddProfessionInfo(ProfessionInfo Model)
        {
            ProfessionInfo ProfessionInfoInfo = null;

                param.Add("[@PersonalInformationId],["+Model.PersonalInformationId+"]");
                param.Add("[@ProfessionType],["+Model.ProfessionType+"]");
                param.Add("[@IsReceiveEnglandControlWork],["+Model.IsReceiveEnglandControlWork+"]");
                param.Add("[@HirerName],["+Model.HirerName+"]");
                param.Add("[@HirerCountry],["+Model.HirerCountry+"]");
                param.Add("[@HirerAddress],["+Model.HirerAddress+"]");
                param.Add("[@HirerCity],["+Model.HirerCity+"]");
                param.Add("[@HirerProvince],["+Model.HirerProvince+"]");
                param.Add("[@ZipCode],["+Model.ZipCode+"]");
                param.Add("[@NatureOfBusiness],["+Model.NatureOfBusiness+"]");
                param.Add("[@Post],["+Model.Post+"]");
                param.Add("[@BEFOREYearRevenue],["+Model.BEFOREYearRevenue+"]");
                param.Add("[@DepositAndInvestment],["+Model.DepositAndInvestment+"]");
                param.Add("[@FastIntoCapitalMoney],["+Model.FastIntoCapitalMoney+"]");
                param.Add("[@SourceOfFundsID],["+Model.SourceOfFunds+"]");
            param.Add("[@OtherSource],[" + Model.OtherSource + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                string obj = ws_mysql.ExecuteScalar(param.ToArray(), "", addSql, "dgm");

                if (!string.IsNullOrEmpty(obj))
                {
                    ProfessionInfoInfo = GetProfessionInfoByProfessionInfoId(obj);
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
            return ProfessionInfoInfo;
        }





        /// <summary>
        /// 根据 开户者职业信息Id查询 开户者职业信息
        /// </summary>
        /// <param name="ClassesID"></param>
        /// <returns></returns>
        public ProfessionInfo GetProfessionInfoByProfessionInfoId(string ProfessionInfoId)
        {
            ProfessionInfo ProfessionInfoInfo = null;
            param.Add("[@ProfessionInfoId],[" + ProfessionInfoId + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", selSql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    ProfessionInfoInfo = new ProfessionInfo();

                    ProfessionInfoInfo.ProfessionInfoId = ProfessionInfoId;
                    ProfessionInfoInfo.PersonalInformationId = mDr["PersonalInformationId"].ToString();
                    ProfessionInfoInfo.ProfessionType = mDr["ProfessionType"].ToString();
                    ProfessionInfoInfo.IsReceiveEnglandControlWork = mDr["IsReceiveEnglandControlWork"].ToString();
                    ProfessionInfoInfo.HirerName = mDr["HirerName"].ToString();
                    ProfessionInfoInfo.HirerCountry = mDr["HirerCountry"].ToString();
                    ProfessionInfoInfo.HirerAddress = mDr["HirerAddress"].ToString();
                    ProfessionInfoInfo.HirerCity = mDr["HirerCity"].ToString();
                    ProfessionInfoInfo.HirerProvince = mDr["HirerProvince"].ToString();
                    ProfessionInfoInfo.ZipCode = mDr["ZipCode"].ToString();
                    ProfessionInfoInfo.NatureOfBusiness = mDr["NatureOfBusiness"].ToString();
                    ProfessionInfoInfo.Post = mDr["Post"].ToString();
                    ProfessionInfoInfo.BEFOREYearRevenue = mDr["BEFOREYearRevenue"].ToString();
                    ProfessionInfoInfo.DepositAndInvestment = mDr["DepositAndInvestment"].ToString();
                    ProfessionInfoInfo.FastIntoCapitalMoney = Convert.ToDecimal(mDr["FastIntoCapitalMoney"].ToString());
                    ProfessionInfoInfo.SourceOfFunds = mDr["SourceOfFundsID"].ToString();
                    ProfessionInfoInfo.OtherSource = mDr["OtherSource"].ToString();
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
            return ProfessionInfoInfo;

        }



    }
}
