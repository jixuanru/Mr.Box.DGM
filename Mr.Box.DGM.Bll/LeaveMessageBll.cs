using Mr.Box.DGM.Dal;
using Mr.Box.DGM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Bll
{
    /// <summary>
    /// 留言消息类
    /// </summary>
    public class LeaveMessageBll
    {
        public bool AddLeaveMessage(LeaveMessage model) { return new LeaveMessageDAL().AddLeaveMessage(model); }


        public bool UpdateMsgState(int id) { return new LeaveMessageDAL().UpdateMsgState(id); }
        /// <summary>
        /// 得到所有的留言信息
        /// </summary>
        /// <returns>返回所有留言信息结果集</returns>
        public IList<LeaveMessage> GetAllMessage(int offset, int pageSize, string stime, string etime, out int total)
        {
           return new LeaveMessageDAL().GetLeaveMessage(offset,pageSize,stime,etime,out total);
        }


        /// <summary>
        /// 根据留言消息Id删除消息
        /// </summary>
        /// <param name="MessageId">留言消息Id</param>
        /// <returns>返回是否删除成功</returns>
        public bool DeleteMessageInfo(string MessageId)
        {
            bool result = false;
            result = new LeaveMessageDAL().DeleteCheckLeaveMessage(Convert.ToInt32(MessageId));
            return result;
        }
    }
}
