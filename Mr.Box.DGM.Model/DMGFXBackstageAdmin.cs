using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    /// <summary>
    /// 网站后台管理员
    /// </summary>
    public class DmgfxBackstageAdmin
    {
        /// <summary>
        /// 管理员ID
        /// </summary>
        public string AdminId { get; set; }

        /// <summary>
        /// 管理员账号
        /// </summary>
        public string AdminAccount { get; set; }

        /// <summary>
        /// 管理员密码
        /// </summary>
        public string AdminPassword { get; set; }

        /// <summary>
        /// 管理员级别
        /// </summary>
        public string AdminLevel { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string AdminExplanation { get; set; }

        public  int IsEnable { get; set; }
    }
}
