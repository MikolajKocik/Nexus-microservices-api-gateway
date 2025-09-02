namespace Nexus.ProductService.Features.Products;

public interface IProductService
{
    IEnumerable<Product> GetAll();
    Product? GetById(int id);
}
