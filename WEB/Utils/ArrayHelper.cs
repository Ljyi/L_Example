using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB.Utils
{
    public static class ArrayHelper
    {
        /// <summary>
        /// 用指定符号串连数组列表（默认为逗号）
        /// </summary>
        /// <param name="separator">分隔符号</param>
        /// <param name="addSingleQuotes">是否添加单引号</param>
        public static string ToSeparateString(this IEnumerable<string> list, bool isAddSingleQuotes, char separator = ',')
        {
            if (list == null || list.Count() == 0) return string.Empty;
            string result = "";
            if (isAddSingleQuotes)
            {
                foreach (var item in list)
                {
                    result += string.Format("'{0}'{1}", item, separator);
                }
            }
            else
            {
                foreach (var item in list)
                {
                    result += string.Format("{0}{1}", item, separator);
                }
            }
            return result.TrimEnd(separator);
        }
        /// <summary>
        /// 拼接字符串并去除重复
        /// </summary>
        /// <param name="stringArr"></param>
        /// <returns>string</returns>
        public static string Merge(string[] stringArr)
        {
            string returnString = string.Empty;
            foreach (string s in stringArr)
            {
                returnString += s + ",";
            }
            returnString = returnString.Substring(0, returnString.Length - 1);
            returnString = returnString.Replace(",,", ",");
            string[] strArr = returnString.Split(',');
            ArrayList arrList = new ArrayList();
            arrList.Add(strArr[0]);
            if (strArr.Length > 1)
            {
                for (int i = 1; i < strArr.Length - 1; i++)
                {
                    if (!arrList.Contains(strArr[i]))
                    {
                        arrList.Add(strArr[i]);
                    }
                }
            }
            returnString = string.Empty;

            for (int i = 0; i < arrList.Count; i++)
            {
                returnString += arrList[i] + ",";
            }
            returnString = returnString.Substring(0, returnString.Length - 1);
            return returnString;
        }

        /// <summary>
        /// 用指定符号串连数组列表（默认为逗号）
        /// </summary>
        /// <param name="separator">分隔符号</param>
        /// <param name="addSingleQuotes">是否添加单引号</param>
        public static string ToSeparateString(this IEnumerable<int> list, bool isAddSingleQuotes, char separator = ',')
        {
            return ToSeparateString(list.Select(p => p.ToString()), isAddSingleQuotes, separator);
        }
        /// <summary>
        /// 用指定符号串连数组列表（默认为逗号）
        /// </summary>
        /// <param name="separator">分隔符号</param>
        public static string ToSeparateString(this IEnumerable<string> list, char separator = ',')
        {
            return ToSeparateString(list, false, separator);
        }
        /// <summary>
        /// 用指定符号串连数组列表（默认为逗号）
        /// </summary>
        /// <param name="separator">分隔符号</param>
        public static string ToSeparateString(this IEnumerable<int> list, char separator = ',')
        {
            return ToSeparateString(list.Select(p => p.ToString()), separator);
        }
        /// <summary>
        /// 用指定符号串连数组列表（默认为逗号）
        /// </summary>
        /// <param name="separator">分隔符号</param>
        public static string ToSeparateString(this IEnumerable<decimal> list, char separator = ',')
        {
            return ToSeparateString(list.Select(p => p.ToString()), separator);
        }
        /// <summary>
        /// 用指定符号串连数组列表（默认为逗号）
        /// </summary>
        /// <param name="separator">分隔符号</param>
        public static string ToSeparateString(this IEnumerable<float> list, char separator = ',')
        {
            return ToSeparateString(list.Select(p => p.ToString()), separator);
        }
        /// <summary>
        /// 用指定符号串连数组列表（默认为逗号）
        /// </summary>
        /// <param name="separator">分隔符号</param>
        public static string ToSeparateString(this IEnumerable<double> list, char separator = ',')
        {
            return ToSeparateString(list.Select(p => p.ToString()), separator);
        }
        public static bool Compare(this IEnumerable<string> list, IEnumerable<string> listCompare)
        {
            bool IsSame = false;
            int count = 0;
            if (list.Count() == listCompare.Count())
            {
                foreach (string str in list)
                {
                    if (listCompare.Contains(str))
                        count += 1;
                }
            }
            if (count == list.Count())
                IsSame = true;
            return IsSame;
        }
        /// <summary>
        /// 字符串转化成整型数组
        /// </summary>
        /// <param name="separator"></param>
        public static int[] ToIntArray(this string str, char separator = ',')
        {
            string[] strArray = str.Split(separator);
            return Array.ConvertAll<string, int>(strArray, delegate (string s) { return int.Parse(s); });
        }

        /// <summary>
        /// 去除字符串字符串数组中 重复的字符串
        /// 输入："qw,de,we,qw"
        /// 输出："qw,de,we"
        /// </summary>
        /// <param name="str">字符串数组</param>
        /// <returns></returns>
        public static string ToRemoveRepeat(this string str)
        {
            return string.Join(",", str.Split(',').Distinct().ToArray());
        }
        /// <summary>  
        /// 字符串替换方法  
        /// </summary>  
        /// <param name="myStr">需要替换的字符串</param>  
        /// <param name="replaceStr">需要替换的字符</param>  
        /// <param name="replaceWord">将替换为</param>  
        /// <returns></returns>  
        public static string ToReplace(this string str, string replaceStr, string replaceWord = "")
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(replaceStr))
            {
                return str;
            }
            var StrList = replaceStr.Split(',').ToList();
            foreach (var item in StrList)
            {
                str.Replace(item, replaceWord);
            }
            return str;
        }
        /// <summary>
        /// 查找字符串中重复出现最多的字符
        /// </summary>
        /// <param name="str">字符串 例："AS,SD,DE,DE,AS"</param>
        /// <returns>返回出现最多的字符串</returns>
        public static string MaxNumString(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            var StrList = str.Split(',').ToList();
            Dictionary<string, int> strDictionary = new Dictionary<string, int>();
            string max = StrList[0];
            foreach (var item in StrList)
            {
                if (strDictionary.ContainsKey(item))
                {
                    strDictionary[item]++;
                }
                else
                {
                    strDictionary.Add(item, 1);
                }
                if (strDictionary[max] < strDictionary[item]) max = item;
            }
            return max;
        }
    }
}