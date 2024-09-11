using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using schliessanlagen_konfigurator.Data;
using schliessanlagen_konfigurator.Models.Users;
using schliessanlagen_konfigurator.Send_Data;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Web.Optimization;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using schliessanlagen_konfigurator;
using schliessanlagen_konfigurator.Service;
using System.Security.Policy;
using schliessanlagen_konfigurator.Controllers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<schliessanlagen_konfiguratorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("schliessanlagen_konfiguratorContext") ?? throw new InvalidOperationException("Connection string 'schliessanlagen_konfiguratorContext' not found.")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<schliessanlagen_konfiguratorContext>();

builder.Services.AddScoped<FooterService>();

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});
builder.Services.AddRazorPages(); 

BundleTable.EnableOptimizations = true;

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;
});

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
          {
            "image/svg+xml",
            "image/png",
            "image/webp",
            "application/atom+xml",
            "application/rss+xml" 
        });
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = System.IO.Compression.CompressionLevel.Fastest;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    SendData.Initialize(services);
}


app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=604800"); 
    }
});

app.UseDefaultFiles();

app.UseResponseCompression();

var supportedCultures = new[] { new CultureInfo("de-DE") };

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("de-DE"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(localizationOptions);

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
            pattern: "{controller=Konfigurator}/{action=IndexKonfigurator}");

    endpoints.MapControllerRoute(
           name: "Schop",
           pattern: "{controller=Schop}/{action=Index}");

    endpoints.MapControllerRoute(
        name: "Sitemap",
        pattern: "sitemap.xml",
        defaults: new { controller = "Sitemap", action = "Index" });

    endpoints.MapControllerRoute(
        name: "NotFound",
        pattern: "{*url}",
        defaults: new { controller = "Home", action = "Error" });


    endpoints.MapControllerRoute(
       name: "robots",
       pattern: "robots.txt",
       defaults: new { controller = "Sitemap", action = "Robots" });

    endpoints.MapRazorPages();

});

app.Run();