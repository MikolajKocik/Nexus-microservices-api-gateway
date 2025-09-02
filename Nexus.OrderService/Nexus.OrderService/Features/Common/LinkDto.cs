namespace Nexus.OrderService.Features.Common;

public sealed record LinkDto
{
    public required string Rel { get; init; } = string.Empty;
    public required string Href { get; init; } = string.Empty;
    public required string Method { get; init; } = string.Empty;
}
