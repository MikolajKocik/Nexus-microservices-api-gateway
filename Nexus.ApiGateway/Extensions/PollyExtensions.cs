using Polly;
using Polly.Extensions.Http;

namespace Nexus.ApiGateway.Extensions;

public static class PollyExtensions
{
    public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            // avoid retry storm, using math.pow retry attempt as exponential backoff
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}
