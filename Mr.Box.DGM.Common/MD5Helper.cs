using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mr.Box.DGM.Common
{
    /// <summary>
    /// MD5 操作类
    /// </summary>
    public class MD5Helper
    {
        #region Method

        /// <summary>
        /// 获得32位的MD5加密
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static string GetMD532(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
            var sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获得16位的MD5加密
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static string GetMD516(string input)
        {
            return GetMD532(input).Substring(8, 16);
        }

        /// <summary>
        /// 获得8位的MD5加密
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static string GetMD58(string input)
        {
            return GetMD532(input).Substring(8, 8);
        }

        /// <summary>
        /// 获得4位的MD5加密
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static string GetMD54(string input)
        {
            return GetMD532(input).Substring(8, 4);
        }

        /// <summary>
        /// 添加MD5的前缀，便于检查有无篡改
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static string AddMD5Profix(string input)
        {
            return GetMD54(input) + input;
        }

        /// <summary>
        /// 移除MD5的前缀
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static string RemoveMD5Profix(string input)
        {
            return input.Substring(4);
        }

        /// <summary>
        /// 验证MD5前缀处理的字符串有无被篡改
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>返回值</returns>
        public static bool ValidateValue(string input)
        {
            bool res = false;
            if (input.Length >= 4)
            {
                string tmp = input.Substring(4);
                if (input.Substring(0, 4) == GetMD54(tmp))
                {
                    res = true;
                }
            }
            return res;
        }

        #endregion

        #region MD5签名验证

        /// <summary>
        /// 对给定文件路径的文件加上标签
        /// </summary>
        /// <param name="path">要加密的文件的路径</param>
        /// <returns>标签的值</returns>
        public static bool AddMD5(string path)
        {
            bool isNeed = true;

            if (CheckMD5(path)) //已进行MD5处理
                isNeed = false;

            try
            {
                var fsread = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var md5File = new byte[fsread.Length];
                fsread.Read(md5File, 0, (int)fsread.Length); // 将文件流读取到Buffer中
                fsread.Close();

                if (isNeed)
                {
                    string result = MD5Buffer(md5File, 0, md5File.Length); // 对Buffer中的字节内容算MD5
                    byte[] md5 = Encoding.ASCII.GetBytes(result); // 将字符串转换成字节数组以便写人到文件中
                    var fsWrite = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                    fsWrite.Write(md5File, 0, md5File.Length); // 将文件，MD5值 重新写入到文件中。
                    fsWrite.Write(md5, 0, md5.Length);
                    fsWrite.Close();
                }
                else
                {
                    var fsWrite = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                    fsWrite.Write(md5File, 0, md5File.Length);
                    fsWrite.Close();
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 对给定路径的文件进行验证
        /// </summary>
        /// <param name="path">要加密的文件的路径</param>
        /// <returns>是否加了标签或是否标签值与内容值一致</returns>
        public static bool CheckMD5(string path)
        {
            try
            {
                var getFile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var md5File = new byte[getFile.Length]; // 读入文件
                getFile.Read(md5File, 0, (int)getFile.Length);
                getFile.Close();

                string result = MD5Buffer(md5File, 0, md5File.Length - 32); // 对文件除最后32位以外的字节计算MD5，这个32是因为标签位为32位。
                string md5 = Encoding.ASCII.GetString(md5File, md5File.Length - 32, 32); //读取文件最后32位，其中保存的就是MD5值
                return result == md5;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        /// <param name="md5File">MD5签名文件字符数组</param>
        /// <param name="index">计算起始位置</param>
        /// <param name="count">计算终止位置</param>
        /// <returns>计算结果</returns>
        private static string MD5Buffer(byte[] md5File, int index, int count)
        {
            var getMD5 = new MD5CryptoServiceProvider();
            byte[] hashByte = getMD5.ComputeHash(md5File, index, count);
            string result = BitConverter.ToString(hashByte);

            result = result.Replace("-", "");
            return result;
        }

        #endregion



        #region DES加密解密
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="strSource">待加密字串</param>
        /// <param name="key">32位Key值</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncrypt(string strSource, byte[] key)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            sa.Key = key;
            sa.Mode = CipherMode.ECB;
            sa.Padding = PaddingMode.Zeros;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, sa.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] byt = Encoding.Unicode.GetBytes(strSource);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="strSource">待解密的字串</param>
        /// <param name="key">32位Key值</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecrypt(string strSource, byte[] key)
        {
            SymmetricAlgorithm sa = Rijndael.Create();
            sa.Key = key;
            sa.Mode = CipherMode.ECB;
            sa.Padding = PaddingMode.Zeros;
            ICryptoTransform ct = sa.CreateDecryptor();
            byte[] byt = Convert.FromBase64String(strSource);
            MemoryStream ms = new MemoryStream(byt);
            CryptoStream cs = new CryptoStream(ms, ct, CryptoStreamMode.Read);
            StreamReader sr = new StreamReader(cs, Encoding.Unicode);
            return sr.ReadToEnd();
        }
        #endregion


        #region 一个用hash实现的加密解密方法
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string EncryptStrByHash(string src)
        {
            if (src.Length == 0)
            {
                return "";
            }
            byte[] HaKey = System.Text.Encoding.ASCII.GetBytes((src + "Test").ToCharArray());
            byte[] HaData = new byte[20];
            HMACSHA1 Hmac = new HMACSHA1(HaKey);
            CryptoStream cs = new CryptoStream(Stream.Null, Hmac, CryptoStreamMode.Write);
            try
            {
                cs.Write(HaData, 0, HaData.Length);
            }
            finally
            {
                cs.Close();
            }
            string HaResult = System.Convert.ToBase64String(Hmac.Hash).Substring(0, 16);
            byte[] RiKey = System.Text.Encoding.ASCII.GetBytes(HaResult.ToCharArray());
            byte[] RiDataBuf = System.Text.Encoding.ASCII.GetBytes(src.ToCharArray());
            byte[] EncodedBytes = { };
            MemoryStream ms = new MemoryStream();
            RijndaelManaged rv = new RijndaelManaged();
            cs = new CryptoStream(ms, rv.CreateEncryptor(RiKey, RiKey), CryptoStreamMode.Write);
            try
            {
                cs.Write(RiDataBuf, 0, RiDataBuf.Length);
                cs.FlushFinalBlock();
                EncodedBytes = ms.ToArray();
            }
            finally
            {
                ms.Close();
                cs.Close();
            }
            return HaResult + System.Convert.ToBase64String(EncodedBytes);
        }
        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string DecrypStrByHash(string src)
        {
            if (src.Length < 40) return "";
            byte[] SrcBytes = System.Convert.FromBase64String(src.Substring(16));
            byte[] RiKey = System.Text.Encoding.ASCII.GetBytes(src.Substring(0, 16).ToCharArray());
            byte[] InitialText = new byte[SrcBytes.Length];
            RijndaelManaged rv = new RijndaelManaged();
            MemoryStream ms = new MemoryStream(SrcBytes);
            CryptoStream cs = new CryptoStream(ms, rv.CreateDecryptor(RiKey, RiKey), CryptoStreamMode.Read);
            try
            {
                cs.Read(InitialText, 0, InitialText.Length);
            }
            finally
            {
                ms.Close();
                cs.Close();
            }
            System.Text.StringBuilder Result = new System.Text.StringBuilder();
            for (int i = 0; i < InitialText.Length; ++i) if (InitialText[i] > 0) Result.Append((char)InitialText[i]);
            return Result.ToString();
        }


        /// <summary>
        /// 对加密后的密文重新编码,如果密文长>16,则去掉前16个字符,如果长度小于16,返回空字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string ReEncryptStrByHash(string s)
        {
            string e = EncryptStrByHash(s);
            return ((e.Length > 16) ? e.Substring(16) : "");
        }
        #endregion

        #region 3DES加密解密
        public string Encrypt3DES(string str)
        {
            //密钥
            string sKey = "wyw308";
            //    //矢量,可为空
            string sIV = "scf521";
            //    //构造对称算法
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();

            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            mCSP.Key = Convert.FromBase64String(sKey);
            mCSP.IV = Convert.FromBase64String(sIV);
            mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);
            byt = Encoding.UTF8.GetBytes(str);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }


        /// <summary>
        /// 带指定密钥和矢量的3DES加密
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sKey"></param>
        /// <param name="sIV"></param>
        /// <returns></returns>
        public string Encrypt3DES(string str, string sKey, string sIV)
        {
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            mCSP.Key = Convert.FromBase64String(sKey);
            mCSP.IV = Convert.FromBase64String(sIV);
            mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ct = mCSP.CreateEncryptor(mCSP.Key, mCSP.IV);
            byt = Encoding.UTF8.GetBytes(str);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        //解密
        public string Decrypt3DES(string Value)
        {
            string sKey = "wyw308";
            string sIV = "scf521";
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            mCSP.Key = Convert.FromBase64String(sKey);
            mCSP.IV = Convert.FromBase64String(sIV);
            mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);
            byt = Convert.FromBase64String(Value);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray());
        }


        /// <summary>
        /// 带指定密钥和矢量的3DES解密
        /// </summary>
        /// <param name="Value"></param>
        /// <param name="sKey"></param>
        /// <param name="sIV"></param>
        /// <returns></returns>
        public string Decrypt3DES(string str, string sKey, string sIV)
        {
            SymmetricAlgorithm mCSP = new TripleDESCryptoServiceProvider();
            ICryptoTransform ct;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;
            mCSP.Key = Convert.FromBase64String(sKey);
            mCSP.IV = Convert.FromBase64String(sIV);
            mCSP.Mode = System.Security.Cryptography.CipherMode.ECB;
            mCSP.Padding = System.Security.Cryptography.PaddingMode.PKCS7;
            ct = mCSP.CreateDecryptor(mCSP.Key, mCSP.IV);
            byt = Convert.FromBase64String(str);
            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();
            cs.Close();
            return Encoding.UTF8.GetString(ms.ToArray());
        }
        #endregion


        #region 一个简单的加密解密方法,只支持英文
        public static string EnCryptEnStr(string str) //倒序加1加密
        {
            byte[] by = new byte[str.Length];
            for (int i = 0;
             i <= str.Length - 1;
             i++)
            {
                by[i] = (byte)((byte)str[i] + 1);
            }
            str = "";
            for (int i = by.Length - 1;
             i >= 0;
             i--)
            {
                str += ((char)by[i]).ToString();
            }
            return str;
        }


        public static string DeCryptEnStr(string str) //顺序减1解码
        {
            byte[] by = new byte[str.Length];
            for (int i = 0;
             i <= str.Length - 1;
             i++)
            {
                by[i] = (byte)((byte)str[i] - 1);
            }
            str = "";
            for (int i = by.Length - 1;
             i >= 0;
             i--)
            {
                str += ((char)by[i]).ToString();
            }
            return str;
        }
        #endregion


        #region 一个简单的加密解密方法,在上一个的基础上支持中文
        public static string EnCryptCnStr(string str)
        {
            string htext = ""; // blank text

            for (int i = 0; i < str.Length; i++)
            {
                htext = htext + (char)(str[i] + 10 - 1 * 2);
            }
            return htext;
        }

        public static string DeCryptCnStr(string str)
        {
            string dtext = "";

            for (int i = 0; i < str.Length; i++)
            {
                dtext = dtext + (char)(str[i] - 10 + 1 * 2);
            }
            return dtext;
        }
        #endregion



        #region Url地址编码解码

        /// <summary>
        /// 编码Url地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(string url)
        {
            byte[] mByte = null;
            mByte = System.Text.Encoding.GetEncoding("GB2312").GetBytes(url);

            return System.Web.HttpUtility.UrlEncode(mByte);
        }

        /// <summary>
        /// 解码Url地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlDecode(string url)
        {
            return HttpUtility.UrlDecode(url, System.Text.Encoding.GetEncoding("GB2312"));
        }
        #endregion



    }
}
