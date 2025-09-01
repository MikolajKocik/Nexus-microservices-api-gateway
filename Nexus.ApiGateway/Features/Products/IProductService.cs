namespace Nexus.ApiGateway.Features.Products;

public interface IProductService
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
}
