using System.IO.Abstractions;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Radzen;
using TableTennisRanker.Components;
using Serilog;
using Serilog.Events;
using TableTennisRanker;
using TableTennisRanker.Data;

Log.Logger = new LoggerConfiguration().CreateBootstrapLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme).AddNegotiate();
builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Host.UseSerilog((_, _, configuration) => configuration
    .MinimumLevel.Verbose()
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .WriteTo.File(@"logs\TTR_.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:HH:mm:ss.fff}; {Level:u3}; {ClassName:l}; {MemberName}; {Message:l}{NewLine}")
    .WriteTo.Debug(
        outputTemplate: "{Timestamp:HH:mm:ss.fff}; {Level:u3}; {ClassName:l}; {MemberName}; {Message:l}{NewLine}"));

builder.Services.AddRadzenComponents();
builder.Services.AddScoped<IFileSystem, FileSystem>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddSingleton(typeof(CompetitorManager));


var app = builder.Build();
app.Use(async (context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        var identity = context.User.Identity as ClaimsIdentity;
        var name = identity?.Name;
        var blackListUsers = builder.Configuration.GetSection("UserBlackList").Get<string[]>();

        if (blackListUsers != null && name != null && blackListUsers.Contains(name))
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("You are not authorized to access this app.");
            return;
        }
    }

    await next();
});


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
