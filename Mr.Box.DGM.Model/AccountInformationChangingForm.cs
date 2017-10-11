using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mr.Box.DGM.Model
{
    [Serializable]
    public class AccountInformationChangingForm
    {
        /// <summary>
        /// 用户账号（登录号）
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 用户姓名（中文）
        /// </summary>
        public string AccountExplanation { get; set; }

        /// <summary>
        /// 用户名（英文名）
        /// </summary>
        public string AccountEnName { get; set; }

        /// <summary>
        /// 账户Email
        /// </summary>
        public string AccountEmail { get; set; }

        /// <summary>
        /// 账户国家
        /// </summary>
        public string AccountCountry { get; set; }

        /// <summary>
        /// 账户州/省
        /// </summary>
        public string AccountProvince { get; set; }

        /// <summary>
        /// 账户地址
        /// </summary>
        public string AccountAddress { get; set; }

        /// <summary>
        /// 账户手机
        /// </summary>
        public string AccountMobile { get; set; }

        /// <summary>
        /// 雇主姓名
        /// </summary>
        public string HirerName { get; set; }

        /// <summary>
        /// 雇主地址
        /// </summary>
        public string HirerAddress { get; set; }

        /// <summary>
        /// 雇主国家
        /// </summary>
        public string HirerCountry { get; set; }

        /// <summary>
        /// 雇主州/省
        /// </summary>
        public string HirerProvince { get; set; }

        /// <summary>
        /// 雇主邮编
        /// </summary>
        public string HirerZipcode { get; set; }

    }
}
