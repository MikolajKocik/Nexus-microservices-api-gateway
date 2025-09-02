using Nexus.ApiGateway.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Polly
builder.Services.AddHttpClient("NexusGateway")
    .AddPolicyHandler(PollyExtensions.GetRetryPolicy());

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.Run();