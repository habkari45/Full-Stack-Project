using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Application.Services;
using WebApplication3.Application.Services.Base;
using WebApplication3.Infrastructure.Data;
using WebApplication3.Infrastructure.Repositories;
using WebApplication3.Infrastructure.Repositories.Base;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Accounts/Login";
        options.AccessDeniedPath = "/Accounts/Login";
    });

builder.Services.AddScoped<ISecurityIncidentRepository, SecurityIncidentRepository>();
builder.Services.AddScoped<ISecurityOfficerRepository, SecurityOfficerRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ISecurityIncidentService, SecurityIncidentService>();
builder.Services.AddScoped<ISecurityOfficerService, SecurityOfficerService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();