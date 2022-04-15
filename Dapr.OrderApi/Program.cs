using Dapr.OrderApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.WebHost.UseUrls("http://*:5000");
// Add services to the container.


builder.Services.AddActors(options =>
{
    options.Actors.RegisterActor<OrderStatusActorService>();
});
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

/// <summary>
/// –Ú¡–ªØ
/// </summary>
app.UseEndpoints(endpoints =>
{
    endpoints.MapActorsHandlers();
});
app.MapControllers();
app.Run();
