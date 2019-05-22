using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Shopee.Requests
{
    public class ShopeeRequest
    {
        public static string PostRequest(string url, string data, string authSign)
        {
            var content = "";
            try
            {
                byte[] postdata = System.Text.Encoding.UTF8.GetBytes(data);
                HttpWebRequest httpWebRequest = null;
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                    httpWebRequest.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
                }
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.ContentLength = postdata.Length;
                httpWebRequest.Timeout = 180000;
                httpWebRequest.Headers.Add("Authorization", authSign);
                Stream newstream = httpWebRequest.GetRequestStream();
                newstream.Write(postdata, 0, postdata.Length);
                newstream.Close();
                HttpWebResponse myRespone = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader reader = new StreamReader(myRespone.GetResponseStream(), System.Text.Encoding.UTF8);
                content = reader.ReadToEnd();
            }
            catch (WebException webEx)
            {
                using (HttpWebResponse response = webEx.Response as HttpWebResponse)
                {
                    string exMsg = "";
                    if (response != null)
                    {
                        using (Stream st = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(st))
                            {
                                exMsg = reader.ReadToEnd();
                            }
                        }
                        throw new WebException(exMsg);
                    }
                    else
                    {
                        throw webEx;
                    }
                }
            }
            return content;
        }
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
    }
}
