using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    [Serializable]
    public class MT4_Users
    {
        /// <summary>
        /// 用户MT4账号
        /// </summary>
        public string LOGIN { get; set; }
        ///// <summary>
        ///// 用户所属组
        ///// </summary>
        //public string GROUP { get; set; }
        /// <summary>
        /// 账户活动标志 1-积极 0-不积极
        /// </summary>
        public string ENABLE { get; set; }
        /// <summary>
        /// 是否允许更改密码 1-允许  0-不允许
        /// </summary>
        public string ENABLE_CHANGE_PASS { get; set; }
        /// <summary>
        /// 没有交易的时候设置只读模式 1-只读模式  0关闭只读模式
        /// </summary>
        public string ENABLE_READONLY { get; set; }
        /// <summary>
        /// 手机密码
        /// </summary>
        public string PASSWORD_PHONE { get; set; }
        /// <summary>
        /// 账户所有者
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string COUNTRY { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string CITY { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string STATE { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string ZIPCODE { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string ADDRESS { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EMAIL { get; set; }
        /// <summary>
        /// 注释
        /// </summary>
        public string COMMENT { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 是否居民身份
        /// </summary>
        public string STATUS { get; set; }
        /// <summary>
        /// 注册日期
        /// </summary>
        public string REGDATE { get; set; }

        /// <summary>
        /// 账户上次交易时间
        /// </summary>
        public string LASTDATE { get; set; }
        /// <summary>
        /// 杠杆
        /// </summary>
        public string LEVERAGE { get; set; }

        /// <summary>
        /// 代理账号
        /// </summary>
        public string AGENT_ACCOUNT { get; set; }
        /// <summary>
        /// 账户最近更新时间(UNIX格式)  服务器时间
        /// </summary>
        public string TIMESTAMP { get; set; }
        /// <summary>
        /// 账户余额
        /// </summary>
        public string BALANCE { get; set; }
        /// <summary>
        /// 上月余额
        /// </summary>
        public string PREVMONTHBALANCE { get; set; }
        /// <summary>
        /// 前一天余额
        /// </summary>
        public string PREVBALANCE { get; set; }
        /// <summary>
        /// 信用
        /// </summary>
        public string CREDIT { get; set; }
        /// <summary>
        /// 收取的年利率
        /// </summary>
        public string INTERESTRATE { get; set; }
        /// <summary>
        /// 税金
        /// </summary>
        public string TAXES { get; set; }
        /// <summary>
        /// 生成日报 1-允许 0-禁止 Flag of generating daily reports (1 - reports are allowed, 0 - reports are prohibited)
        /// </summary>
        public string SEND_REPORTS { get; set; }
        /// <summary>
        /// 分配给每个用户的一个唯一的标识符  MetaTrader4为Iphone 或Android安装过程中产生的
        /// </summary>
        public string MQID { get; set; }
        /// <summary>
        /// 账户的颜色
        /// </summary>
        public string USER_COLOR { get; set; }
        /// <summary>
        /// 权益，资产净值
        /// </summary>
        public string EQUITY { get; set; }
        /// <summary>
        /// 保留利润的大小(保证金)
        /// </summary>
        public string MARGIN { get; set; }
        /// <summary>
        /// 保证金水平
        /// </summary>
        public string MARGIN_LEVEL { get; set; }
        /// <summary>
        /// Size of free margin
        /// </summary>
        public string MARGIN_FREE { get; set; }

        public string CURRENCY { get; set; }
        /// <summary>
        /// 记录在数据库最后修改的时间
        /// </summary>
        public string MODIFY_TIME { get; set; }
    }
}
