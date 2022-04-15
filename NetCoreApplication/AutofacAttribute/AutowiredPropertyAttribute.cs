using Autofac.Core;
using System.Reflection;

namespace NetCoreApplication.AutofacAttribute
{
    //为了支持属性注入，只能打到属性上
    [AttributeUsage(AttributeTargets.Property)]
    public class AutowiredAttribute : Attribute
    {
    }
    /// <summary>
    /// IPropertySelector:查看属性上是否标记某一个特性
    /// </summary>
    public class AutowiredPropertySelector : IPropertySelector
    {
        public bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            //判断属性的特性是否包含自定义的属性,标记有返回true
            return propertyInfo.CustomAttributes.Any(s => s.AttributeType == typeof(AutowiredAttribute));
        }
    }
}
