using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;
using System.Web.Configuration;

namespace Mr.Box.DGM.Dal
{
    public class LeaveMessageDAL
    {
        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();

        List<string> param = new List<string>();

        //        public int CodeId { get; set; }
        string addSql = "insert into LeaveMessage values (LeaveMessageId,@Name, @Company, @Address, @Email, @Phone, @MessageContent, @MessageType ,@LeaveDate,@IsEnable);select @@identity;";
        string deleteSql = "delete from LeaveMessage where LeaveMessageId=@LeaveMessageId;";
        //string updateSql = "update LeaveMessage set Name = @Name,Company = @Company,Email = @Email,Phone = @Phone,MessageContent = @MessageContent, MessageType = @MessageType,IsEnable=@IsEnable, LeaveDate = @LeaveDate where LeaveMessageId=@LeaveMessageId;";
        //string selSql = "select * from LeaveMessage where LeaveMessageId=@LeaveMessageId;";
        //string sql = "select * from LeaveMessage order by  LeaveMessageId desc;";

        /// <summary>
        /// 添加留言信息
        /// </summary>
        /// <param name="model">留言信息</param>
        /// <returns>新添加的留言信息</returns>
        public bool AddLeaveMessage(LeaveMessage model)
        {
            int num = 0;
            param.Add("[@Name],[" + model.Name + "]");
            param.Add("[@Company],[" + model.Company + "]");
            param.Add("[@Address],[" + model.Address + "]");
            param.Add("[@Email],[" + model.Email + "]");
            param.Add("[@Phone],[" + model.Phone + "]");
            param.Add("[@MessageContent],[" + model.MessageContent + "]");
            param.Add("[@LeaveDate],[" + model.LeaveDate + "]");
            param.Add("[@MessageType],[" + model.MessageType + "]");
            param.Add("[@IsEnable],[" + model.IsEnable);

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                num = ws_mysql.ExecuteNonQuery(param.ToArray(), "", addSql, "dgm");
            }
            catch
            { }
            finally
            {
                param.Clear();
            }
            return num > 0;
        }

        public bool UpdateMsgState(int id)
        {
            string _sql = "update LeaveMessage set IsEnable='已处理' where LeaveMessageId=" + id.ToString();
            int num = 0;

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                num = ws_mysql.ExecuteNonQuery(param.ToArray(), "", _sql, "dgm");
            }
            catch
            { }
            return num > 0;
        }

        /// <summary>
        /// 根据留言信息ID删除留言信息
        /// </summary>
        /// <param name="ClassesID">留言信息ID</param>
        /// <returns>是否删除成功</returns>
        public bool DeleteCheckLeaveMessage(int LeaveMessageId)
        {
            param.Add("[@LeaveMessageId],[" + LeaveMessageId.ToString() + "]");
            int num = 0;

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
            return num > 0;
        }

        /// <summary>
        /// 查询所有留言信息
        /// </summary>
        /// <returns></returns>
        public IList<LeaveMessage> GetLeaveMessage(int offset, int pageSize, string stime, string etime, out int total)
        {
            IList<LeaveMessage> list = new List<LeaveMessage>();
            string sql = string.Format("SELECT * FROM  LeaveMessage WHERE LeaveDate>'{0}' AND LeaveDate<='{1}' ORDER BY LeaveMessageId DESC Limit {2},{3};", stime, etime, offset, pageSize);
            string _sql = string.Format("SELECT COUNT(*) FROM LeaveMessage  WHERE LeaveDate>'{0}' AND LeaveDate<='{1}';", stime, etime);

            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "dgm");
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    list.Add(new LeaveMessage
                    {
                        LeaveMessageId = mDr["LeaveMessageId"].ToString(),
                        Name = mDr["Name"].ToString(),
                        Company = mDr["Company"].ToString(),
                        Address = mDr["Address"].ToString(),
                        Email = mDr["Email"].ToString(),
                        Phone = mDr["Phone"].ToString(),
                        MessageContent = mDr["MessageContent"].ToString(),
                        MessageType = mDr["MessageType"].ToString(),
                        LeaveDate = mDr["LeaveDate"].ToString(),
                        IsEnable = mDr["IsEnable"].ToString()
                    });
                }
                total = int.Parse(ws_mysql.ExecuteScalar(param.ToArray(), "", _sql, "dgm"));
            }
            catch
            {
                total = 0;
            }
            return list;
        }
    }
}
