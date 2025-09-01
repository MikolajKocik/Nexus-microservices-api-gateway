using Nexus.ApiGateway.Persistence;

namespace Nexus.ApiGateway.Features.Products;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Product> GetAll()
        => _dbContext.Products.ToList();    

    public Product? GetById(int id)
        => _dbContext.Products.FirstOrDefault(p => p.Id == id);
    
}
