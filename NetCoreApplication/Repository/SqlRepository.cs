namespace NetCoreApplication.Repository
{
    // <summary>
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
}
