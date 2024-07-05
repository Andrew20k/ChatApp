using ChatApp.Hub;
using Microsoft.EntityFrameworkCore;
using ChatApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mvc = builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(12);
    options.Cookie.Name = "ChatApp.Session";
    options.Cookie.IsEssential = true;
});

builder.Services.AddSignalR(hubOptions =>
{
    hubOptions.EnableDetailedErrors = true;
});

// Register the ChatContext with Entity Framework Core
builder.Services.AddDbContext<ChatContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register the DatabaseManager
builder.Services.AddScoped<IDatabaseManager, DatabaseManager>();

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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/chat");

app.Run();