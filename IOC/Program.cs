using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOC
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 通过构造函数进行依赖
            //不使用IOC，通过构造函数进行依赖
            SqlRepository sql = new SqlRepository();
            DBBase db = new DBBase(sql);
            db.Search("SELECT * FORM USER");
            Console.ReadKey();
            #endregion

            #region 通过Autofac进行依赖
            //实例
            RedisRepository Redis = new RedisRepository();
            var builder = new ContainerBuilder();
            builder.RegisterInstance(Redis).As<IRepository>();
            using (var container = builder.Build())
            {
                var manager = container.Resolve<IRepository>();
                manager.Get();
            }

            Console.ReadKey();

            #region 使用代码进行依赖
            // InstancePerDependency 对每一个依赖或每一次调用创建一个新的唯一的实例。这也是默认的创建实例的方式
            // 创建组件/服务注册的容器
            var builderIp = new ContainerBuilder();
            builderIp.RegisterType<DBBase>().InstancePerDependency();
            // 注册执行创建对象的表达式
            builderIp.RegisterType<SqlRepository>().As<IRepository>().InstancePerDependency();
            // 编译容器完成注册且准备对象解析
            using (var container = builderIp.Build())
            {
                var manager = container.Resolve<DBBase>();
                 manager.Search("SELECT * FORM USER");
            }
            Console.ReadKey();
            // InstancePerLifetimeScope 在一个生命周期域中，每一个依赖或调用创建一个单一的共享的实例，且每一个不同的生命周期域，实例是唯一的，不共享的
            var builder1 = new ContainerBuilder();
            builder1.RegisterType<DBBase>().InstancePerLifetimeScope();
            builder1.RegisterType<RedisRepository>().As<IRepository>().InstancePerLifetimeScope();
         //   builder1.RegisterType<SqlRepository>().As<IRepository>().InstancePerLifetimeScope();
            using (var container = builder1.Build())
            {
                var manager = container.Resolve<IRepository>();
                manager.Get();
            }
            Console.ReadKey();







            // InstancePerMatchingLifetimeScope 在一个做标识的生命周期域中，每一个依赖或调用创建一个单一的共享的实例。
            // 打了标识了的生命周期域中的子标识域中可以共享父级域中的实例。若在整个继承层次中没有找到打标识的生命周期域
            var builder2 = new ContainerBuilder();
            builder2.RegisterType<DBBase>().InstancePerMatchingLifetimeScope();
            builder2.RegisterType<SqlRepository>().As<IRepository>().InstancePerMatchingLifetimeScope();
            using (var container = builder2.Build())
            {
                var manager = container.Resolve<DBBase>();
                manager.Search("SELECT * FORM USER");
            }
            Console.ReadKey();
            // InstancePerMatchingLifetimeScope 在一个生命周期域中，每一个依赖或调用创建一个单一的共享的实例，
            // 且每一个不同的生命周期域，实例是唯一的，不共享的
            var builder3 = new ContainerBuilder();
            builder3.RegisterType<DBBase>().InstancePerMatchingLifetimeScope();
            builder3.RegisterType<SqlRepository>().As<IRepository>().InstancePerMatchingLifetimeScope();
            using (var container = builder3.Build())
            {
                var manager = container.Resolve<DBBase>();
                manager.Search("SELECT * FORM USER");
            }
            Console.ReadKey();
            // InstancePerOwned 在一个生命周期域中所拥有的实例创建的生命周期中，每一个依赖组件或调用Resolve()方法创建一个单一的共享的实例，
            // 并且子生命周期域共享父生命周期域中的实例。若在继承层级中没有发现合适的拥有子实例的生命周期域
            //builder.RegisterType<DBBase>().InstancePerOwned();
            //builder.RegisterType<SqlRepository>().As<IRepository>().InstancePerOwned();
            //using (var container = builder.Build())
            //{
            //    var manager = container.Resolve<DBBase>();
            //    manager.Search("SELECT * FORM USER");
            //}
            // SingleInstance 每一次依赖组件或调用Resolve()方法都会得到一个相同的共享的实例。其实就是单例模式
            var builder4 = new ContainerBuilder();
            builder4.RegisterType<DBBase>().SingleInstance();
            builder4.RegisterType<SqlRepository>().As<IRepository>().SingleInstance();
            using (var container = builder4.Build())
            {
                var manager = container.Resolve<DBBase>();
                manager.Search("SELECT * FORM USER");
            }
            Console.ReadKey();
            // InstancePerHttpRequest 在一次Http请求上下文中,共享一个组件实例。仅适用于asp.net mvc开发。
            //builder.RegisterType<DBBase>().InstancePerHttpRequest();
            //builder.RegisterType<SqlRepository>().As<IRepository>().InstancePerHttpRequest();
            //using (var container = builder.Build())
            //{
            //    var manager = container.Resolve<DBBase>();
            //    manager.Search("SELECT * FORM USER");
            //}
            #endregion

            #region 反射的方式
            var builderReflect = new ContainerBuilder();
            builderReflect.RegisterType<DBBase>();
            builderReflect.RegisterType<SqlRepository>().As<IRepository>();
            using (var container = builder.Build())
            {
                var manager = container.Resolve<DBBase>();
                manager.Search("SELECT * FORM USER");
            }
            #endregion
            #region 使用配置文件进行依赖

            #endregion
            #endregion
        }
    }

}
