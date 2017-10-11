using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    /// <summary>
    /// 个人信息类
    /// </summary>
    [Serializable]
    public class PersonalInformation
    {

        /// <summary>
        /// 个人信息编号
        /// </summary>
        public string InformationId { get; set; }
        
        /// <summary>
        /// 所属城市
        /// </summary>
        public string Country { get; set; } 
        
        /// <summary>
        /// 所在省份
        /// </summary>
        public string Province { get; set; }
        
        /// <summary>
        /// 所在城市
        /// </summary>
        public string City { get; set; } 
        
        /// <summary>
        /// 居住具体地址
        /// </summary>
        public string LiveAddress { get; set; }
        
        /// <summary>
        /// 电话号码
        /// </summary>
        public string PhoneNumber { get; set; }
        
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string HomePhone { get; set; }
        
        /// <summary>
        /// 出生日期
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 所在国籍
        /// </summary>
        public string Nationality { get; set; } 

        /// <summary>
        /// 证件签发国
        /// </summary>
        public string CredentialsFromCountry { get; set; }  

        /// <summary>
        /// 证件类型
        /// </summary>
        public string CredentialsType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string CredentialsNumber { get; set; } 

        /// <summary>
        /// 说明
        /// </summary>
        public string Explanation { get; set; } 

        /// <summary>
        /// 平台类型
        /// </summary>
        public string AccountBelongsPlatformType { get; set; }
    }
}
