using Autofac;
using Microsoft.OpenApi.Models;
using NetCoreApplication;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
//.netcore 自带依赖注入
builder.Services.AddTransient<IOperationTransient, Operation>();
builder.Services.AddScoped<IOperationScoped, Operation>();
builder.Services.AddSingleton<IOperationSingleton, Operation>();
builder.Services.AddSingleton<IHelloService, HelloService>();
//Autofac注入
AutofacBuilder.ConfigureServices(builder);
AutofacBuilder.ServiceServices(builder);
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    AutofacBuilder.ConfigureContainer(container);
});

builder.Services.AddSwaggerGen();

//builder.Services.AddSwaggerGen(options =>
//{
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Version = "v1",
//        Title = "API标题",
//        Description = "API描述"
//    });
//    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var serviceProvider = app.Services;
var service = serviceProvider.GetRequiredService<IHelloService>();
service.SayHello();

// app.UseMyMiddleware();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSwagger();
//app.UseSwaggerUI();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
   // options.RoutePrefix = string.Empty;
});
app.UseRouting();

app.UseAuthorization();
app.MapControllers();

app.Run();
