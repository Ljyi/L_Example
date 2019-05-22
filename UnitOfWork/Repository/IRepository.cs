using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Repository
{
    /// <summary>
    /// Ado.Net实现的仓储
    /// </summary>
    public interface IRepository<T>
    {
        /// <summary>
        /// 插入对象
        /// </summary>
        /// <param name="entity"></param>
        int Insert(T entity);

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="predicate"></param>
        int Update(T entity, Expression<Func<T, bool>> express);

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <param name="predicate"></param>
        int Delete(Expression<Func<T, bool>> express = null);

        /// <summary>
        /// 查询对象集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<T> QueryAll(Expression<Func<T, bool>> express = null);

        /// <summary>
        /// 查询对象集合
        /// </summary>
        /// <param name="index"></param>
        /// <param name="pagesize"></param>
        /// <param name="order"></param>
        /// <param name="asc"></param>
        /// <param name="express"></param>
        /// <returns></returns>
        List<T> QueryAll(int index, int pagesize, string order = null, bool asc = false, Expression<Func<T, bool>> express = null);

        /// <summary>
        /// 查询对象集合
        /// </summary>
        /// <param name="type"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        List<object> QueryAll(Type type, Expression<Func<T, bool>> express = null);

        /// <summary>
        /// 查询对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        T Query(Expression<Func<T, bool>> express);

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        object QueryCount(Expression<Func<T, bool>> express = null);
    }
}
