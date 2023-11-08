using Microsoft.EntityFrameworkCore;
using Purple.DAL;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("ConString");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));


var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area=exist}/{controller=dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
