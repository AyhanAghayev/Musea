using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EBIN.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<EBINContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EBINContext") ?? throw new InvalidOperationException("Connection string 'EBINContext' not found.")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.Name = "EBIN.Auth";
    options.LoginPath = "/Auth/Login";
    options.AccessDeniedPath = "/Auth/Login";
});
// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "ProfilesRoute",
    pattern: "Profiles/{userName}",
    defaults: new {controller = "Profiles", action = "Index"});

app.MapControllerRoute(
    name: "MainRoute",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
