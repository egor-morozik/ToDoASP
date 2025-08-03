var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapAreaControllerRoute(
    name: "default",
    areaName: "Task",
    pattern: "{area:exists}/{controller=Tasks}/{action=Index}/");

app.Run();