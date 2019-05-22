using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Extension;
using UnitOfWork.UnitOfWork;

namespace UnitOfWork
{
    /// <summary>
    /// Ado.Net的具体实现
    /// </summary>
    public abstract class UnitOfWorkContext : UnitOfWork.UnitOfWork.IUnitOfWorkContext, IDisposable
    {
        /// <summary>
        /// 数据库连接字符串标识
        /// </summary>
        public abstract string Key { get; }

        private SqlConnection connection;

        private SqlConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[Key];
                    connection = new SqlConnection(settings.ConnectionString);
                }
                return connection;
            }
        }

        /// <summary>
        /// 注册新对象到事务
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public int ExecuteNonQuery(string commandText, IDictionary<string, object> parameters = null)
        {
            Func<SqlCommand, int> excute = (commend) =>
            {
                return commend.ExecuteNonQuery();
            };
            return CreateDbCommondAndExcute<int>(commandText, parameters, excute);
        }

        /// <summary>
        /// 查询对象集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="load">自定义处理</param>
        /// <returns>泛型实体集合</returns>

        public List<T> ReadValues<T>(string commandText, IDictionary<string, object> parameters = null, Func<IDataReader, T> load = null) where T : class, new()
        {
            Func<SqlCommand, List<T>> excute = (dbCommand) =>
            {
                List<T> result = new List<T>();
                using (IDataReader reader = dbCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (load == null)
                        {
                            load = (s) => { return s.GetReaderData<T>(); };
                        }
                        var item = load(reader);
                        result.Add(item);
                    }
                    return result;
                }
            };
            return CreateDbCommondAndExcute(commandText, parameters, excute);
        }

        /// <summary>
        /// 查询对象集合
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="setItem"></param>
        /// <returns></returns>
        public List<object> ReadValues(string commandText, Type type, IDictionary<string, object> parameters = null, Action<dynamic> setItem = null)
        {
            Func<SqlCommand, List<object>> excute = (dbCommand) =>
            {
                var result = new List<object>();

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var item = dataReader.GetReaderData(type);
                        if (setItem != null)
                        {
                            setItem(item);
                        }
                        result.Add(item);
                    }
                }
                return result;
            };
            return CreateDbCommondAndExcute<List<object>>(commandText, parameters,
                excute);
        }

        /// <summary>
        /// 查询对象
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="excute"></param>
        /// <returns></returns>
        public T ExecuteReader<T>(string commandText, IDictionary<string, object> parameters = null, Func<IDataReader, T> load = null) where T : class, new()
        {
            Func<SqlCommand, T> excute = (dbCommand) =>
            {
                var result = default(T);
                using (IDataReader reader = dbCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (load == null)
                        {
                            load = (s) => { return s.GetReaderData<T>(); };
                        }
                        result = load(reader);
                    }
                    return result;
                }
            };
            return CreateDbCommondAndExcute<T>(commandText, parameters, excute);
        }

        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, IDictionary<string, object> parameters = null)
        {
            Func<SqlCommand, object> excute = (dbCommand) =>
            {
                return dbCommand.ExecuteScalar();
            };
            return CreateDbCommondAndExcute(commandText, parameters, excute);
        }

        /// <summary>
        /// 创建命令并执行
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="commandText"></param>
        /// <param name="parameters"></param>
        /// <param name="excute"></param>
        /// <returns></returns>
        private TValue CreateDbCommondAndExcute<TValue>(string commandText,
            IDictionary<string, object> parameters, Func<SqlCommand, TValue> excute)
        {
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); };
            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = commandText; ;
                command.Connection = Connection;
                command.SetParameters(parameters);
                return excute(command);
            }
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Dispose()
        {
            if (connection != null)
            {
                Connection.Dispose();//非托管资源
            }
        }
    }
}
