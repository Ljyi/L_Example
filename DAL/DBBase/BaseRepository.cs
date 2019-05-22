using DAL.DBContext;
using DAL.DBContext.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBBase
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public DBEntity dbEF = DbContextFactory.GetCurrentContext();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Add(T entity)
        {
            dbEF.Entry<T>(entity).State = EntityState.Added;
            dbEF.SaveChanges();
            return entity;
        }
        public virtual int Count(Expression<Func<T, bool>> predicate)
        {
            return dbEF.Set<T>().AsNoTracking().Count(predicate);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual T Update(T entity)
        {
            dbEF.Set<T>().Attach(entity);
            dbEF.Entry<T>(entity).State = EntityState.Modified;
            dbEF.SaveChanges();
            return entity;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Delete(T entity)
        {
            dbEF.Set<T>().Attach(entity);
            dbEF.Entry<T>(entity).State = EntityState.Deleted;

            return dbEF.SaveChanges() > 0;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="entityList"></param>
        /// <returns></returns>
        public virtual bool DeleteAll(IEnumerable<T> entityList)
        {
            dbEF.Set<IEnumerable<T>>().Attach(entityList);
            dbEF.Entry<IEnumerable<T>>(entityList).State = EntityState.Deleted;
            return dbEF.SaveChanges() > 0;
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="anyLambda"></param>
        /// <returns></returns>
        public virtual bool Exist(Expression<Func<T, bool>> anyLambda)
        {
            return dbEF.Set<T>().AsNoTracking().Any(anyLambda);
        }

        /// <summary>
        /// 查询单个
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public virtual T Find(Expression<Func<T, bool>> whereLambda)
        {
            T _entity = dbEF.Set<T>().AsNoTracking().FirstOrDefault<T>(whereLambda);
            return _entity;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="whereLamdba"></param>
        /// <returns></returns>
        public virtual IQueryable<T> FindList(Expression<Func<T, bool>> whereLamdba)
        {
            var _list = dbEF.Set<T>().AsNoTracking().Where<T>(whereLamdba);
            return _list;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="whereLamdba"></param>
        /// <param name="isAsc"></param>
        /// <param name="orderLamdba"></param>
        /// <returns></returns>
        public virtual IQueryable<T> FindList<S>(Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            var _list = dbEF.Set<T>().AsNoTracking().Where<T>(whereLamdba);
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba);
            else _list = _list.OrderByDescending<T, S>(orderLamdba);
            return _list;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecord"></param>
        /// <param name="whereLamdba"></param>
        /// <param name="isAsc"></param>
        /// <param name="orderLamdba"></param>
        /// <returns></returns>
        public virtual IQueryable<T> FindPageList<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        {
            var _list = dbEF.Set<T>().AsNoTracking().Where<T>(whereLamdba);
            totalRecord = _list.Count();
            if (isAsc) _list = _list.OrderBy<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            else _list = _list.OrderByDescending<T, S>(orderLamdba).Skip<T>((pageIndex - 1) * pageSize).Take<T>(pageSize);
            return _list;
        }
        //public virtual PagedList<T> FindPageList1<S>(int pageIndex, int pageSize, out int totalRecord, Expression<Func<T, bool>> whereLamdba, bool isAsc, Expression<Func<T, S>> orderLamdba)
        //{
        //    var _list = dbEF.Set<T>().AsNoTracking().Where<T>(whereLamdba);
        //    totalRecord = _list.Count();
        //    PagedList<T> resultList = null;
        //    if (isAsc) resultList = _list.OrderBy<T, S>(orderLamdba).ToPagedList(pageIndex, pageSize);
        //    else resultList = _list.OrderByDescending<T, S>(orderLamdba).ToPagedList(pageIndex, pageSize);
        //    return resultList;
        //}

        /// <summary>
        /// 查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual List<T> FindListBySQL<T>(string sql, params object[] parameters)
        {
            var list = dbEF.Database.SqlQuery<T>(sql, parameters).ToList();
            return list;
        }
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual int ExecuteBySQL(string sql, params object[] parameters)
        {
            var q = dbEF.Database.ExecuteSqlCommand(sql, parameters);
            return q;
        }
        #region Fields
        /// <summary>
        /// 数据总数
        /// </summary>
        int _dataTotalCount = 0;

        /// <summary>
        /// 数据总页数
        /// </summary>
        int _dataTotalPages = 0;

        /// <summary>
        /// 数据页面大小（每次向数据库提交的记录数）
        /// </summary>
        private const int DataPageSize = 10000;

        #endregion
        /// <summary>
        /// 分页进行数据提交的逻辑
        /// </summary>
        /// <param name="item">原列表</param>
        /// <param name="method">处理方法</param>
        /// <param name="currentItem">要进行处理的新列表</param>
        private void DataPageProcess(IEnumerable<T> item, Action<IEnumerable<T>> method)
        {
            if (item != null && item.Any())
            {
                _dataTotalCount = item.Count();
                this._dataTotalPages = item.Count() / DataPageSize;
                if (_dataTotalCount % DataPageSize > 0)
                    _dataTotalPages += 1;
                for (int pageIndex = 1; pageIndex <= _dataTotalPages; pageIndex++)
                {
                    var currentItems = item.Skip((pageIndex - 1) * DataPageSize).Take(DataPageSize).ToList();
                    method(currentItems);
                }
            }
        }


        #region 异步操作
        public async Task<int> InsertAsync(T entity)
        {
            dbEF.Entry<T>(entity).State = EntityState.Added;
            return await dbEF.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(T entity)
        {
            dbEF.Entry<T>(entity).State = EntityState.Deleted;
            return await dbEF.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(T entity)
        {
            dbEF.Entry<T>(entity).State = EntityState.Modified;
            return await dbEF.SaveChangesAsync();
        }
        public async Task<int> InsertAsync(IEnumerable<T> item)
        {
            dbEF.Entry<IEnumerable<T>>(item).State = EntityState.Added;
            return await dbEF.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(IEnumerable<T> item)
        {
            dbEF.Entry<IEnumerable<T>>(item).State = EntityState.Modified;
            return await dbEF.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(IEnumerable<T> item)
        {
            dbEF.Entry<IEnumerable<T>>(item).State = EntityState.Deleted;
            return await dbEF.SaveChangesAsync();
        }
        public async Task BulkInsertAsync(IEnumerable<T> item, bool isRemoveIdentity)
        {
            dbEF.Entry<IEnumerable<T>>(item).State = EntityState.Added;
            await dbEF.SaveChangesAsync();
        }
        public async Task BulkInsertAsync(IEnumerable<T> item)
        {
            dbEF.Entry<IEnumerable<T>>(item).State = EntityState.Added;
            await dbEF.SaveChangesAsync();
        }
        #endregion

        #region
        /// <summary>  
        /// 批量插入  
        /// </summary>  
        /// <typeparam name="T">泛型集合的类型</typeparam>  
        /// <param name="conn">连接对象</param>  
        /// <param name="tableName">将泛型集合插入到本地数据库表的表名</param>  
        /// <param name="list">要插入大泛型集合</param>  
        public void BulkInsert<T>(SqlConnection conn, string tableName, IList<T> list)
        {
            using (var bulkCopy = new SqlBulkCopy(conn))
            {
                bulkCopy.BatchSize = list.Count;
                bulkCopy.DestinationTableName = tableName;

                var table = new DataTable();
                var props = TypeDescriptor.GetProperties(typeof(T)).Cast<PropertyDescriptor>().Where(propertyInfo => propertyInfo.PropertyType.Namespace.Equals("System")).ToArray();
                foreach (var propertyInfo in props)
                {
                    bulkCopy.ColumnMappings.Add(propertyInfo.Name, propertyInfo.Name);
                    table.Columns.Add(propertyInfo.Name, Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType);
                }
                var values = new object[props.Length];
                foreach (var item in list)
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        values[i] = props[i].GetValue(item);
                    }

                    table.Rows.Add(values);
                }
                bulkCopy.WriteToServer(table);
            }
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        //public string InsertPaperCertInfoListData(List<PaperCertInfo> list)
        //{
        //    string returnStr = string.Empty;
        //    DataTable dt = new DataTable();
        //    var con = dbEF.GetSqlConnection();
        //    try
        //    {
        //        dt = GetPaperCertInfolDataTable(list);
        //    }
        //    catch (Exception ex)
        //    {

        //        returnStr = "Error"; ;
        //    }
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        try
        //        {

        //            if (con.State == ConnectionState.Closed)
        //            {
        //                con.Open();
        //            }

        //            if (con.State == ConnectionState.Open)
        //            {
        //                using (SqlTransaction ts = con.BeginTransaction())
        //                {
        //                    try
        //                    {
        //                        using (SqlBulkCopy bulk = new SqlBulkCopy(con, SqlBulkCopyOptions.Default, ts))
        //                        {
        //                            bulk.BatchSize = 1000;
        //                            bulk.DestinationTableName = "dbo.TPaperCertInfo";
        //                            bulk.ColumnMappings.Clear();
        //                            bulk.ColumnMappings.Add("fPaperCertCode", "fPaperCertCode");
        //                            bulk.ColumnMappings.Add("fCertTypeID", "fCertTypeID");
        //                            bulk.ColumnMappings.Add("fCertTypeName", "fCertTypeName");
        //                            bulk.ColumnMappings.Add("fPosition", "fPosition");
        //                            bulk.ColumnMappings.Add("fCertBatchNo", "fCertBatchNo");
        //                            bulk.ColumnMappings.Add("fCertBatchName", "fCertBatchName");
        //                            bulk.ColumnMappings.Add("fStatus", "fStatus");
        //                            bulk.ColumnMappings.Add("fPrintStatus", "fPrintStatus");
        //                            bulk.ColumnMappings.Add("fMailStatus", "fMailStatus");
        //                            bulk.ColumnMappings.Add("fCreateTime", "fCreateTime");
        //                            bulk.ColumnMappings.Add("fCreateUserID", "fCreateUserID");
        //                            bulk.ColumnMappings.Add("fNotes", "fNotes");
        //                            bulk.NotifyAfter = 100;
        //                            //bulk.SqlRowsCopied += new SqlRowsCopiedEventHandler(SqlRowsCopied);
        //                            bulk.WriteToServer(dt);
        //                            ts.Commit();
        //                            //returnStr = "OK";
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {

        //                        ts.Rollback();
        //                        returnStr = "Error";
        //                    }
        //                }
        //                dt.Dispose();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            returnStr = "Error";
        //        }
        //        finally
        //        {
        //            con.Close();
        //            con.Dispose();
        //        }
        //    }
        //    return returnStr;
        //}



        /// <summary>
        /// 建立datatable映射到数据库中的表
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        //private DataTable GetPaperCertInfolDataTable(List<PaperCertInfo> list)
        //{
        //    DataTable dt = new DataTable();
        //    //dt.Columns.Add("fPaperCertID");
        //    dt.Columns.Add("fPaperCertCode");
        //    dt.Columns.Add("fCertTypeID");
        //    dt.Columns.Add("fCertTypeName");
        //    dt.Columns.Add("fPosition");
        //    dt.Columns.Add("fCertBatchNo");
        //    dt.Columns.Add("fCertBatchName");
        //    dt.Columns.Add("fStatus");
        //    dt.Columns.Add("fPrintStatus");
        //    dt.Columns.Add("fMailStatus");
        //    dt.Columns.Add("fCreateTime");
        //    dt.Columns.Add("fCreateUserID");
        //    dt.Columns["fCreateUserID"].DataType = typeof(Guid);
        //    dt.Columns.Add("fNotes");
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        DataRow dr = dt.NewRow();
        //        //dr["fPaperCertID"] = Convert.ToInt32(list[i].PaperCertID);
        //        dr["fPaperCertCode"] = list[i].PaperCertCode;
        //        dr["fCertTypeID"] = Convert.ToInt32(list[i].CertTypeID);
        //        dr["fCertTypeName"] = list[i].CertTypeName;
        //        dr["fPosition"] = list[i].Position;
        //        dr["fCertBatchNo"] = list[i].CertBatchNo;
        //        dr["fCertBatchName"] = list[i].CertBatchName;
        //        dr["fStatus"] = Convert.ToInt32(list[i].Status);
        //        dr["fPrintStatus"] = Convert.ToInt32(list[i].PrintStatus);
        //        dr["fMailStatus"] = Convert.ToInt32(list[i].MailStatus);
        //        dr["fCreateTime"] = Convert.ToDateTime(list[i].CreateTime);
        //        dr["fCreateUserID"] = list[i].CreateUserID;
        //        //dr["fCreateUserID"] = Guid.NewGuid();
        //        dr["fNotes"] = list[i].Notes;
        //        dt.Rows.Add(dr);
        //    }
        //    return dt;
        //}

        #endregion
    }

}
