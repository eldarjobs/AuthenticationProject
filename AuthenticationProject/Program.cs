using AuthenticationProject.Data;
using AuthenticationProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    // aynı mail adresi birden fazla kullanılamaz, mail uniq olmak zorundadır
    options.User.RequireUniqueEmail = true;

    // Password Settings
    options.Password.RequireDigit = true;           // En az bir rakam
    options.Password.RequiredLength = 6;            // En az 6 karakter
    options.Password.RequireLowercase = true;       // En az bir küçük harf
    options.Password.RequireUppercase = true;       // En az bir büyük harf
    options.Password.RequireNonAlphanumeric = true; // En az bir özel karakter
    // options.Password.RequiredUniqueChars = 1;       // Tekrar eden karakter sayısı (örn: aa, bb, cc gibi) 1 olmalıdır

    // Lockout Settings

    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // 5 dakika sonra tekrar deneme hakkı
    options.Lockout.MaxFailedAccessAttempts = 2; // 5 defa yanlış giriş yapınca hesap kilitlenir
    options.Lockout.AllowedForNewUsers = true; // Yeni kullanıcılar içinde bu ayar geçerli olacak
})
    
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/admin/account/login";
        options.AccessDeniedPath = "/admin/account/accessdenied";
    });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=roles}/{action=Index}/{id?}");

app.Run();
