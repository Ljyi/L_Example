using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yima.AdoDotNet.Repository;
using Yima.AdoDotNet.UnitOfWork;
using Yima.Infrastructure.Attributes;
using Yima.Infrastructure.ExpressionHelper;
using Yima.Infrastructure;

namespace Yima.AdoDotNet
{
    /// <summary>
    /// 基础库实现
    /// </summary>
    public abstract class BaseRepository<T> : IRepository<T>
        where T:class,new()
    {
        private IUnitOfWork unitOfWork;

        private IUnitOfWorkContext context;

        public BaseRepository(IUnitOfWork unitOfWork, IUnitOfWorkContext context)
        {
            this.unitOfWork = unitOfWork;
            this.context = context;
        }

        Lazy<ConditionBuilder> builder = new Lazy<ConditionBuilder>();

        public string tableName {
            get
            {
                TableNameAttribute attr= (TableNameAttribute)typeof(T).GetCustomAttribute(typeof(TableNameAttribute));
                return attr.Name; 
            }
        }

        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="entity"></param>
        public virtual int Insert(T entity)
        {
            Func<PropertyInfo[], string, IDictionary<string, object>, int> excute = (propertys, condition, parameters) =>
            {
                List<string> names = new List<string>();
                foreach (PropertyInfo property in propertys)
                {
                    if (property.GetCustomAttribute(typeof(IncrementAttribute)) == null)
                    {
                        string attrName = property.Name;
                        object value = property.GetValue(entity);
                        names.Add(string.Format("@{0}", attrName));
                        parameters.Add(attrName, value);
                    }
                }
                string sql = "Insert into {0} values({1})";
                string combineSql = string.Format(sql, tableName, string.Join(",", names), builder.Value.Condition);
                return unitOfWork.Command(combineSql, parameters);
            };
            return CreateExcute<int>(null, excute);
        }

        /// <summary>
        /// 修改对象
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="express"></param>
        public virtual int Update(T entity, Expression<Func<T, bool>> express)
        {

            Func<PropertyInfo[], string, IDictionary<string, object>, int> excute = (propertys, condition, parameters) =>
            {
                List<string> names = new List<string>();
                foreach (PropertyInfo property in propertys)
                {
                    if (property.GetCustomAttribute(typeof(IncrementAttribute)) == null)
                    {
                        string attrName = property.Name;
                        object value = property.GetValue(entity);
                        names.Add(string.Format("{0}=@{1}", attrName, attrName));
                        parameters.Add(attrName, value);
                    }
                }
                string sql = "update {0} set {1} where {2}";
                string combineSql = string.Format(sql, tableName, string.Join(",", names), builder.Value.Condition);
                return unitOfWork.Command(combineSql, parameters);
            };
            return CreateExcute<int>(express, excute);
        }
        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="express"></param>
        public virtual int Delete(Expression<Func<T, bool>> express = null)
        {
            Func<PropertyInfo[], string, IDictionary<string, object>, int> excute = (propertys, condition, parameters) =>
            {
                string sql = "delete from {0} {1}";
                string combineSql = string.Format(sql, tableName, condition);
                return unitOfWork.Command(combineSql, parameters);
            };
            return CreateExcute<int>(express, excute);
        }

        /// <summary>
        /// 查询对象集合
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public virtual List<T> QueryAll(Expression<Func<T, bool>> express = null)
        {
            Func<PropertyInfo[], string, IDictionary<string, object>, List<T>> excute = (propertys, condition, parameters) =>
            {
                string sql = "select {0} from {1} {2}";
                string combineSql = string.Format(sql, string.Join(",", propertys.Select(x => x.Name)), tableName, condition);
                return context.ReadValues<T>(combineSql, parameters);
            };
            return CreateExcute<List<T>>(express, excute);
        }

        /// <summary>
        /// 查询对象集合(分页)
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <param name="order"></param>
        /// <param name="asc"></param>
        /// <param name="express"></param>
        /// <returns></returns>
        public virtual List<T> QueryAll(int index,int pagesize,List<PropertyInfo> orderFields,Expression<Func<T, bool>> express = null)
        {
            Func<PropertyInfo[], string, IDictionary<string, object>, List<T>> excute = (propertys, condition, parameters) =>
            {
                if (orderFields == null) { throw new Exception("排序字段不能为空"); }
                string sql = "select * from (select {0} , ROW_NUMBER() over({1}) as rownum  from {2} {3}) as t where t.rownum >= {4} && t.rownum < {5}";
                string combineSql = string.Format(sql, string.Join(",", propertys.Select(x => x.Name)), tableName, condition);
                return context.ReadValues<T>(combineSql, parameters);
            };
            return CreateExcute<List<T>>(express, excute);
        }

        /// <summary>
        /// 查询对象集合
        /// </summary>
        /// <param name="type"></param>
        /// <param name="express"></param>
        /// <returns></returns>
        public virtual List<object> QueryAll(Type type, Expression<Func<T, bool>> express = null)
        {
            Func<PropertyInfo[], string, IDictionary<string, object>, List<object>> excute = (propertys, condition, parameters) =>
            {
                string sql = "select {0} from {1} {2}";
                string combineSql = string.Format(sql, string.Join(",", propertys.Select(x => x.Name)), tableName, condition);
                return context.ReadValues(combineSql, type, parameters);
            };
            return CreateExcute<List<object>>(express, excute);
        }

        /// <summary>
        /// 查询对象
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public virtual T Query(Expression<Func<T, bool>> express)
        {
            Func<PropertyInfo[], string, IDictionary<string, object>, T> excute = (propertys, condition, parameters) =>
            {
                string sql = "select {0} from {1} {2}";
                string combineSql = string.Format(sql, string.Join(",", propertys.Select(x => x.Name)), tableName, condition);
                return context.ExecuteReader<T>(combineSql, parameters);
            };
            return CreateExcute<T>(express, excute);
        }

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public virtual object QueryCount(Expression<Func<T, bool>> express = null)
        {
            Func<PropertyInfo[], string, IDictionary<string, object>, object> excute = (propertys, condition, parameters) =>
            {
                string sql = "select * from {0} {1}";
                string combineSql = string.Format(sql, string.Join(",", propertys.Select(x => x.Name)), tableName, condition);
                return context.ExecuteScalar(combineSql, parameters);
            };

            return CreateExcute<object>(express, excute);
        }

        private TValue CreateExcute<TValue>(Expression<Func<T, bool>> express, Func<PropertyInfo[], string, IDictionary<string, object>, TValue> excute)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            PropertyInfo[] propertys = typeof(T).GetProperties();
            string condition = "";
            if (express != null)
            {
                builder.Value.Build(express, tableName);
                condition = string.Format("where {0} ", builder.Value.Condition);
                for (int i = 0; i < builder.Value.Arguments.Length; i++)
                {
                    parameters.Add(string.Format("Param{0}", i), builder.Value.Arguments[i]);
                }
            }
            return excute(propertys, condition, parameters);
        }

        public List<T> QueryAll(int index, int pagesize, string order = null, bool asc = false, Expression<Func<T, bool>> express = null)
        {
            throw new NotImplementedException();
        }
    }
}
