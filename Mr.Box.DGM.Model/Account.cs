using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    /// <summary>
    /// 账户基础信息
    /// </summary>
    [Serializable]
    public class Account
    {
        /// <summary>
        /// 账户编号
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// 账户名称
        /// </summary>
        public string AccountName { get; set; }

        
        
        /// <summary>
        /// 账户密码
        /// </summary>
        public string AccountPassword { get; set; }
        
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string Nickname { get; set; }
        
        /// <summary>
        /// 所在国家
        /// </summary>
        public string Country { get; set; }
         
        /// <summary>
        /// 中文姓氏
        /// </summary>
        public string CH_Surname { get; set; }
        
        /// <summary>
        /// 英文姓氏
        /// </summary>
        public string EN_Surname { get; set; }
        
        /// <summary>
        /// 中文名
        /// </summary>
        public string CH_Name { get; set; }
        
        /// <summary>
        /// 英文名
        /// </summary>
        public string EN_Name { get; set; }


        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// 是否代理账号
        /// </summary>
        public string IsAgents { get; set; }

        /// <summary>
        /// 代理商
        /// </summary>
        public string Agents { get; set; }
       
        /// <summary>
        /// 币种
        /// </summary>
        public string CurrencyType { get; set; }
        
        /// <summary>
        /// 账户类型
        /// </summary>
        public string AccountType { get; set; }
        
       
        
        /// <summary>
        /// 账户个人信息编号
        /// </summary>
        public string InformationId { get; set; }

        /// <summary>
        /// 账户说明
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// 账户所属平台类型
        /// </summary>
        public string AccountBelongsPlatformType { get; set; }

        /// <summary>
        /// 账户交易类型
        /// </summary>
        public string AccountTradeType { get; set; }

        /// <summary>
        /// 是否购买软件
        /// </summary>
        public string IsBuy { get; set; }

        /// <summary>
        /// 在线课堂激活时间
        /// </summary>
        public string OnlineStart { get; set; }

        /// <summary>
        /// 在线课堂失效时间
        /// </summary>
        public string OnlineEnd { get; set; }
        /// <summary>
        /// 在线课堂等级
        /// </summary>
        public string RegDate { get; set; }

        /// <summary>
        /// 在线课堂密码
        /// </summary>
        public string OnlineRoomPwd { get; set; }




    }
}
