namespace Nexus.ApiGateway.Jobs;

public sealed class SendReportJob
{
    public Task ExecuteAsync()
    {
        Console.WriteLine($"Report sent at {DateTime.Now}");
        return Task.CompletedTask;  
    }
}
