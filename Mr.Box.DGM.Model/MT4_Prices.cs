using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    [Serializable]
    /// <summary>
    /// 货币对价格
    /// </summary>
    public class MT4_Prices
    {
        
        /// <summary>
        /// 货币对
        /// </summary>
        public string SYMBOL { get; set; }

        /// <summary>
        /// 最后报价时间
        /// </summary>
        public string TIME { get; set; }

        /// <summary>
        /// 卖价
        /// </summary>
        public string BID { get; set; }

        /// <summary>
        /// 买价
        /// </summary>
        public string ASK { get; set; }

        /// <summary>
        /// 低点
        /// </summary>
        public string LOW { get; set; }

        /// <summary>
        /// 高点
        /// </summary>
        public string HIGH { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public string DIRECTION { get; set; }

        /// <summary>
        /// 位，数字，指
        /// </summary>
        public string DIGITS { get; set; }

        /// <summary>
        /// 扩大，扩展，扩散，推广
        /// </summary>
        public string SPREAD { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public string MODIFY_TIME { get; set; }






















    }
}
