using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Requests
{
    public abstract class BaseRequest
    {
        public BaseRequest(string user = "")
        {
            URL = "https://gw.open.1688.com/openapi/param2";
            AppKey = "6368408";
            SecretKey = "oOIhhdRl03YG";
            AccessToken = "62af211a-d23e-4683-b189-032c25f85d18";
            WebSite = "1688";
            Version = 1;
            RequestMethod = "Post";
        }
        public SortedDictionary<string, string> m_DictParameters = new SortedDictionary<string, string>();
        protected abstract void AddParameters(SortedDictionary<string, string> dictParameters);
        /// <summary>
        /// key
        /// </summary>
        public string AppKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string NamespaceValue;
        /// <summary>
        /// 
        /// </summary>
        public string Name;
        /// <summary>
        /// 签名Url
        /// </summary>
        public string UrlPath { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public int Version;
        public string URL { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string Timestamp { get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public string LastUrl { get; set; }

        public string WebSite { get; set; }

        public bool IsSigna { get; set; }

        public string RequestType { get; set; }

        public string RequestMethod { get; set; }
        /// <summary>
        /// 获取Url
        /// </summary>
        /// <returns></returns>
        public virtual void GetRequestUrl()
        {
            StringBuilder relativeBuilder = new StringBuilder();
            relativeBuilder.Append(URL);
            relativeBuilder.Append("/").Append(Version);
            relativeBuilder.Append("/").Append(NamespaceValue);
            relativeBuilder.Append("/").Append(Name);
            relativeBuilder.Append("/").Append(AppKey);
            m_DictParameters = new SortedDictionary<string, string>();
            AddParameters(m_DictParameters);
            if (!string.IsNullOrEmpty(Timestamp))
            {
                m_DictParameters.Add("_aop_timestamp", Timestamp);
            }
            m_DictParameters.Add("webSite", WebSite);
            if (RequestType == "GetOrder")
            {
                var stringToSign = string.Join("&", m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
                m_DictParameters.Add("access_token", AccessToken);
                URL = relativeBuilder.ToString();
                SetUrlPath();
                Signature = GetSign(UrlPath, m_DictParameters, SecretKey);
                URL = URL + "?" + "_aop_signature=" + Signature;
                LastUrl = URL + stringToSign;
            }
            else if (RequestType == "CancelOrder" || RequestType == "CreateOrder" || RequestType == "GetProduct" || RequestType == "SyncProductsPushed" || RequestType == "CreateOrderPreview" || RequestType == "GetSubAccounts" || RequestType == "SubaccountAuthCancel")
            {
                var stringToSign = string.Join("&", m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
                m_DictParameters.Add("access_token", AccessToken);
                URL = relativeBuilder.ToString();
                SetUrlPath();
                Signature = GetSign(UrlPath, m_DictParameters, SecretKey);
                m_DictParameters.Add("_aop_signature", Signature);
                URL = URL;
                LastUrl = URL + stringToSign;
            }
            else if (RequestType == "ReceiveAddress")
            {
                var stringToSign = string.Join("&", m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
                m_DictParameters.Add("access_token", AccessToken);
                URL = relativeBuilder.ToString();
                SetUrlPath();
                Signature = GetSign(UrlPath, m_DictParameters, SecretKey);
                if (RequestMethod == "Get")
                {
                    LastUrl = URL;
                    m_DictParameters.Add("_aop_signature", Signature);
                }
                else
                {
                    URL = URL + "?" + "_aop_signature=" + Signature;
                    LastUrl = URL + stringToSign;
                }

            }
            else if (RequestType == "SubaccountAuth" || RequestType == "SubaccountAuthInfo")
            {
                m_DictParameters.Remove("webSite");
                m_DictParameters.Add("access_token", AccessToken);
                URL = relativeBuilder.ToString();
                SetUrlPath();
                Signature = GetSign(UrlPath, m_DictParameters, SecretKey);
                m_DictParameters.Add("_aop_signature", Signature);
            }

        }
        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="urlPath"></param>
        /// <param name="paramDic"></param>
        /// <param name="SecretKey"></param>
        /// <returns></returns>
        private string GetSign(string urlPath, SortedDictionary<string, string> paramDic, string SecretKey)
        {
            byte[] signatureKey = Encoding.UTF8.GetBytes(SecretKey);
            List<string> list = new List<string>();
            foreach (KeyValuePair<string, string> kv in paramDic)
            {
                list.Add(kv.Key + kv.Value);
            }
            list.Sort();
            string tmp = urlPath;
            foreach (string kvstr in list)
            {
                tmp = tmp + kvstr;
            }
            HMACSHA1 hmacsha1 = new HMACSHA1(signatureKey);
            hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(tmp));
            byte[] hash = hmacsha1.Hash;
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToUpper();
        }

        /// <summary>
        /// 获取签名Url
        /// </summary>
        private void SetUrlPath()
        {
            UrlPath = "param2";
            StringBuilder relativeBuilder = new StringBuilder();
            relativeBuilder.Append("/").Append(Version);
            relativeBuilder.Append("/").Append(NamespaceValue);
            relativeBuilder.Append("/").Append(Name);
            relativeBuilder.Append("/").Append(AppKey);
            UrlPath += relativeBuilder.ToString();
        }
    }
}
