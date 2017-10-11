using Mr.Box.DGM.Dal;
using Mr.Box.DGM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mr.Box.DGM.Bll
{
    // <summary>
    /// 申请真实账户
    /// </summary>
    public class ApplyForLiveAccountBll
    {

        /// <summary>
        /// 开设真实账户，录入信息到数据库
        /// </summary>
        /// <param name="account">用户基础信息</param>
        /// <param name="profe">开户者职业信息</param>
        /// <param name="personalInfo">个人信息</param>
        /// <param name="tradingInfo">从事交易信息</param>
        /// <returns>返回录入结果</returns>
        public int ApplyForLiveAccount(Account account, ProfessionInfo profe, PersonalInformation personalInfo)
        {
            try
            {
                
                //个人信息
                personalInfo = new PersonalInformationDAL().AddPersonalInformation(personalInfo);
                if (personalInfo == null) { return 0; }

                profe.PersonalInformationId = personalInfo.InformationId;

                //雇主信息
                profe = new ProfessionInfoDAL().AddProfessionInfo(profe);
                if (profe == null) { return 0; }

                //基础信息

                account.InformationId = profe.ProfessionInfoId;
                account = new AccountDAL().AddAccount(account);
                if (account == null) { return 0; }


                return account.AccountId;
            }
            catch (Exception)
            {
                return 0;
            }
        }



        /// <summary>
        /// 得到所有的开设真实账户信息
        /// </summary>
        /// <returns>返回所有的开设真实账户信息</returns>
        public List<Account> GetAccount(int offset, int pageSize, string search, string type, out int total)
        {
          return new AccountDAL().GetAccount(offset,pageSize,search,type,out total);
        }
      
        /// <summary>
        /// 根据账户ID获取账户基本信息
        /// </summary>
        /// <param name="AccountID"></param>
        /// <returns></returns>
        public Account GetAccountByAccountID(string AccountID)
        {
            Account account = null;

            account = new AccountDAL().GetAccountByAccountID(Convert.ToInt32(AccountID));

            return account;
        }

        /// <summary>
        /// 根据账户ID获取开户者职业信息
        /// </summary>
        /// <param name="AccountID">基本信息中个人信息ID</param>
        /// <returns></returns>
        public ProfessionInfo GetProfessionInfoByAccountInformationId(string AccountInformationId)
        {
            ProfessionInfo account = null;

            account = new ProfessionInfoDAL().GetProfessionInfoByProfessionInfoId(AccountInformationId);

            return account;
        }

        /// <summary>
        /// 根据账户ID获取账户个人信息
        /// </summary>
        /// <param name="ProfessionInfoPersonalInformationId">个人信息中的开户者信息ID</param>
        /// <returns></returns>
        public PersonalInformation GetPersonalInformationByProfessionInfoPersonalInformationId(string ProfessionInfoPersonalInformationId)
        {
            PersonalInformation account = null;

            account = new PersonalInformationDAL().GetPersonalInformationByInformationId(ProfessionInfoPersonalInformationId);

            return account;
        }

        

        public CurrencyType GetCurrencyTypeByCurrencyTypeId(string CurrencyTypeId) {
            return new CurrencyTypeDAL().GetCurrencyTypeByCurrencyTypeId(CurrencyTypeId);
        }
    }
}
