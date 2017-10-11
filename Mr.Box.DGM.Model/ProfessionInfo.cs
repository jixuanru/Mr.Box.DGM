using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    /// <summary>
    /// 开户者职业信息
    /// </summary>
    [Serializable]
    public class ProfessionInfo
    {

        /// <summary>
        /// 开户者职业信息编号
        /// </summary>
        public string ProfessionInfoId { get; set; }
        
        /// <summary>
        /// 所属个人信息编号
        /// </summary>
        public string PersonalInformationId { get; set; }
        
        /// <summary>
        /// 所属行业
        /// </summary>
        public string ProfessionType { get; set; }
        
        /// <summary>
        /// 是否在英国金融机构管制工作过
        /// </summary>
        public string IsReceiveEnglandControlWork { get; set; }
        
        /// <summary>
        /// 雇主名称
        /// </summary>
        public string HirerName { get; set; }
        
        /// <summary>
        /// 工作地点所属国家
        /// </summary>
        public string HirerCountry { get; set; }
        
        /// <summary>
        /// 工作地点具体地址
        /// </summary>
        public string HirerAddress { get; set; }
        
        /// <summary>
        /// 工作地点所在城市
        /// </summary>
        public string HirerCity { get; set; }
        
        /// <summary>
        /// 工作地点所在省份
        /// </summary>
        public string HirerProvince { get; set; }
        
        /// <summary>
        /// 邮编号码
        /// </summary>
        public string ZipCode { get; set; }
        
        /// <summary>
        /// 工作业务性质
        /// </summary>
        public string NatureOfBusiness { get; set; }
        
        /// <summary>
        /// 职位
        /// </summary>
        public string Post { get; set; }
        
        /// <summary>
        /// 税前年收入
        /// </summary>
        public string BEFOREYearRevenue { get; set; }
        
        /// <summary>
        /// 储蓄和投资总额
        /// </summary>
        public string DepositAndInvestment { get; set; }
        
        /// <summary>
        /// 首笔入资金额
        /// </summary>
        public Decimal FastIntoCapitalMoney { get; set; } 
        
        /// <summary>
        /// 资金来源
        /// </summary>
        public string SourceOfFunds { get; set; }

        /// <summary>
        /// 其他来源
        /// </summary>
        public string OtherSource { get; set; }
    }
}
