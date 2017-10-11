using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    /// <summary>
    /// 账户存款
    /// </summary>
    [Serializable]
    public class DepositAccount
    {
        /// <summary>
        /// 存款ID
        /// </summary>
        public string DepositId { get; set; }

        /// <summary>
        /// 账户编号
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// 存款金额
        /// </summary>
        public string DepositMoney { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Explanation { get; set; }

        /// <summary> 
        /// 订单编号
        /// </summary>
        public string Billno { get; set; }

        /// <summary>
        /// //商户编号
        /// </summary>
        public string Mercode  { get; set; }

        /// <summary>
        /// //币种
        /// </summary>
        public string Currency_Type  { get; set; }


        /// <summary>
        /// //订单金额
        /// </summary>
        public string OrderAmount  { get; set; }

        /// <summary>
        /// //订单日期
        /// </summary>
        public string OrderDate  { get; set; }
        
        /// <summary>
        /// //成功标示
        /// </summary>
        public string SymbolSuccess  { get; set; }

        /// <summary>
        /// //发卡行的返回信息
        /// </summary>
        public string BankReturnMsg  { get; set; }

        /// <summary>
        /// //商户数据包
        /// </summary>
        public string AttachData  { get; set; }

        /// <summary>
        /// //IPS订单号
        /// </summary>
        public string IPSBillno  { get; set; }

        /// <summary>
        /// //交易返回签名方式
        /// </summary>
        public string Retencodetype  { get; set; }

        /// <summary>
        /// //数字签名信息
        /// </summary>
        public string SignatureInfo  { get; set; }
        
        /// <summary>
        /// 存款账号所属平台
        /// </summary>
        public string PlatformType { get; set; }
        /// <summary>
        /// 支付平台 
        /// 网银支付
        /// 支付宝
        /// 微信
        /// </summary>
        public string PayType { get; set; }
    }
}
