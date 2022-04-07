using Autofac;
using AutofacApplication.Service;

namespace AutofacApplication
{
    public class AutofacExtensions
    {
        /// <summary>
        /// Autofac注册服务的地方,Autofac会自动调用
        /// </summary>
        /// <param name="containerBuilder"></param>
        public static void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            // 瞬时注入：containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerDependency(); ;

            // 单例注入：containerBuilder.RegisterType<UserService>().As<IUserService>().SingleInstance();

            // 生命周期注入： containerBuilder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            //注册服务
            containerBuilder.RegisterType<UserService>().As<IUserService>();
        }
    }
}
