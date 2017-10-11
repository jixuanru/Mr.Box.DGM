using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    [Serializable]
    public class MT4_UsersInfo
    {
        /// <summary>
        /// mt4账户号
        /// </summary>
        public string MT4_UsersLogin { get; set; }

        /// <summary>
        /// mt4账户代理人数
        /// </summary>
        public string MT4_UsersRoleCount { get; set; }

        /// <summary>
        /// mt4账户名称
        /// </summary>
        public string MT4_UsersName { get; set; }

        /// <summary>
        /// mt4账户余额
        /// </summary>
        public string MT4_UsersBALANCE { get; set; }

        /// <summary>
        /// mt4账户电话
        /// </summary>
        public string MT4_UsersPhone { get; set; }

        /// <summary>
        /// mt4账户邮箱
        /// </summary>
        public string MT4_UsersEmail { get; set; }

        /// <summary>
        /// mt4账户属性1
        /// </summary>
        public string Property1 { get; set; }

        /// <summary>
        /// mt4账户属性2
        /// </summary>
        public string Property2 { get; set; }
    }
}
