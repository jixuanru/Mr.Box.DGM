using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Dal;

namespace Mr.Box.DGM.Bll
{
    public class FileUploadBll
    {
        /// <summary>
        /// 获得所有的图片上传信息
        /// </summary>
        /// <returns></returns>
        public IList<UploadImage> GetImages(int offset, int pageSize, string search, out int total)
        {
            return new UploadImageDAL().GetImages(offset, pageSize, search, out total);
        }

        /// <summary>
        /// 根据上传图片Id删除上传图片信息
        /// </summary>
        /// <param name="UploadImageId">上传图片Id</param>
        /// <returns>返回处理结果</returns>
        public bool DeleteUpLoadImageInfo(string UploadImageId)
        {
            bool result = false;
            result = new UploadImageDAL().DeleteCheckUploadImage(Convert.ToInt32(UploadImageId));
            return result;
        }

        public UploadImage GetUploadImageByUploadImageId(string UploadImagesId) { return new UploadImageDAL().GetUploadImageByUploadImageId(UploadImagesId); }
    }
}
