namespace Nexus.ApiGateway.Features.Products;

public sealed class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; } = default!;
    public decimal Price { get; private set; }  
}
