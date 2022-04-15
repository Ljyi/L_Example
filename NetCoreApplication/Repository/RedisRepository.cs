namespace NetCoreApplication.Repository
{
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
}
