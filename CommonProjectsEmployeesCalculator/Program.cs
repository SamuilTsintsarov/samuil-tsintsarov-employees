using BusinessLogicLayer.Services.Implementation;
using BusinessLogicLayer.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICalculateCommonProjectsService, CalculateCommonProjectsService>();
builder.Services.AddScoped<ICsvLoaderService, CsvLoaderService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
