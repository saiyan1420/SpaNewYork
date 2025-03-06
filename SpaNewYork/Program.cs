using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using SpaNewYork.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Configure database connection
builder.Services.AddDbContext<SpaNewYorkDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connections")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure authentication with cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "SpaNewYork.Cookie";
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Luôn gửi cookie qua HTTPS
        options.Cookie.SameSite = SameSiteMode.None; // Cho phép gửi cookie qua các nguồn khác nhau nếu cần
        options.Cookie.HttpOnly = true; // Bảo vệ cookie khỏi JavaScript
        options.ExpireTimeSpan = TimeSpan.FromMinutes(1000);
        options.SlidingExpiration = true;
        options.LoginPath = "/Home/Login";
        options.LogoutPath = "/Home/Logout";
        options.AccessDeniedPath = "/Home/Forbidden";
    });

// Enable Session
builder.Services.AddSession(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.HttpOnly = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout session
});

var app = builder.Build();

// Enforce HTTPS and security headers
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Redirect HTTP to HTTPS
app.UseHttpsRedirection();
app.UseStaticFiles();

// Forward headers when behind reverse proxies (Azure, Nginx, etc.)
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto
});

// Enable middleware
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

// Route configuration
app.MapControllerRoute(name: "adminareas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
