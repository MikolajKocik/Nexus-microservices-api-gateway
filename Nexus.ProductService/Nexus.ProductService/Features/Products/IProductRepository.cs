namespace Nexus.ProductService.Features.Products;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
}
