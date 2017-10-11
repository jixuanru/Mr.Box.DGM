using System;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Mr.Box.DGM.Model;
using Mr.Box.DGM.Common;

namespace Mr.Box.DGM.Dal
{
    public class MT4AccountDAL
    {
        com.dgmfx.ws.mysql.WS_MYSQL ws_mysql = new com.dgmfx.ws.mysql.WS_MYSQL();


        /// <summary>
        /// 判断登录是否成功
        /// </summary>
        /// <param name="login">登录用户名</param>
        /// <param name="password">登录密码</param>
        /// <returns>返回登录结果</returns>
        public string DGMFXLogIn(string login, string password, string userRole)
        {
            Socket clientSocket = null;
            try
            {
                //const string ip1 = "202.51.130.86"; //主服
                //const string ip2 = "live.dgmfx.com"; //代理202.51.130.87
                
                IPEndPoint ipep;
                string sendMessage;
                try
                {
                    //IPAddress[] ps = Dns.GetHostAddresses(ip2);
                    IPAddress[] ps = Dns.GetHostAddresses(PublicConst.SlaveMT4Server);
                    ipep = new IPEndPoint(IPAddress.Parse(ps[0].ToString()), 443);
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    clientSocket.Connect(ipep);
                    sendMessage = "ZWAPUSER-" + login + "|" + password + "\r\nQUIT\r\n";////代理202.51.130.87
                }
                catch //主服不通  链接代理
                {
                    //ipep = new IPEndPoint(IPAddress.Parse(ip1), 443);
                    ipep = new IPEndPoint(IPAddress.Parse(PublicConst.MasterMT4Server), 443);
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    clientSocket.Connect(ipep);
                    sendMessage = "WWAPUSER-" + login + "|" + password + "\r\nQUIT\r\n";/////主服202.51.130.86
                    
                }
                //创建套接字
                clientSocket.Send(Encoding.ASCII.GetBytes(sendMessage));

                string recvStr = "";
                byte[] recvBytes = new byte[1024];
                var bytes = clientSocket.Receive(recvBytes, recvBytes.Length, 0);
                recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);

                //是否登录成功//成功返回Balance   and  Margin //失败返回Invalid Account
                if (recvStr.IndexOf("Balance", StringComparison.Ordinal) >= 0 && recvStr.IndexOf("Margin", StringComparison.Ordinal) >= 0)
                {
                    return "Success";
                }
                else if (recvStr.IndexOf("Invalid Account", StringComparison.Ordinal) >= 0 || recvStr.IndexOf("Account Disabled", StringComparison.Ordinal) >= 0)
                {
                    return "Failure";
                }
                else
                {
                    return "error";
                }
            }
            catch 
            {
                return "error";
            }
            finally
            {
                clientSocket?.Close();
            }

        }
        /// <summary>
        /// 获取MT4平台信息
        /// </summary>
        /// <param name="AccountID">用户账号</param>
        /// <returns></returns>
        public DGMUserInfo GetAccountInfo(string AccountID)
        {
            DGMUserInfo userInfo = null;

            string sql = "select * from mt4_users  where login = " + AccountID;
            try
            {
                ws_mysql.Credentials = new System.Net.NetworkCredential(PublicConst.WS_USERNAME, PublicConst.WS_USERPWD);
                DataSet myDataset = ws_mysql.ExecuteDataSetBySQL(sql, "mt4");
                //MySqlConnection conn = new MySqlConnection(MYSqlHelper.ConnectionStringManager);
                //conn.Open();
                //MySqlDataAdapter adp = new MySqlDataAdapter();
                //adp.SelectCommand = new MySqlCommand(sql, conn);
                //DataSet ds = new DataSet();
                //adp.Fill(ds);

                //conn.Close();
                //if (ds.Tables.Count > 0)
                //{
                foreach (DataRow mDr in myDataset.Tables[0].Rows)
                {
                    userInfo = new DGMUserInfo();
                    //遍历列
                    userInfo.DGMUser_Login = mDr["LOGIN"].ToString();//账号
                                                                     //account.AccountName = Convert.ToString(DBReader["GROUP"]);//所属组
                    userInfo.DGMUser_PREVMONTHBALANCE = mDr["PREVMONTHBALANCE"].ToString();//上月余额
                    userInfo.DGMUser_Balance = mDr["BALANCE"].ToString();//余额
                    userInfo.DGMUser_Equity = mDr["EQUITY"].ToString();
                    userInfo.DGMUser_PREVBALANCE = mDr["PREVBALANCE"].ToString();//上次余额
                    userInfo.DGMUser_Name = mDr["NAME"].ToString();//用户昵
                                                                   //userInfo.AccountName = Convert.ToString(DBReader["COUNTRY"]);//国家
                                                                   //userInfo.AccountName = Convert.ToString(DBReader["CITY"]);//城市
                                                                   //userInfo.AccountName = Convert.ToString(DBReader["STATE"]);//州
                                                                   //userInfo.AccountName = Convert.ToString(DBReader["ZIPCODE"]);//邮编
                    userInfo.DGMUser_Address = mDr["COUNTRY"].ToString() + "_" + mDr["CITY"].ToString() + "_" + mDr["STATE"].ToString() + "_" + mDr["ADDRESS"].ToString();//地址
                    userInfo.DGMUser_Phone = mDr["PHONE"].ToString();//电话
                    userInfo.DGMUser_Property1 = mDr["EMAIL"].ToString();//邮箱
                    userInfo.DGMUser_REGDATE = mDr["REGDATE"].ToString();//注册日期
                    userInfo.DGMUser_LEVERAGE = mDr["LEVERAGE"].ToString();//杠杆

                }
                //}
            }
            catch (Exception ex)
            {
                LogHelper.WriteSystemErrorLog("数据库连接错误！", "数据库错误", ex.Message.ToString(), @"E:/WebErrorLog/SystemErrorLog");
            }
            finally
            {
            }
            return userInfo;
        }

    }
}
