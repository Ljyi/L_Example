using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Yima.Core.Model;
using System.Reflection;
using Yima.Infrastructure.ReflectionExtend;

namespace Yima.AdoDotNet.Extension
{
    /// <summary>
    /// Sql Server 扩展类
    /// </summary>
    public static class SqlServerExtension
    {
        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <param name="parameters"></param>
        public static void SetParameters(this IDbCommand dbCommand, IDictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                return;
            }
            foreach (var parameter in parameters)
            {
                if (parameter.Value != null)
                {
                    dbCommand.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                }
                else 
                {
                    dbCommand.Parameters.Add(new SqlParameter(parameter.Key,DBNull.Value));    
                }
            }
        }

        /// <summary>
        ///  获取对应的实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="TEntity"></param>
        /// <returns></returns>
        public static TEntity GetReaderData<TEntity>(this IDataReader reader) where TEntity : class, new()
        {
            var item = new TEntity();
            var filedList = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                filedList.Add(reader.GetName(i));
            }
            //映射数据库中的字段到实体属性
            IEnumerable<PropertyInfo> propertys = typeof(TEntity).GetProperties().Where(s => filedList.Contains(s.Name));
            foreach (var property in propertys)
            {
                //对实体属性进行设值
                //item.SetValue(property, reader);
                property.SetValue(item, reader[property.Name]); //这种方式也是可以的，为什么需要上面的方式？
            }
            return item;
        }

        /// <summary>
        /// 设定值
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="property"></param>
        /// <param name="reader"></param>
        /// <param name="isCache"></param>
        /// <param name="readName"></param>
        public static void SetValue<TEntity>(this TEntity entity, PropertyInfo property, IDataReader reader, bool isCache = true,
            string readName = null) where TEntity : class, new()
        {
            readName = readName ?? property.Name;
            if (property.PropertyType == typeof(string))
            {
                string value = reader.GetValue<string>(readName);
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, string>(entity, value, isCache);
            }
            else if (property.PropertyType == typeof(int))
            {
                int value = reader.GetValue<int>(readName);
                if (value == 0)
                {
                    return;
                }
                property.SetValue(entity, value);
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, int>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(int?)))
            {
                var value = reader.GetValue<int?>(readName);
                if (value == null)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, int?>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(long)))
            {
                var value = reader.GetValue<long>(readName);
                if (value == 0)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, long>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(long?)))
            {
                var value = reader.GetValue<long?>(readName);
                if (value == null)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, long?>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(decimal)))
            {
                var value = reader.GetValue<decimal>(readName);
                if (value == 0)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, decimal>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(decimal?)))
            {
                var value = reader.GetValue<decimal?>(readName);
                if (value == null)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, decimal?>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(double)))
            {
                var value = reader.GetValue<double>(readName);
                if (value == 0)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, double>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(double?)))
            {
                var value = reader.GetValue<double?>(readName);
                if (value == null)
                {
                    return;
                }
                property.SetPropertyValue<TEntity, double?>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(DateTime)))
            {
                var value = reader.GetValue<DateTime>(readName);
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, DateTime>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(DateTime?)))
            {
                var value = reader.GetValue<DateTime?>(readName);
                if (value == null)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, DateTime?>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(bool)))
            {
                var value = reader.GetValue<bool>(readName);
                if (value == false)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, bool>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(bool?)))
            {
                var value = reader.GetValue<bool?>(readName);
                if (value == null)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, bool?>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(byte[])))
            {
                var value = reader.GetValue<byte[]>(readName);
                if (value == null)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, byte[]>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(Guid)))
            {
                var value = reader.GetValue<Guid>(readName);
                if (value == null)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, Guid>(entity, value, isCache);
            }
            else if (property.PropertyType.Equals(typeof(Guid?)))
            {
                var value = reader.GetValue<Guid?>(readName);
                if (value == null)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, Guid?>(entity, value, isCache);
            }
            else
            {
                var value = reader.GetValue<object>(readName);
                if (value == null)
                {
                    return;
                }
                if (typeof(TEntity) == typeof(object))
                {
                    property.SetValue(entity, value);
                    return;
                }
                property.SetPropertyValue<TEntity, object>(entity, value, isCache);
            }
        }

        /// <summary>
        /// 根据列名获取值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static T GetValue<T>(this IDataReader reader, string columnName)
        {
            int index = reader.GetOrdinal(columnName);
            if (reader.IsDBNull(index))
            { 
                return default(T);
            }
            return (T)reader[index];
        }

        /// <summary>
        /// 获取对应的实体
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetReaderData(this IDataReader reader,Type type)
        {
            var item = Activator.CreateInstance(type);
            var filedList = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                filedList.Add(reader.GetName(i).ToLower());
            }
            var properties = (from s in type.GetProperties()
                              let name = s.Name.ToLower().Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries).LastOrDefault()
                              where filedList.Contains(s.Name)
                              select new
                              {
                                  Name = s.Name,
                                  Property = s
                              }).ToList();

            foreach (var property in properties)
            {
                property.Property.SetValue(item, reader[property.Name]);
                //item.SetValue(property.Property, reader, true, property.Name);
            }
            return item;
        }

        /// <summary>
        ///  设置属性的值扩展
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="property"></param>
        /// <param name="entity"></param>
        /// <param name="value"></param>
        /// <param name="isCache"></param>
        public static void SetPropertyValue<TEntity, TValue>(this PropertyInfo property, TEntity entity, TValue value, bool isCache = true)
            where TEntity : class
        {
            var propertyInfo = ReflectionFactory.Create<TEntity, TEntity, TValue>(property, property, isCache);

            propertyInfo.SetValue(entity, value);
        }
    }
}
