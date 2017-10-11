using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{

    /// <summary>
    /// 交易信息
    /// </summary>
    [Serializable]
    public class MT4_Trades
    {
        /// <summary>
        /// 票据
        /// </summary>
        public string TICKET { get; set; }

        /// <summary>
        /// 账户
        /// </summary>
        public string LOGIN { get; set; }

        /// <summary>
        /// 货币对
        /// </summary>
        public string SYMBOL { get; set; }

        /// <summary>
        /// 位，数字，指
        /// </summary>
        public string DIGITS { get; set; }

        /// <summary>
        /// 订单类型 0 - BUY, 1 - SELL, 2 - BUY LIMIT, 3 - SELL LIMIT, 4 - BUY STOP, 5 - SELL STOP, 6 - BALANCE, 7 - CREDIT
        /// </summary>
        public string CMD { get; set; }

        /// <summary>
        /// 交易量/手数
        /// </summary>
        public string VOLUME { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string OPEN_TIME { get; set; }

        /// <summary>
        /// 开始价格
        /// </summary>
        public string OPEN_PRICE { get; set; }

        /// <summary>
        /// 止损Stop Loss of an order
        /// </summary>
        public string SL { get; set; }

        /// <summary>
        /// Take Profit of an order止盈
        /// </summary>
        public string TP { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string CLOSE_TIME { get; set; }

        /// <summary>
        /// Expiration date and time of pending order
        /// </summary>
        public string EXPIRATION { get; set; }

        /// <summary>
        /// Reason of an order placing
        /// Shows how an order was placed: 
        /// 0 - Client, 1 - Expert, 2 - Dealer, 3 - Signal, 4 - Gateway, 5 - Mobile, 6 - Web, 7 - API
        /// </summary>
        public string REASON { get; set; }

        /// <summary>
        /// Base symbol currency to deposit currency rate at time of order opening
        /// </summary>
        public string CONV_RATE1 { get; set; }

        /// <summary>
        /// Base symbol currency to deposit currency rate at time of order closing
        /// </summary>
        public string CONV_RATE2 { get; set; }

        /// <summary>
        /// 佣金
        /// </summary>
        public string COMMISSION { get; set; }

        /// <summary>
        /// 佣金代理
        /// </summary>
        public string COMMISSION_AGENT { get; set; }

        /// <summary>
        /// Charged swap利率交换
        /// </summary>
        public string SWAPS { get; set; }

        /// <summary>
        /// 收盘价
        /// </summary>
        public string CLOSE_PRICE { get; set; }

        /// <summary>
        /// 利润
        /// </summary>
        public string PROFIT { get; set; }

        /// <summary>
        /// 税
        /// </summary>
        public string TAXES { get; set; }

        /// <summary>
        /// 注解 Withdrawal  Deposit
        /// </summary>
        public string COMMENT { get; set; }

        /// <summary>
        /// 内部ID
        /// </summary>
        public string INTERNAL_ID { get; set; }

        /// <summary>
        /// 保证金率
        /// </summary>
        public string MARGIN_RATE { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string TIMESTAMP { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string MODIFY_TIME { get; set; }

    }
}
