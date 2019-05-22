using Alibaba.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Alibaba
{
    public class HttpClientRequest
    {
        /// <summary>
        /// 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public ResponseResult RequesResult(string url, string postDataStr, string method = "post")
        {
            //    postDataStr = System.Web.HttpUtility.UrlEncode(postDataStr, Encoding.UTF8);
            ResponseResult responseResult = new ResponseResult() { ErrorMsg = "", Result = "", Success = true };
            StringBuilder queryBuilder = new StringBuilder();
            if ("GET".Equals(method.ToUpper()))
            {
                string uriStr = url;
                if (!string.IsNullOrEmpty(postDataStr))
                {
                    uriStr = uriStr + "?" + postDataStr;
                }
                Uri uri = new Uri(uriStr);
                HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
                httpWebRequest.Method = "GET";
                httpWebRequest.KeepAlive = false;
                httpWebRequest.AllowAutoRedirect = true;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:5.0.1) Gecko/20100101 Firefox/5.0.1";
                HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse;
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string result = reader.ReadToEnd();
                responseResult.Result = result;
            }
            else
            {
                byte[] bytesToPost = Encoding.UTF8.GetBytes(postDataStr);//转换请求数据为二进制
                Uri uri = new Uri(url);
                HttpWebRequest httpWebRequest = WebRequest.Create(uri) as HttpWebRequest;
                httpWebRequest.Method = "POST";
                httpWebRequest.KeepAlive = false;
                httpWebRequest.AllowAutoRedirect = true;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";//application/json; charset=UTF-8
                httpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:5.0.1) Gecko/20100101 Firefox/5.0.1";
                httpWebRequest.ContentLength = bytesToPost.Length;
                System.IO.Stream outputStream = httpWebRequest.GetRequestStream();
                outputStream.Write(bytesToPost, 0, bytesToPost.Length);
                outputStream.Close();
                try
                {
                    HttpWebResponse response = httpWebRequest.GetResponse() as HttpWebResponse;
                    Stream responseStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(responseStream);
                    string result = reader.ReadToEnd();
                    responseResult.Result = result;
                }
                catch (System.Net.WebException webException)
                {
                    HttpWebResponse response = webException.Response as HttpWebResponse;
                    Stream responseStream = response.GetResponseStream();
                    string errorMsg = webException.Message;
                    responseResult.Success = false;
                    responseResult.ErrorMsg = errorMsg;
                }
            }
            return responseResult;
        }

        public static string get_uft8(string unicodeString)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(unicodeString);
            String decodedString = utf8.GetString(encodedBytes);
            return decodedString;
        }
    }
}
