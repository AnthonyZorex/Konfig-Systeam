using Microsoft.EntityFrameworkCore;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Send_Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<schliessanlagen_konfiguratorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("schliessanlagen_konfiguratorContext") ?? throw new InvalidOperationException("Connection string 'schliessanlagen_konfiguratorContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".AdventureWorks.Session";
    options.IdleTimeout = TimeSpan.FromSeconds(10);
    options.Cookie.IsEssential = true;
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SendData.Initialize(services);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Konfigurator}/{action=IndexKonfigurator}/{id?}");
app.UseSession();
app.Run();
