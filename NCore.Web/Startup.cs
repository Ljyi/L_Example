using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NCore.Web.DbContextCore;
using System.Globalization;
using System.Threading.Tasks;

namespace NCore.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //  var connection = "Data Source = .;Initial Catalog = NewCoreDb; User ID = sa;Password =123;";
            var connection = Configuration.GetConnectionString("ConnectionDatabase");
            services.AddDbContext<ModelContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<ModelContext>(options => { options.UseSqlServer(connection, sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()); });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 向 Startup.Configure 方法添加中间件组件的顺序定义了针对请求调用这些组件的顺序，以及响应的相反顺序。 
        /// 此排序对于安全性、性能和功能至关重要。以下 Startup.Configure 方法将为常见应用方案添加中间件组件：　　
        ///(1) 异常/错误处理
        ///(2) HTTP 严格传输安全协议
        ///(3) HTTPS 重定向
        ///(4) 静态文件服务器
        ///(5) Cookie 策略实施
        ///(6) 身份验证
        ///(7) 会话
        ///(8) MV
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // 当应用程序在开发环境中运行时：
                //    使用Developer Exception页面报告应用程序运行时错误。
                //    使用Database Error Page报告数据库运行时错误。
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            // 使用HTTPS重定向中间件将HTTP请求重定向到HTTPS。
            //  app.UseHttpsRedirection();
            // 返回静态文件并结束管道。
            app.UseStaticFiles();
            // 在用户访问安全资源之前进行身份验证。
            //  app.UseAuthentication();
            //调用自定义中间件
            app.UseRequestCulture();
            app.Run(async (context) =>
            {
                await ResponseAsync(context);
            });
            //添加路由
            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Blog", action = "GetBlogs" });
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task ResponseAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            await context.Response.WriteAsync(
                    //打印当前显示的语言
                    $"Hello { CultureInfo.CurrentCulture.DisplayName }"
                    );
        }
    }
}
