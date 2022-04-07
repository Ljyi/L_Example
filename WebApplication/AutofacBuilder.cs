using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutofacApplication.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetCoreApplication.AutofacAttribute;
using NetCoreApplication.Denpendency;
using System.Reflection;

namespace NetCoreApplication
{
    public static class AutofacBuilder
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        }
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
            AutofacAttribute(containerBuilder);
        }
        /// <summary>
        /// 属性注入
        /// </summary>
        /// <param name="builder"></param>
        public static void ServiceServices(WebApplicationBuilder builder)
        {
            builder.Services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
        }
        /// <summary>
        /// 
        /// </summary>
        public static void AutofacAttribute(ContainerBuilder containerBuilder)
        {
            //获取所有控制器类型并使用属性注入
            Type[] controllersTypeAssembly = typeof(Program).Assembly.GetExportedTypes()
                .Where(type => typeof(ControllerBase).IsAssignableFrom(type)).ToArray();
            containerBuilder.RegisterTypes(controllersTypeAssembly).PropertiesAutowired(new AutowiredPropertySelector());

            //批量自动注入,把需要注入层的程序集传参数
            //containerBuilder.BatchAutowired(typeof(UserService).Assembly);
            //containerBuilder.BatchAutowired(typeof(UserRepository).Assembly);
        }
        /// <summary>
        /// 批量注入扩展
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assembly"></param>
        public static void BatchAutowired(this ContainerBuilder builder, Assembly assembly)
        {
            var transientType = typeof(ITransitDenpendency); //瞬时注入
            var singletonType = typeof(ISingletonDenpendency); //单例注入
            var scopeType = typeof(IScopeDenpendency); //单例注入
            //瞬时注入
            builder.RegisterAssemblyTypes(assembly).Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(transientType))
                .AsSelf()
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .PropertiesAutowired(new AutowiredPropertySelector());
            //单例注入
            builder.RegisterAssemblyTypes(assembly).Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(singletonType))
               .AsSelf()
               .AsImplementedInterfaces()
               .SingleInstance()
               .PropertiesAutowired(new AutowiredPropertySelector());
            //生命周期注入
            builder.RegisterAssemblyTypes(assembly).Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(scopeType))
               .AsSelf()
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope()
               .PropertiesAutowired(new AutowiredPropertySelector());
        }
    }
}
