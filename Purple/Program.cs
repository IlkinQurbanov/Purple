using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Purple.DAL;
using Purple.Helpers;
using Purple.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IFileService, FileService>();

var connectionString = builder.Configuration.GetConnectionString("ConString");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));


//User login and registrations

builder.Services.AddIdentity<User, IdentityRole>( option => {

    option.Password.RequiredLength = 10;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequiredUniqueChars = 1;
    option.Password.RequireDigit = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireUppercase = true;
    option.User.RequireUniqueEmail = true;
    option.Lockout.MaxFailedAccessAttempts = 3;

}).AddEntityFrameworkStores<AppDbContext>();


var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area}/{controller=dashboard}/{action=Index}/{id?}");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
