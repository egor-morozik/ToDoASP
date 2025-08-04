var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapAreaControllerRoute(
    name: "task_area",
    areaName: "Task",
    pattern: "{area}/{controller=Tasks}/{action=Index}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();