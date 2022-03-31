using Autofac.Extensions.DependencyInjection;

namespace NetCoreApplication
{
    public static class AutofacBuilder
    {
        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        }
    }
}
