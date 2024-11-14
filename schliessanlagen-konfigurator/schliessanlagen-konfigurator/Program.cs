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
using schliessanlagen_konfigurator.ViewComponent;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddDbContext<schliessanlagen_konfiguratorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("schliessanlagen_konfiguratorContext") ?? throw new InvalidOperationException("Connection string 'schliessanlagen_konfiguratorContext' not found.")));

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<schliessanlagen_konfiguratorContext>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
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
builder.Services.AddControllers().AddJsonOptions(options =>
 {
     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
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

builder.Services.AddScoped<FooterServis>();

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

app.Use(async (context, next) =>
{
    if (!context.Request.Cookies.ContainsKey("CookiesAccepted"))
    {
        context.Items["ShowCookieConsent"] = true;
    }

    await next.Invoke();
});

app.UseStatusCodePagesWithRedirects("/Error/{0}");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error/500");
    app.UseHsts();
}


app.UseEndpoints(endpoints =>
{
    // Ваши основные маршруты
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
      name: "robots",
      pattern: "robots.txt",
      defaults: new { controller = "Sitemap", action = "Robots" });

    // Catch-all для 404 ошибок
    endpoints.MapControllerRoute(
        name: "NotFound",
        pattern: "{*url}",
        defaults: new { controller = "Error", action = "NotFound" });

    // Настройки для Razor Pages и MVC контроллеров
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

app.Run();