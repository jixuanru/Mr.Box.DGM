using System;
using System.Collections.Generic;
using System.Data;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;

namespace Mr.Box.DGM.Dal
{
    public class UploadImageDAL
    {
        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();

        List<string> param = new List<string>();

        string addSql = "insert into UploadImage values (UploadimgId,@AccountID, @PictureTypeId, @UploadName, @PictureUrl, @UPloadTime, @AddressDifferent);select @@identity";
        string deleteSql = "delete from UploadImage where UploadimgId=@UploadimgId";
        //string updateSql = "update UploadImage set AccountID = @AccountID, PictureTypeId = @PictureTypeId, UploadName = @UploadName, PictureUrl = @PictureUrl, UPloadTime = @UPloadTime, AddressDifferent = @AddressDifferent where UploadimgId = @UploadimgId";
        string selSql = "select * from UploadImage where UploadimgId = @UploadimgId";


        public bool AddUploadImage(UploadImage Model)
        {
            param.Add("[@AccountID],[" + Model.AccountID + "]");
            param.Add("[@PictureTypeId],[" + Model.PictureTypeId + "]");
            param.Add("[@UploadName],[" + Model.UploadName + "]");
            param.Add("[@PictureUrl],[" + Model.PictureUrl + "]");
            param.Add("[@UPloadTime],[" + Model.UPloadTime + "]");
            param.Add("[@AddressDifferent],[" + Model.AddressDifferent + "]");

            int num = 0;
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                num = ws_mysql.ExecuteNonQuery(param.ToArray(), "", addSql, "dgm");
            }
            catch
            {
            }
            finally
            {
                param.Clear();
            }
            return (num > 0);
        }

        /// <summary>
        /// 根据资金来源ID删除资金来源
        /// </summary>
        /// <param name="ClassesID">资金来源ID</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteCheckUploadImage(int UploadImageId)
        {
            int num = 0;

            param.Add("[@UploadimgId],[" + UploadImageId.ToString() + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);

                num = ws_mysql.ExecuteNonQuery(param.ToArray(), "", deleteSql, "dgm");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                param.Clear();
            }
            return (num > 0);
        }



        /// <summary>
        /// 根据资金来源Id查询资金来源
        /// </summary>
        /// <param name="ClassesID"></param>
        /// <returns></returns>
        public UploadImage GetUploadImageByUploadImageId(string UploadImagesId)
        {
            UploadImage UploadImageInfo = null;

            param.Add("[@UploadimgId],[" + UploadImagesId + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", selSql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    UploadImageInfo = new UploadImage();
                    UploadImageInfo.UploadimgId = UploadImagesId;
                    UploadImageInfo.AccountID = mDr["AccountID"].ToString();
                    UploadImageInfo.PictureTypeId = mDr["PictureTypeId"].ToString();
                    UploadImageInfo.UploadName = mDr["UploadName"].ToString();
                    UploadImageInfo.PictureUrl = mDr["PictureUrl"].ToString();
                    UploadImageInfo.UPloadTime = mDr["UPloadTime"].ToString();
                    UploadImageInfo.AddressDifferent = mDr["AddressDifferent"].ToString();
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
            return UploadImageInfo;
        }


        public IList<UploadImage> GetImages(int offset, int pageSize, string search, out int total)
        {
            IList<UploadImage> list = new List<UploadImage>();
            string sql = string.Empty;
            string _sql = string.Empty;

            total = 0;

            if (String.IsNullOrEmpty(search))
            {
                sql = string.Format("SELECT UploadimgId,PictureTypeId,UploadName,PictureUrl,UPloadTime FROM UploadImage group by UploadimgId,PictureTypeId,UploadName,PictureUrl,UPloadTime ORDER BY UPloadTime DESC Limit {0},{1};", offset,pageSize);
                _sql = string.Format("SELECT COUNT(*) FROM UploadImage;");
            }
            else
            {
                sql = string.Format("SELECT UploadimgId,PictureTypeId,UploadName,PictureUrl,UPloadTime FROM UploadImage WHERE UploadName LIKE'%{0}%' group by UploadimgId,PictureTypeId,UploadName,PictureUrl,UPloadTime ORDER BY UPloadTime DESC Limit {1},{2};", search, offset, pageSize);
                _sql = string.Format(" SELECT COUNT(*) FROM UploadImage WHERE UploadName LIKE '%{0}%'", search);
            }

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new UploadImage
                    {
                        UploadimgId = mDr["UploadimgId"].ToString(),
                        PictureTypeId = mDr["PictureTypeId"].ToString(),
                        UploadName = mDr["UploadName"].ToString(),

                        PictureUrl = mDr["PictureUrl"].ToString().Replace('\\', '/'),
                        UPloadTime = mDr["UPloadTime"].ToString()
                    });
                }
                total = int.Parse(ws_mysql.ExecuteScalar(param.ToArray(), "", _sql, "dgm"));
            }
            catch
            { }
            return list;
        }
    }
}
