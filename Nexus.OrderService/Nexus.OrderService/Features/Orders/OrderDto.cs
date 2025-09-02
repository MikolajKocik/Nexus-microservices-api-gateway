namespace Nexus.OrderService.Features.Orders;

public sealed record OrderDto
{
    public int Id { get; init; }
    public DateTime CreatedAt { get; init; }
    public required string CustomerName { get; init; } = string.Empty;
    public required List<int> ProductIds { get; init; } = new();
}
