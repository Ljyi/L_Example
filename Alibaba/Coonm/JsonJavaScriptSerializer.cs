using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Alibaba.Coonm
{
    public static class JsonJavaScriptSerializer
    {
        /// <summary>
        /// 内存对象转换为json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJSON(object obj)
        {
            StringBuilder sb = new StringBuilder();
            JavaScriptSerializer json = new JavaScriptSerializer();
            json.Serialize(obj, sb);
            return sb.ToString();
        }
        /// <summary>
        /// Json字符串转内存对象
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T FromJSON<T>(string jsonString)
        {
            JavaScriptSerializer json = new JavaScriptSerializer();
            return json.Deserialize<T>(jsonString);
        }

    }
}
