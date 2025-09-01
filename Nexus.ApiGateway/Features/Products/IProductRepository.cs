namespace Nexus.ApiGateway.Features.Products;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
}
