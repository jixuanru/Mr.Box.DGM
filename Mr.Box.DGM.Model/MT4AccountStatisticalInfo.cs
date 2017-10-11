using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    public class MT4AccountStatisticalInfo
    {

        /// <summary>
        /// 账户
        /// </summary>
        public string LOGIN { get; set; }

        /// <summary>
        /// 总入金数
        /// </summary>
        public string DepositCount { get; set; }

        /// <summary>
        /// 总出金数
        /// </summary>
        public string WithdrawalCount { get; set; }

        /// <summary>
        /// 已经成交总交易量
        /// </summary>
        public string VOLUMECount { get; set; }

        /// <summary>
        /// 总佣金
        /// </summary>
        public string COMMISSION { get; set; }

        /// <summary>
        /// 账户净值
        /// </summary>
        public string EQUITY { get; set; }

        /// <summary>
        /// 交易总利息
        /// </summary>
        public string SWAPS { get; set; }

        /// <summary>
        /// 账户余额
        /// </summary>
        public string BALANCE { get; set; }

        /// <summary>
        /// 账户信用额度
        /// </summary>
        public string CREDIT { get; set; }

        /// <summary>
        /// 统计属性1
        /// </summary>
        public string Property1 { get; set; }

        /// <summary>
        /// 统计属性2
        /// </summary>
        public string Property2 { get; set; }
    }
}
