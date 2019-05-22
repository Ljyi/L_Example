using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utils.Model
{
    /// <summary>
    /// 导出基类
    /// </summary>
    public class ExportBase
    {
        /// <summary>
        /// 根据属性获取值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetValue(string propertyName)
        {
            string value = "";
            try
            {
                if (!string.IsNullOrEmpty(propertyName))
                {
                    var objectValue = this.GetType().GetProperty(propertyName).GetValue(this, null);
                    if (objectValue != null)
                    {
                        value = objectValue.ToString();
                    }
                }
            }
            catch (Exception)
            {
            }
            return value;
        }
        /// <summary>
        /// 根据属性获取描述值
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public string GetDescription(string propertyName)
        {
            try
            {
                PropertyInfo item = this.GetType().GetProperty(propertyName);
                string des = ((DescriptionAttribute)Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute))).Description;// 属性值
                return des;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
