using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    /// <summary>
    /// 新闻信息类
    /// </summary>
    [Serializable]
    public class DGM_News
    {
        /// <summary>
        /// 新闻编号
        /// </summary>
        public string DGMNewsId { get; set; }
        
        /// <summary>
        /// 新闻标题
        /// </summary>
        public string NewsTitle { get; set; }

        /// <summary>
        /// 新闻内容
        /// </summary>
        public string NewContent { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateDate { get; set; }

        /// <summary>
        /// 文章作者
        /// </summary>
        public string NewsAuthor { get; set; }

        /// <summary>
        /// 新闻类型
        /// </summary>
        public string NewsType { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Explanation { get; set; }

        /// <summary>
        /// 是否首页显示
        /// </summary>
        public string IsHomeShow { get; set; }

        /// <summary>
        /// 首页显示内容
        /// </summary>
        public string NewsHomeText { get; set; }

        /// <summary>
        /// DGM标题图片URL
        /// </summary>
        public string DGMNewsImgUrl { get; set; }
    }
}
