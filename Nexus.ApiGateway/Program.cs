using Microsoft.AspNetCore.HttpLogging;
using Nexus.ApiGateway.Extensions;
using Ocelot.Cache.CacheManager;
using Ocelot.Configuration.Repository;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// ocelot + json
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, false)
    .AddJsonFile("ocelot.json", false, false)
    .AddOcelot(builder.Environment);

// register custom Ocelot file configuration repository 
builder.Services.AddSingleton<IFileConfigurationRepository, OcelotConfigProvider>();

builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(options =>
    {
        options.WithDictionaryHandle();
    });

// logging
builder.Logging.ClearProviders()
    .SetMinimumLevel(LogLevel.Warning);
if (!builder.Environment.IsProduction())
{
    builder.Logging.AddConsole();
}

// Basic HTTP logging for requests/responses 
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields =
        HttpLoggingFields.RequestMethod |
        HttpLoggingFields.RequestPath |
        HttpLoggingFields.ResponseStatusCode;
});

// Health checks endpoint for basic liveness
builder.Services.AddHealthChecks();

// Simple CORS policy for local dev
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseHttpLogging();

// health endpoint
app.MapHealthChecks("/health");

// wait for completed task - ocelot
await app.UseOcelot();

await app.RunAsync();