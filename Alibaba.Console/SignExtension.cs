using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Console
{
    /// <summary>
    /// 判断签名算法
    /// </summary>
    public class SignExtension
    {
        private static readonly string SecretKey = "oOIhhdRl03YG";
        private static readonly string _aop_signature = "8CC13186312161F89D416250CB360F4611746169";
        /// <summary>
        /// 判断签名算法
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="url"></param>
        /// <param name="sign">签名</param>
        /// <returns></returns>
        public static bool Validate(string appId, string sign)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(SecretKey);
            string signs = string.Empty;
                signs = _aop_signature;
            byte[] val = Encoding.UTF8.GetBytes(string.Concat(signs.OrderBy(c => c)));//排序
            string key = null;
            using (HMACSHA1 SecretKey = new HMACSHA1(bytes))
            {
                var SecretKeyBytes = SecretKey.ComputeHash(val);
                key = Convert.ToBase64String(SecretKeyBytes);
            }
            return (sign.Equals(key, StringComparison.Ordinal));
        }
    }
}
