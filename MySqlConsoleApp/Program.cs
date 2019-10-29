using MySql.DAL;

namespace MySqlConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new DbMySqlContent();
            //IDatabaseInitializer<DbMySqlContent> dbInitializer = null;
            //if (dbContext.Database.Exists())
            //{
            //    //如果数据库已经存在
            //    dbInitializer = new DropCreateDatabaseIfModelChanges<DbMySqlContent>();
            //}
            //else
            //{
            //    //总是先删除然后再创建
            //    dbInitializer = new DropCreateDatabaseAlways<DbMySqlContent>();
            //}
            ////Database.SetInitializer(dbInitializer);
            //dbInitializer.InitializeDatabase(dbContext);

            var entity = dbContext.MyEntity.Find(1);

            //dbContext.MyEntity.Add(new MyEntity() { Id = 2, Name = "南孚电池" });
            //dbContext.MyEntity.Add(new MyEntity() { Id = 2, Name = "测试" });
            dbContext.SaveChanges();
        }
    }
}
