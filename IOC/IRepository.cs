using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC
{
    /// <summary>
    /// DB Operate Interface
    /// </summary>
    public interface IRepository
    {
        void Get();
        string GetConn();
    }
    /// <summary>
    /// 对SQL数据源操作
    /// </summary>
    public class SqlRepository : IRepository
    {
        #region IRepository 成员
        public string GetConn()
        {
            return "Sql";
        }
        public void Get()
        {
            Console.WriteLine("sql数据源");
        }

        #endregion
    }
    /// <summary>
    /// 对redis数据源操作
    /// </summary>
    public class RedisRepository : IRepository
    {
        #region IRepository 成员
        public string GetConn()
        {
            return "Redis";
        }
        public void Get()
        {
            Console.WriteLine("Redis数据源");
        }

        #endregion
    }
    /// <summary>
    /// 数据源基类
    /// </summary>
    public class DBBase
    {
        public IRepository _iRepository;
        public DBBase(IRepository iRepository)
        {
            _iRepository = iRepository;
        }
        public void Search(string commandText)
        {
            _iRepository.Get();
        }
    }
}
