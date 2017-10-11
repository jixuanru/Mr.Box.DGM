using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Dal;

namespace Mr.Box.DGM.Bll
{
    public class AccountFormsBll
    {
        #region 客户信息变更表
        public AccountInformationChangingForm GetUserForChangingForm(string Login)
        {
            return new AccountFormsDal().GetUserForChangingForm(Login);
        }

        public bool AccountInformationChangingForm(string AccountName, string AccountEmail, string AccountCountry, string AccountProvince,
            string AccountAddress, string AccountMobile, string HirerName, string HirerAddress, string HirerZipCode,string HirerCountry, string HirerProvince)
        {
            AccountInformationChangingForm Info=new Model.AccountInformationChangingForm{
                AccountName=AccountName,
                AccountEmail=AccountEmail,
                AccountCountry=AccountCountry,
                AccountProvince=AccountProvince,
                AccountAddress=AccountAddress,
                AccountMobile=AccountMobile,
                HirerName=HirerName,
                HirerAddress=HirerAddress,
                HirerCountry=HirerCountry,
                HirerProvince=HirerProvince,
                HirerZipcode=HirerZipCode
            };
            return new AccountFormsDal().AccountInformationChangingForm(Info);
        }
        #endregion
    }
}
