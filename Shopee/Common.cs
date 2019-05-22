using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shopee
{
    public class Common
    {
        /// <summary>
        /// 时间戳
        /// </summary>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime endTime)
        {
            TimeSpan ts = Convert.ToDateTime(endTime) - new DateTime(1970, 1, 1);
            return Convert.ToInt64(ts.TotalSeconds);
        }
        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="key"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public static string HMACSHA256Encrypt(string key, string signature)
        {
            using (HMACSHA256 hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] comByte = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(signature));
                return BitConverter.ToString(comByte).Replace("-", "").ToLower();
            }
        }
    }
}
