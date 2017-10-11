using System;
using System.Collections.Generic;
using System.Data;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;

namespace Mr.Box.DGM.Dal
{
    public class DGM_NewsDAL
    {
        List<string> param = new List<string>();
        DataSet myDataset = null;

        //com.dgmfx.ws.mssql.WS_MSSQL ws_mssql = new com.dgmfx.ws.mssql.WS_MSSQL();
        
        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();

        private string addSql = "insert into DGM_News values (DGMNewsId,@NewsTitle,@NewContent, @CreateDate,@NewsAuthor, @NewsType, @Explanation ,@IsHomeShow,@NewsHomeText, @DGMNewsImgUrl);select @@identity";
        private string deleteSql = "delete from DGM_News where DGMNewsId=@DGMNewsId";
        //private string updateSql = "update DGM_News set NewsTitle = @NewsTitle,NewContent = @NewContent,CreateDate = @CreateDate,NewsAuthor = @NewsAuthor,NewsType = @NewsType,Explanation = @Explanation ,IsHomeShow = @IsHomeShow,NewsHomeText = @NewsHomeText, DGMNewsImgUrl = @DGMNewsImgUrl where DGMNewsId=@DGMNewsId";
        private string selSql = "select * from DGM_News where DGMNewsId=@DGMNewsId";

        public List<DGM_News> GetNews(int offset, int pageSize, string search, out int total)
        {
            List<DGM_News> list = new List<DGM_News>();
            ///MYSQL:
            string sql =
                $"SELECT  DGMNewsId,NewsTitle,CreateDate,NewsAuthor,NewsType FROM DGM_News WHERE NewsType='DGM公告' AND NewsTitle LIKE '%{search}%' ORDER BY CreateDate Desc Limit {offset},{pageSize}";

            string _sql = $"SELECT COUNT(*) FROM DGM_News WHERE NewsType='DGM公告' AND NewsTitle LIKE '%{search}%'";

            total = 0;
            
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", sql, "dgm");
                //ws_mysql_local.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                //myDataset = ws_mysql_local.ExecuteDataSet(param.ToArray(), "", sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add( new DGM_News
                    {
                        DGMNewsId = mDr["DGMNewsId"].ToString(),
                        NewsTitle = mDr["NewsTitle"].ToString(),
                        CreateDate = mDr["CreateDate"].ToString(),
                        NewsAuthor = mDr["NewsAuthor"].ToString(),
                        NewsType = mDr["NewsType"].ToString()
                    });
                }
                string val = ws_mysql.ExecuteScalar(param.ToArray(), "", _sql, "dgm");
                if (!string.IsNullOrEmpty(val))
                {
                    total = int.Parse(val.ToString());
                }
                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 添加新闻信息
        /// </summary>
        /// <param name="Model">新闻信息</param>
        /// <returns>新添加的新闻信息</returns>
        public DGM_News AddDGM_News(DGM_News Model)
        {
            DGM_News dgmNewsInfo = null;

            param.Add("[@NewsTitle],[" + Model.NewsTitle + "]");
            param.Add("[@NewContent],[" + Model.NewContent + "]");
            param.Add("[@CreateDate],[" + Model.CreateDate + "]");
            param.Add("[@NewsAuthor],[" + Model.NewsAuthor + "]");
            param.Add("[@NewsType],[" + Model.NewsType + "]");
            param.Add("[@IsHomeShow],[" + Model.IsHomeShow + "]");
            param.Add("[@NewsHomeText],[" + Model.NewsHomeText + "]");
            param.Add("[@DGMNewsImgUrl],[" + Model.DGMNewsImgUrl + "]");
            param.Add("[@Explanation],[" + Model.Explanation + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                string obj = ws_mysql.ExecuteScalar(param.ToArray(), "", addSql, "dgm");
                //                var obj = SqlHelper.ExecuteScalar(SqlHelper.connectionString, CommandType.Text, addSql, param);
                if (!string.IsNullOrEmpty(obj))
                {
                    dgmNewsInfo = GetDGMNewsByDGMNewsId(obj);
                }
            }
            catch
            {
                param.Clear();
            }
            return dgmNewsInfo;
        }

        /// <summary>
        /// 根据新闻信息ID删除新闻信息
        /// </summary>
        /// <param name="ClassesID">新闻信息ID</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteCheckDGM_News(int DGMNewsId)
        {
            param.Add("[@DGMNewsId],[" + DGMNewsId.ToString() + "]");
            int num = 0;
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                num = ws_mysql.ExecuteNonQuery(param.ToArray(), "", deleteSql, "dgm");
            }
            catch
            {

            }
            finally
            {
                param.Clear();
            }
            return num > 0;
        }

        /// <summary>
        /// 根据新闻信息Id查询新闻信息
        /// </summary>
        /// <param name="ClassesID"></param>
        /// <returns></returns>
        public DGM_News GetDGMNewsByDGMNewsId(string DGMNewsId)
        {
            DGM_News DGM_NewsInfo = null;
            param.Add("[@DGMNewsId],[" + Convert.ToString(DGMNewsId) + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", selSql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    DGM_NewsInfo = new DGM_News();
                    DGM_NewsInfo.DGMNewsId = DGMNewsId;
                    DGM_NewsInfo.NewsTitle = mDr["NewsTitle"].ToString();
                    DGM_NewsInfo.NewContent = mDr["NewContent"].ToString();
                    DGM_NewsInfo.CreateDate = mDr["CreateDate"].ToString();
                    DGM_NewsInfo.NewsAuthor = mDr["NewsAuthor"].ToString();
                    DGM_NewsInfo.NewsType = mDr["NewsType"].ToString();
                    DGM_NewsInfo.Explanation = mDr["Explanation"].ToString();
                    DGM_NewsInfo.IsHomeShow = mDr["IsHomeShow"].ToString();
                    DGM_NewsInfo.NewsHomeText = mDr["NewsHomeText"].ToString();
                    DGM_NewsInfo.DGMNewsImgUrl = mDr["DGMNewsImgUrl"].ToString();
                }
            }
            catch
            {

            }
            finally
            {
                param.Clear();
            }
            return DGM_NewsInfo;
        }


        public List<DGM_News> GetNewsById(string id)
        {
            List<DGM_News> list = new List<DGM_News>();
            param.Add("[@DGMNewsId],[" + Convert.ToString(id) + "]");

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                myDataset = ws_mysql.ExecuteDataSet(param.ToArray(), "", selSql, "DGM");

                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new DGM_News
                    {
                        DGMNewsId = id,
                        NewsTitle = mDr["NewsTitle"].ToString(),
                        NewContent = mDr["NewContent"].ToString(),
                        CreateDate = mDr["CreateDate"].ToString(),
                        NewsAuthor = mDr["NewsAuthor"].ToString(),
                        NewsType = mDr["NewsType"].ToString(),
                        Explanation = mDr["Explanation"].ToString(),
                        IsHomeShow = mDr["IsHomeShow"].ToString(),
                        NewsHomeText = mDr["NewsHomeText"].ToString(),
                        DGMNewsImgUrl = mDr["DGMNewsImgUrl"].ToString()
                    });
                }
            }
            catch
            { }
            finally
            {
                param.Clear();
            }
            return list;
        }

        /// <summary>
        /// 根据时间查询当天最视频课堂
        /// </summary>
        /// <param name="time">日期 yyyy-MM-dd</param>
        /// <returns></returns>
        public IList<DGM_News> GetOnlineClass(string time)
        {
            string _sql = "select NewsTitle,NewsAuthor ,NewsHomeText,CreateDate from DGM_News where  NewsType='课程安排' and CreateDate='" + time + "'  and IsHomeShow=0 order by DGMNewsId desc Limit 1;";
            List<DGM_News> list = new List<DGM_News>();

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                myDataset = ws_mysql.ExecuteDataSetBySQL(_sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new DGM_News
                    {
                        NewsTitle = mDr["NewsTitle"].ToString(),
                        CreateDate = mDr["CreateDate"].ToString(),
                        NewsAuthor = mDr["NewsAuthor"].ToString(),
                        NewsHomeText = mDr["NewsHomeText"].ToString()
                    });
                }
            }
            catch
            { }
            return list;
        }

        public IList<DGM_News> getOnlineClassNotice()
        {
            string _sql = "select DGMNewsId,NewsTitle,NewsHomeText,CreateDate,NewsAuthor from DGM_News where NewsType='视频课堂公告' order by DGMNewsId desc Limit 1;";
            List<DGM_News> list = new List<DGM_News>();

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                myDataset = ws_mysql.ExecuteDataSetBySQL(_sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new DGM_News
                    {
                        DGMNewsId = mDr["DGMNewsId"].ToString(),
                        NewsTitle = mDr["NewsTitle"].ToString(),
                        CreateDate = mDr["CreateDate"].ToString(),
                        NewsAuthor = mDr["NewsAuthor"].ToString(),
                        NewsHomeText = mDr["NewsHomeText"].ToString()
                    });
                }
            }
            catch
            { }
            return list;
        }

    }
}
