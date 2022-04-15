namespace NetCoreApplication.Repository
{
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
