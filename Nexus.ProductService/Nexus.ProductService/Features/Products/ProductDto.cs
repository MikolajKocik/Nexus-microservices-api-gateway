using Nexus.ProductService.Features.Common;

namespace Nexus.ProductService.Features.Products;

public sealed record ProductDto
{
    public int Id { get; init; }
    public required string Name { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public List<LinkDto> Links { get; init; } = new();
}
