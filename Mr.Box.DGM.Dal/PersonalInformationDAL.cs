using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;
using System.Web.Configuration;

namespace Mr.Box.DGM.Dal
{
    public class PersonalInformationDAL
    {
        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();

        List<string> param = new List<string>();

        string addSql = "insert into PersonalInformation values (InformationId,@Country, @Province, @City, @LiveAddress, @PhoneNumber, @HomePhone, @Birthday, @Nationality, @CredentialsFromCountry, @CredentialsTypeId, @CredentialsNumber, @Explanation );select @@identity;";
        //string deleteSql = "delete from PersonalInformation where InformationId=@InformationId;";
        //string updateSql = "update PersonalInformation set Country = @Country, Province = @Province, City = @City, LiveAddress = @LiveAddress, PhoneNumber = @PhoneNumber, HomePhone = @HomePhone, Birthday = @Birthday, Nationality = @Nationality, CredentialsFromCountry = @CredentialsFromCountry, CredentialsTypeId = @CredentialsTypeId, CredentialsNumber = @CredentialsNumber, Explanation = @Explanation where InformationId=@InformationId;";
        string selSql = "select * from PersonalInformation where InformationId=@InformationId;";
        //string sql = "select * from PersonalInformation;";

        /// <summary>
        /// 添加个人信息
        /// </summary>
        /// <param name="Model">个人信息</param>
        /// <returns>新添加的个人信息</returns>
        public PersonalInformation AddPersonalInformation(PersonalInformation Model)
        {
            PersonalInformation PersonalInformationInfo = null;

            param.Add("[@Country],[" + Model.Country + "]");
            param.Add("[@Province],[" + Model.Province + "]");
            param.Add("[@City],[" + Model.City + "]");
            param.Add("[@LiveAddress],[" + Model.LiveAddress + "]");
            param.Add("[@PhoneNumber],[" + Model.PhoneNumber + "]");
            param.Add("[@HomePhone],[" + Model.HomePhone + "]");
            param.Add("[@Birthday],[" + Model.Birthday + "]");
            param.Add("[@Nationality],[" + Model.Nationality + "]");
            param.Add("[@CredentialsFromCountry],[" + Model.CredentialsFromCountry + "]");
            param.Add("[@CredentialsTypeId],[" + Model.CredentialsType + "]");
            param.Add("[@CredentialsNumber],[" + Model.CredentialsNumber + "]");
            param.Add("[@Explanation],[" + Model.Explanation);

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                string obj = ws_mysql.ExecuteScalar(param.ToArray(), "", addSql, "dgm");

                if (!string.IsNullOrEmpty(obj))
                {
                    PersonalInformationInfo = GetPersonalInformationByInformationId(Convert.ToString(obj));
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
            return PersonalInformationInfo;
        }

        /// <summary>
        /// 根据个人信息Id查询个人信息
        /// </summary>
        /// <param name="ClassesID"></param>
        /// <returns></returns>
        public PersonalInformation GetPersonalInformationByInformationId(string InformationId)
        {
            PersonalInformation PersonalInformationInfo = null;

            param.Add("[@InformationId],[" + InformationId + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", selSql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    PersonalInformationInfo = new PersonalInformation();

                    PersonalInformationInfo.InformationId = InformationId;
                    PersonalInformationInfo.Country = mDr["Country"].ToString();
                    PersonalInformationInfo.Province = mDr["Province"].ToString();
                    PersonalInformationInfo.City = mDr["City"].ToString();
                    PersonalInformationInfo.LiveAddress = mDr["LiveAddress"].ToString();
                    PersonalInformationInfo.PhoneNumber = mDr["PhoneNumber"].ToString();
                    PersonalInformationInfo.HomePhone = mDr["HomePhone"].ToString();
                    PersonalInformationInfo.Birthday = mDr["Birthday"].ToString();
                    PersonalInformationInfo.Nationality = mDr["Nationality"].ToString();
                    PersonalInformationInfo.CredentialsFromCountry = mDr["CredentialsFromCountry"].ToString();
                    PersonalInformationInfo.CredentialsType = mDr["CredentialsTypeId"].ToString();
                    PersonalInformationInfo.CredentialsNumber = mDr["CredentialsNumber"].ToString();
                    PersonalInformationInfo.Explanation = mDr["Explanation"].ToString();
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
            return PersonalInformationInfo;
        }


    }
}
