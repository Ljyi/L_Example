using ERP.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.DAL
{
    public class BaseDbContext : DbContext
    {
        //static BaseDbContext()
        //{
        //    try
        //    {
        //        // Database.SetInitializer<BaseDbContext>(null);
        //        // Database.SetInitializer(new CreateDatabaseIfNotExists<BaseDbContext>());//数据库不存在时，自动创建数据库
        //        //   Database.SetInitializer(new DropCreateDatabaseAlways<BaseDbContext>());//先删除原数据库，后创建新的数据库
        //        //Database.SetInitializer(new BaseDbContext<ERPContext>());//每次均先删除原数据库再创建新的数据库，不管数据模型是否发生改变
        //    }
        //    catch (Exception ex)
        //    {
        //        string msg = ex.Message;
        //        throw;
        //    }
        //}

        public BaseDbContext() : base("name=ERPContext")
        {
            // 禁用延迟加载
            //  this.Configuration.LazyLoadingEnabled = false;
            //删除数据库
            //  Database.Initialize(true);
            // Database.SetInitializer<BaseDbContext>(null);
            Database.SetInitializer(new CreateDatabaseIfNotExists<BaseDbContext>());//数据库不存在时，自动创建数据库
            //   Database.SetInitializer(new DropCreateDatabaseAlways<BaseDbContext>());//先删除原数据库，后创建新的数据库
            //Database.SetInitializer(new BaseDbContext<ERPContext>());//每次均先删除原数据库再创建新的数据库，不管数据模型是否发生改变
        }
        /// <summary>
        /// 初始化一个 使用指定数据连接名称或连接串 的数据访问上下文类 的新实例
        /// </summary>
        public BaseDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString) { }
        public DbSet<User> User { get; set; }
        public DbSet<SysMenu> SysMenu { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<OperationButton> OperationButton { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<UserRights> UserRights { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<SysMenu>();
            modelBuilder.Entity<Role>();
            modelBuilder.Entity<UserRole>();
            modelBuilder.Entity<OperationButton>();
            modelBuilder.Entity<UserRights>();
            //  Database.SetInitializer<BaseDbContext>(new ManagerInitializer());
            // 禁用默认表名复数形式
            //     modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            // 禁用一对多级联删除
            //     modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            // 禁用多对多级联删除
            //     modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }
    }
}
