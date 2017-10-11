using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class DGMUserInfo
    {
        /// <summary>
        /// 用户账户
        /// </summary>
        public string DGMUser_Login { get; set; }
        
        /// <summary>
        /// 用户密码
        /// </summary>
        public string DGMUser_Pwd { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public string DGMUser_Role { get; set; }

        /// <summary>
        /// 用户余额
        /// </summary>
        public string DGMUser_Balance { get; set; }

        /// <summary>
        /// 账户净值
        /// </summary>
        public string DGMUser_Equity { get; set; }

        /// <summary>
        /// 用户上月余额
        /// </summary>
        public string DGMUser_PREVMONTHBALANCE { get; set; }

        /// <summary>
        /// 上次余额
        /// </summary>
        public string DGMUser_PREVBALANCE { get; set; }

        /// <summary>
        /// 客户姓名
        /// </summary>
        public string DGMUser_Name { get; set; }

        /// <summary>
        /// 客户中文姓名
        /// </summary>
        public string DGMUser_ChName { get; set; }

        /// <summary>
        /// 客户地址
        /// </summary>
        public string DGMUser_Address { get; set; }

        /// <summary>
        /// 客户电话
        /// </summary>
        public string DGMUser_Phone { get; set; }

        /// <summary>
        /// 注册日期
        /// </summary>
        public string DGMUser_REGDATE { get; set; }

        /// <summary>
        /// 杠杆
        /// </summary>
        public string DGMUser_LEVERAGE { get; set; }

        /// <summary>
        /// 用户详细信息
        /// </summary>
        public Account DGMUser_Account { get; set; }

        /// <summary>
        /// 备用属性1
        /// </summary>
        public string DGMUser_Property1 { get; set; }

        /// <summary>
        /// 备用属性2
        /// </summary>
        public string DGMUser_Property2 { get; set; }
    }
}
