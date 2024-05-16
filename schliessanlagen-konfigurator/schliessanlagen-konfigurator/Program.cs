using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models.Users;
using schliessanlagen_konfigurator.Send_Data;
using System.Threading;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<schliessanlagen_konfiguratorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("schliessanlagen_konfiguratorContext") ?? throw new InvalidOperationException("Connection string 'schliessanlagen_konfiguratorContext' not found.")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<schliessanlagen_konfiguratorContext>();


builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});
builder.Services.AddRazorPages();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.ExcludedMimeTypes = new[] { "text/plain" };
 
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SendData.Initialize(services);
}


app.UseDefaultFiles();
app.UseStaticFiles();

//if (app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseResponseCompression();

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.MapRazorPages();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Konfigurator}/{action=IndexKonfigurator}/{id?}");
    endpoints.MapRazorPages();
});
app.Run();