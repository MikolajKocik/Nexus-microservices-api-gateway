using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Nexus.ApiGateway.Extensions;
using Nexus.ApiGateway.Features.Orders;
using Nexus.ApiGateway.Features.Products;
using Nexus.ApiGateway.Persistence;
using Nexus.ApiGateway.Validators;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// load local settings
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

// Hangfire
builder.Services.AddHangfire(config =>
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHangfireServer(options =>
    options.SchedulePollingInterval = TimeSpan.FromSeconds(1));

// API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // np. v1
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddTransient<ProductValidator>();

// Polly
builder.Services.AddHttpClient("ProductService")
    .AddPolicyHandler(PollyExtensions.GetRetryPolicy());

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddOpenApi();

// EF Core + SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

WebApplication app = builder.Build();

app.UseHangfireDashboard("/hangfire");

app.UseApiVersioning();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseRouting(); 
app.UseHttpsRedirection();
app.MapControllers();

await app.RunAsync();