using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Model
{
    /// <summary>
    /// 下载图片
    /// </summary>
    [Serializable]
    public class UploadImage
    {
        /// <summary>
        /// 下载图片ID
        /// </summary>
        public string UploadimgId { get; set; }

        /// <summary>
        /// 账户编号
        /// </summary>
        public string AccountID { get; set; }

        /// <summary>
        /// 上传图片类型
        /// </summary>
        public string PictureTypeId { get; set; }

        /// <summary>
        /// 上传名称
        /// </summary>
        public string UploadName { get; set; }

        /// <summary>
        /// 图片储存地址
        /// </summary>
        public string PictureUrl { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public string UPloadTime { get; set; }

        /// <summary>
        /// 账户地址是否和上传人地址是否一致
        /// </summary>
        public string AddressDifferent { get; set; }
    }
}
