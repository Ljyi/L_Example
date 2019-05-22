using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Utils
{
    public static class Common
    {
        /// <summary>
        /// 人民转换为大写
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static string ConvertToChineseRMB(decimal amount)
        {
            string strAmount = amount.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            string regex = Regex.Replace(strAmount, @"((?<=-|^)[^\-1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            string amountRMB = "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰";
            string UpperRMB = Regex.Replace(regex, ".", m => amountRMB[m.Value[0] - '-'].ToString());
            if (UpperRMB.EndsWith("元"))
                UpperRMB = UpperRMB + "整";
            return UpperRMB;
        }
        /// <summary>
        /// 替换字符串起始位置(开头)中指定的字符串
        /// </summary>
        /// <param name="s">源串</param>
        /// <param name="searchStr">查找的串</param>
        /// <param name="replaceStr">替换的目标串</param>
        /// <returns></returns>
        public static string TrimStarString(this string s, string searchStr, string replaceStr)
        {
            var result = s;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    return result;
                }
                if (s.Length < searchStr.Length)
                {
                    return result;
                }
                if (s.IndexOf(searchStr, 0, searchStr.Length, StringComparison.Ordinal) > -1)
                {
                    result = s.Substring(searchStr.Length);
                }
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }
        /// <summary>
        /// 替换字符串末尾位置中指定的字符串
        /// </summary>
        /// <param name="s">源串</param>
        /// <param name="searchStr">查找的串</param>
        /// <param name="replaceStr">替换的目标串</param>
        public static string TrimEndString(this string s, string searchStr, string replaceStr)
        {
            var result = s;
            try
            {
                if (string.IsNullOrEmpty(result))
                {
                    return result;
                }
                if (s.Length < searchStr.Length)
                {
                    return result;
                }
                if (s.IndexOf(searchStr, s.Length - searchStr.Length, searchStr.Length, StringComparison.Ordinal) > -1)
                {
                    result = s.Substring(0, s.Length - searchStr.Length);
                }
                return result;
            }
            catch (Exception e)
            {
                return result;
            }
        }
    }
    public class CommonS
    {
        /// <summary>
        /// 人民转换为大写
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string ConvertToChineseRMB(decimal amount, string test)
        {
            string strAmount = amount.ToString("#L#E#D#C#K#E#D#C#J#E#D#C#I#E#D#C#H#E#D#C#G#E#D#C#F#E#D#C#.0B0A");
            string regex = Regex.Replace(strAmount, @"((?<=-|^)[^\-1-9]*)|((?'z'0)[0A-E]*((?=[1-9])|(?'-z'(?=[F-L\.]|$))))|((?'b'[F-L])(?'z'0)[0A-L]*((?=[1-9])|(?'-z'(?=[\.]|$))))", "${b}${z}");
            string amountRMB = "负元空零壹贰叁肆伍陆柒捌玖空空空空空空空分角拾佰仟万亿兆京垓秭穰";
            string UpperRMB = Regex.Replace(regex, ".", m => amountRMB[m.Value[0] - '-'].ToString());
            if (UpperRMB.EndsWith("元"))
                UpperRMB = UpperRMB + "整";
            return UpperRMB;
        }
        public string ConvertToChineseRMBTest(string amount)
        {
            return amount;
        }
    }
}
