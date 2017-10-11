using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mr.Box.DGM.Model
{
    [Serializable]
    public class TableList
    {
        /// <summary>
        /// TableID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 用户账号
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 确认时间
        /// </summary>
        public string UpdateTime { get; set; }

        /// <summary>
        /// 表格类型
        /// </summary>
        public string TableType { get; set; }

        /// <summary>
        /// 明细表中的ID
        /// </summary>
        public string TableDetailID { get; set; }

        /// <summary>
        /// 明细表名称
        /// </summary>
        public string TableDetailName { get; set; }

    }
}
