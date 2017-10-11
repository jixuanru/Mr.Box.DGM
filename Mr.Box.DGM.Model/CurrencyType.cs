using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    /// <summary>
    /// 货币类型
    /// </summary>
    [Serializable]
    public class CurrencyType
    {
        /// <summary>
        /// 货币类型编号
        /// </summary>
        public string  CurrencyTypeId { get; set; }

        /// <summary>
        /// 货币类型显示名
        /// </summary>
        public string CurrencyTypeName { get; set; }

        /// <summary>
        /// 货币类型简称
        /// </summary>
        public string abbreviation { get; set; }

        /// <summary>
        /// 货币类型说明
        /// </summary>
        public string Explanation { get; set; }
    }
}
