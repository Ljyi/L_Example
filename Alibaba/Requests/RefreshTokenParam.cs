using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alibaba.Requests
{
    public class RefreshTokenParam : BaseRequest
    {
        public RefreshTokenParam()
        {
            NamespaceValue = "system.oauth2";
            Name = "getToken";
            Version = 1;
            Grant_type = "refresh_token";
            RefreshToken = "123";
        }
        public string RefreshToken { get; set; }
        public string Grant_type { get; set; }
        protected override void AddParameters(SortedDictionary<string, string> dictParameters)
        {
            dictParameters.Add("grant_type", Grant_type);
            dictParameters.Add("client_id", AppKey);
            dictParameters.Add("client_secret", SecretKey);
            dictParameters.Add("refresh_token", RefreshToken);
        }
        public override void GetRequestUrl()
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
            var stringToSign = string.Join("&", m_DictParameters.Select(zw => zw.Key + "=" + zw.Value));
            URL = relativeBuilder.ToString();
            LastUrl = URL;
        }
    }
}
