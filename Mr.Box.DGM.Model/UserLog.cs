using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    /// <summary>
    /// 用户日志
    /// </summary>
    public class UserLog
    {
        /// <summary>
        /// 日志主键ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户登录账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 用户操作内容
        /// </summary>
        public string Explain { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public string LogDate { get; set; }
        /// <summary>
        /// 登录IP地址信息
        /// </summary>
        public string LogIP { get; set; }
        /// <summary>
        /// 备用属性
        /// </summary>
        public string Property1 { get; set; }
    }
}
