using Mr.Box.DGM.Dal;
using Mr.Box.DGM.Model;

namespace Mr.Box.DGM.Bll
{
    public class DMGFXBackstageAdminBll
    {

        public DmgfxBackstageAdmin Login(string name, string pwd) { return  new DmgfxBackstageAdminDal().Login(name,pwd);}
    }
}
