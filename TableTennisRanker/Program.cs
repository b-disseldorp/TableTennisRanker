using System.IO.Abstractions;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Radzen;
using TableTennisRanker.Components;
using Serilog;
using Serilog.Events;
using TableTennisRanker;
using TableTennisRanker.Data;
using TableTennisRanker.Middleware;

Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();
builder.Services.AddScoped<IFileSystem, FileSystem>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddSingleton(typeof(CompetitorManager));

builder.Host.UseSerilog((_, services, configuration) => configuration
    .MinimumLevel.Verbose()
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(@"logs\TTR_.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:HH:mm:ss.fff}; {Level:u3}; {ClassName:l}; {MemberName}; {Message:l}{NewLine}")
    .WriteTo.Debug(
        outputTemplate: "{Timestamp:HH:mm:ss.fff}; {Level:u3}; {ClassName:l}; {MemberName}; {Message:l}{NewLine}"));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Html/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
