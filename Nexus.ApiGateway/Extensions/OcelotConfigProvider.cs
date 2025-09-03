using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.Responses;

namespace Nexus.ApiGateway.Extensions;

public sealed class OcelotConfigProvider : IFileConfigurationRepository
{
    private readonly IConfiguration _configuration;
    // Stores the configuration in RAM after being read from the file for quick access, avoiding repeated disk I/O.
    private FileConfiguration? _cached; 

    public OcelotConfigProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<Response<FileConfiguration>> Get()
    {
        _cached ??= _configuration.Get<FileConfiguration>() ?? new FileConfiguration();

        _cached.Routes ??= new List<FileRoute>();

        foreach (var route in _cached.Routes)
        {
            // if RateLimit not set or disabled, set a default rule
            if (route.RateLimitOptions is null)
            {
                route.RateLimitOptions = new FileRateLimitRule
                {
                    EnableRateLimiting = true,
                    Period = "5s",
                    PeriodTimespan = 5,
                    Limit = 5
                };
            }
        }

        return Task.FromResult<Response<FileConfiguration>>(new OkResponse<FileConfiguration>(_cached));
    }

    public Task<Response> Set(FileConfiguration fileConfiguration)
    {
        _cached = fileConfiguration;
        return Task.FromResult<Response>(new OkResponse());
    }
}
