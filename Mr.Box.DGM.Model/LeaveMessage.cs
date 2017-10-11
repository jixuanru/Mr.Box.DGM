using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    /// <summary>
    /// 留言信息类
    /// </summary>
    [Serializable]
    public class LeaveMessage
    {
        /// <summary>
        /// 留言新消息编号
        /// </summary>
        public string LeaveMessageId { get; set; }

        /// <summary>
        /// 留言者姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 留言者名字
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        public string MessageContent { get; set; }

        /// <summary>
        /// 留言消息类别
        /// </summary>
        public string MessageType { get; set; }

        /// <summary>
        /// 留言内容
        /// </summary>
        public string LeaveDate { get; set; }
        /// <summary>
        /// 是否处理
        /// </summary>
        public string IsEnable { get; set; }
    }
}
