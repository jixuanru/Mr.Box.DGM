using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;

namespace Mr.Box.DGM.Common
{
    public class IPFormat
    {
        public static readonly int HeaderLength = 8;
        public static readonly int IndexRecLength = 7;
        public static readonly int IndexOffset = 3;
        public static readonly int RecOffsetLength = 3;

        public static readonly string UnknownCountry = "未知的国家";
        public static readonly string UnknownZone = "未知的地区";

        public static uint ToUint(byte[] val)
        {
            if (val.Length > 4) throw new ArgumentException();
            if (val.Length < 4)
            {
                byte[] copyBytes = new byte[4];
                Array.Copy(val, 0, copyBytes, 0, val.Length);
                return BitConverter.ToUInt32(copyBytes, 0);
            }
            else
            {
                return BitConverter.ToUInt32(val, 0);
            }
        }
    }

     public class IPLocation
     {
         private IPAddress m_ip;
         private string m_country;
         private string m_loc;

         public IPLocation(IPAddress ip, string country, string loc)
         {
             m_ip = ip;
             m_country = country;
             m_loc = loc;
         }

         public IPAddress IP
         {
             get { return m_ip; }
         }

         public string Country
         {
             get { return m_country; }
         }

         public string Zone
         {
             get { return m_loc; }
         }
     }

     /// <summary> 
     /// This class used to control ip seek 
     /// </summary> 
     public class IPSeeker
     {
         private string m_libPath;
         private uint m_indexStart;
         private uint m_indexEnd;
         public IPSeeker(string libPath)
         {
             m_libPath = libPath;
             //Locate the index block 
             using (FileStream fs = new FileStream(m_libPath, FileMode.OpenOrCreate, FileAccess.Read))
             {

                 BinaryReader reader = new BinaryReader(fs);
                 Byte[] header = reader.ReadBytes(IPFormat.HeaderLength);
                 m_indexStart = BitConverter.ToUInt32(header, 0);
                 m_indexEnd = BitConverter.ToUInt32(header, 4);

             }
         }

         /// <summary> 
         /// 输入IP地址，获取IP所在的地区信息 
         /// </summary> 
         /// <param name="ip">待查询的IP地址</param> 
         /// <returns></returns> 
         public IPLocation GetLocation(IPAddress ip)
         {
             using (FileStream fs = new FileStream(m_libPath, FileMode.Open, FileAccess.Read))
             {
                 BinaryReader reader = new BinaryReader(fs);
                 //Because it is network order(BigEndian), so we need to transform it into LittleEndian 
                 Byte[] givenIpBytes = BitConverter.GetBytes(IPAddress.NetworkToHostOrder(BitConverter.ToInt32(ip.GetAddressBytes(), 0)));
                 uint offset = FindStartPos(fs, reader, m_indexStart, m_indexEnd, givenIpBytes);
                 return GetIPInfo(fs, reader, offset, ip, givenIpBytes);
             }
         }

         #region private method
         private uint FindStartPos(FileStream fs, BinaryReader reader, uint m_indexStart, uint m_indexEnd, byte[] givenIp)
         {
             uint givenVal = BitConverter.ToUInt32(givenIp, 0);
             fs.Position = m_indexStart;

             while (fs.Position <= m_indexEnd)
             {
                 Byte[] bytes = reader.ReadBytes(IPFormat.IndexRecLength);
                 uint curVal = BitConverter.ToUInt32(bytes, 0);
                 if (curVal > givenVal)
                 {
                     fs.Position = fs.Position - 2 * IPFormat.IndexRecLength;
                     bytes = reader.ReadBytes(IPFormat.IndexRecLength);
                     byte[] offsetByte = new byte[4];
                     Array.Copy(bytes, 4, offsetByte, 0, 3);
                     return BitConverter.ToUInt32(offsetByte, 0);
                 }
             }
             return 0;
         }

         private IPLocation GetIPInfo(FileStream fs, BinaryReader reader, long offset, IPAddress ipToLoc, Byte[] ipBytes)
         {
             fs.Position = offset;
             //To confirm that the given ip is within the range of record IP range 
             byte[] endIP = reader.ReadBytes(4);
             uint endIpVal = BitConverter.ToUInt32(endIP, 0);
             uint ipVal = BitConverter.ToUInt32(ipBytes, 0);
             if (endIpVal < ipVal) return null;

             string country;
             string zone;
             //Read the Redirection pattern byte 
             Byte pattern = reader.ReadByte();
             if (pattern == RedirectMode.Mode_1)
             {
                 Byte[] countryOffsetBytes = reader.ReadBytes(IPFormat.RecOffsetLength);
                 uint countryOffset = IPFormat.ToUint(countryOffsetBytes);

                 if (countryOffset == 0) return GetUnknownLocation(ipToLoc);

                 fs.Position = countryOffset;
                 if (fs.ReadByte() == RedirectMode.Mode_2)
                 {
                     return ReadMode2Record(fs, reader, ipToLoc);
                 }
                 else
                 {
                     fs.Position--;
                     country = ReadString(reader);
                     zone = ReadZone(fs, reader, Convert.ToUInt32(fs.Position));
                 }
             }
             else if (pattern == RedirectMode.Mode_2)
             {
                 return ReadMode2Record(fs, reader, ipToLoc);
             }
             else
             {
                 fs.Position--;
                 country = ReadString(reader);
                 zone = ReadZone(fs, reader, Convert.ToUInt32(fs.Position));
             }
             return new IPLocation(ipToLoc, country, zone);

         }

         //When it is in Mode 2 
         private IPLocation ReadMode2Record(FileStream fs, BinaryReader reader, IPAddress ip)
         {
             uint countryOffset = IPFormat.ToUint(reader.ReadBytes(IPFormat.RecOffsetLength));
             uint curOffset = Convert.ToUInt32(fs.Position);
             if (countryOffset == 0) return GetUnknownLocation(ip);
             fs.Position = countryOffset;
             string country = ReadString(reader);
             string zone = ReadZone(fs, reader, curOffset);
             return new IPLocation(ip, country, zone);
         }

         //return a Unknown Location 
         private IPLocation GetUnknownLocation(IPAddress ip)
         {
             string country = IPFormat.UnknownCountry;
             string zone = IPFormat.UnknownZone;
             return new IPLocation(ip, country, zone);
         }
         //Retrieve the zone info 
         private string ReadZone(FileStream fs, BinaryReader reader, uint offset)
         {
             fs.Position = offset;
             byte b = reader.ReadByte();
             if (b == RedirectMode.Mode_1 || b == RedirectMode.Mode_2)
             {
                 uint zoneOffset = IPFormat.ToUint(reader.ReadBytes(3));
                 if (zoneOffset == 0) return IPFormat.UnknownZone;
                 return ReadZone(fs, reader, zoneOffset);
             }
             else
             {
                 fs.Position--;
                 return ReadString(reader);
             }
         }

         private string ReadString(BinaryReader reader)
         {
             List<byte> stringLst = new List<byte>();
             byte byteRead = 0;
             while ((byteRead = reader.ReadByte()) != 0)
             {
                 stringLst.Add(byteRead);
             }
             return Encoding.GetEncoding("gb2312").GetString(stringLst.ToArray());
         }
         #endregion
     }

     public class RedirectMode
     {
         public static readonly int Mode_1 = 1;
         public static readonly int Mode_2 = 2;
     }

     public class IPHelper 
     {
         /// <summary>
         /// 获取当前用户IP(无视代理)
         /// 返回城市  IP
         /// </summary>
         /// <returns></returns>
         public static string GetIp()
         {
             string _ip = "";
             if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
                 _ip= HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
             else
                 _ip= HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

             string path =HttpContext.Current.Server.MapPath("..\\Content\\dll\\qqwry.dat");
             IPAddress ip = IPAddress.Parse(_ip);
             IPLocation location = new IPSeeker(path).GetLocation(ip);
             return location.Country + location.Zone + location.IP;
         }
     }
}
