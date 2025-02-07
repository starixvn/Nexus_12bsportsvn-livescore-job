using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using SabaNews.Livescore.ScheduledTasks;
using SabaNews.Livescore.ScheduledTasks.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost
    .UseUrls($"http://*:5000")
    .UseNLog();

IConfiguration configuration = builder.Configuration;

var services = builder.Services;
services.AddControllers();

ServiceCollectionExtensions.AddConfiguration(services, configuration);
ServiceCollectionExtensions.AddRedisConfiguration(services, configuration);
ServiceCollectionExtensions.AddDependencyInjection(services, configuration);
ServiceCollectionExtensions.AddMqttClientHostedService(services, configuration);
ServiceCollectionExtensions.AddWorker(services);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseMiddleware<ErrorHandler>();
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();