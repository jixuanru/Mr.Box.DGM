using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    /// <summary>
    /// 取款信息类
    /// </summary>
    [Serializable]
    public class AccountWithdrawals
    {
        /// <summary>
        /// 取款ID
        /// </summary>
        public string WithdrawalsId { get; set; }

        /// <summary>
        /// 持有人姓名
        /// </summary>
        public string WithdrawalsAccountHolder { get; set; }

        /// <summary>
        /// 交易账户
        /// </summary>
        public string WithdrawalsAccount { get; set; }

        /// <summary>
        /// 申请取款时间
        /// </summary>
        public string WithdrawalsDate { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string WithdrawalsEmail { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string WithdrawalsAddress { get; set; }

        /// <summary>
        /// 所在国家
        /// </summary>
        public string WithdrawalsCountry { get; set; }

        /// <summary>
        /// 所在省份
        /// </summary>
        public string WithdrawalsProvince { get; set; }

        /// <summary>
        /// 所在城市
        /// </summary>
        public string WithdrawalsCity { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string WithdrawalsCityZipCode { get; set; }

        /// <summary>
        /// 取款金额
        /// </summary>
        public string Money { get; set; }

        /// <summary>
        /// 所属平台
        /// </summary>
        public string BelongsPlatform { get; set; }

        /// <summary>
        /// 是否关闭账号
        /// </summary>
        public string IsCloseAccount { get; set; }

        /// <summary>
        /// 关闭账户原因
        /// </summary>
        public string CloseReason { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 银行账户
        /// </summary>
        public string BankAccount { get; set; }

        /// <summary>
        /// 银行持有人
        /// </summary>
        public string BankHolder { get; set; }

        /// <summary>
        /// 银行地址
        /// </summary>
        public string BankAddress { get; set; }

        /// <summary>
        /// 国际银行代码
        /// </summary>
        public string SWIFT_Code { get; set; }

        /// <summary>
        /// 银行所在国家
        /// </summary>
        public string BankCountry { get; set; }

        /// <summary>
        /// 银行所在省
        /// </summary>
        public string BankProvince { get; set; }

        /// <summary>
        /// 银行所在城市
        /// </summary>
        public string BankCity { get; set; }

        /// <summary>
        /// 银行所在城市邮编
        /// </summary>
        public string BankCityZipCode { get; set; }



        /// <summary>
        /// 是否有中转银行
        /// </summary>
        public string IsTransitBank { get; set; }

        /// <summary>
        /// 中转银行名称
        /// </summary>
        public string TransitBankName { get; set; }

        /// <summary>
        /// 中转银行账号
        /// </summary>
        public string TransitBankAccount { get; set; }

        /// <summary>
        /// 中转银行持有人姓名
        /// </summary>
        public string TransitBankHolder { get; set; }

        /// <summary>
        /// 中转银行地址
        /// </summary>
        public string TransitBankAddress { get; set; }

        /// <summary>
        /// 中转银行国际银行代码
        /// </summary>
        public string TransitBankSWIFICode { get; set; }

        /// <summary>
        /// 中转银行所在国家
        /// </summary>
        public string TransitBankCountry { get; set; }

        /// <summary>
        /// 中转银行所在省份
        /// </summary>
        public string TransitBankProvince { get; set; }

        /// <summary>
        /// 中转银行所在城市
        /// </summary>
        public string TransitBankCity { get; set; }

        /// <summary>
        /// 中转银行所在城市邮编
        /// </summary>
        public string TransitBankCityZipCode { get; set; }


        /// <summary>
        /// 主要账户持有人姓名
        /// </summary>
        public string MainAccountName { get; set; }

        /// <summary>
        /// 主要账户持有人姓名
        /// </summary>
        public string MinorAccountName { get; set; }

        /// <summary>
        /// 处理状态
        /// </summary>
        public string ProcessingStatus { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Explanation { get; set; }
    }
}
