using MySql.Data.MySqlClient;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace MySql.DAL
{
    [DbConfigurationType(typeof(MySqlConfiguration))]
    public class DbMySqlContent : DbContext
    {
        //您的上下文已配置为从您的应用程序的配置文件(App.config 或 Web.config)
        //使用“DbContent”连接字符串。默认情况下，此连接字符串针对您的 LocalDb 实例上的
        //“MySqlConsoleApp.DbContent”数据库。
        // 
        //如果您想要针对其他数据库和/或数据库提供程序，请在应用程序配置文件中修改“DbContent”
        //连接字符串。
        public DbMySqlContent() : base("name=MyContext")
        {
            Database.SetInitializer<DbMySqlContent>(new CreateDatabaseIfNotExists<DbMySqlContent>());
        }
        //为您要在模型中包含的每种实体类型都添加 DbSet。有关配置和使用 Code First  模型
        //的详细信息，请参阅 http://go.microsoft.com/fwlink/?LinkId=390109。

        public virtual DbSet<MyEntity> MyEntity { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyEntity>();
        }
    }

    public class MyEntity
    {
        [Description("主键")]
        [Key]
        public int Id { get; set; }
        [Description("名称")]
        public string Name { get; set; }
    }
}