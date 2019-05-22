using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>添加后的数据实体</returns>
        T Add(T entity);
        /// <summary>
        /// 查询记录数
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns>记录数</returns>
        int Count(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>是否成功</returns>
        T Update(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <returns>是否成功</returns>
        bool Delete(T entity);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        bool DeleteAll(IEnumerable<T> entityList);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="anyLambda">查询表达式</param>
        /// <returns>布尔值</returns>
        bool Exist(Expression<Func<T, bool>> anyLambda);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="whereLambda">查询表达式</param>
        /// <returns>实体</returns>
        T Find(Expression<Func<T, bool>> whereLambda);

        /// <summary>
        /// 查找数据列表
        /// </summary>
        /// <typeparam name="S">排序</typeparam>
        /// <param name="whereLamdba">查询表达式</param>
        /// <returns></returns>
        IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba);

        /// <summary>
        /// 查找数据列表
        /// </summary>
        /// <typeparam name="S">排序</typeparam>
        /// <param name="whereLamdba">查询表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="orderLamdba">排序表达式</param>
        /// <returns></returns>
        IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba);

        /// <summary>
        /// 查找分页数据列表
        /// </summary>
        /// <typeparam name="S">排序</typeparam>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="totalRecord">总记录数</param>
        /// <param name="whereLamdba">查询表达式</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="orderLamdba">排序表达式</param>
        /// <returns></returns>
        IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba);

        List<T> FindListBySQL<T>(string sql, params object[] parameters);

        int ExecuteBySQL(string sql, params object[] parameters);

        #region 异步操作
        /// <summary>
        /// 添加实体并提交到数据服务器
        /// </summary>
        /// <param name="item">Item to add to repository</param>
        Task<int> InsertAsync(T item);

        /// <summary>
        /// 移除实体并提交到数据服务器
        /// 如果表存在约束，需要先删除子表信息
        /// </summary>
        /// <param name="item">Item to delete</param>
        Task<int> DeleteAsync(T item);

        /// <summary>
        /// 修改实体并提交到数据服务器
        /// </summary>
        /// <param name="item"></param>
        Task<int> UpdateAsync(T item);

        /// <summary>
        /// 添加集合[集合数目不大时用此方法，超大集合使用BulkInsert]
        /// </summary>
        /// <param name="item"></param>
        Task<int> InsertAsync(IEnumerable<T> item);

        /// <summary>
        /// 修改集合[集合数目不大时用此方法，超大集合使用BulkUpdate]
        /// </summary>
        /// <param name="item"></param>
        Task<int> UpdateAsync(IEnumerable<T> item);

        /// <summary>
        /// 删除集合[集合数目不大时用此方法，超大集合使用批量删除]
        /// </summary>
        /// <param name="item"></param>
        Task<int> DeleteAsync(IEnumerable<T> item);
        /// <summary>
        /// 批量添加，添加之前可以去除自增属性,默认不去除
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isRemoveIdentity"></param>
        Task BulkInsertAsync(IEnumerable<T> item, bool isRemoveIdentity);

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="item"></param>
        Task BulkInsertAsync(IEnumerable<T> item);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="item"></param>
        //  Task BulkUpdateAsync(IEnumerable<T> item, params string[] fieldParams);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="item"></param>
        //   Task BulkDeleteAsync(IEnumerable<T> item);
        #endregion
    }
}
