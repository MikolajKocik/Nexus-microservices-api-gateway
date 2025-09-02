using Microsoft.AspNetCore.Mvc;
using Nexus.ProductService.Features.Products;

namespace Nexus.ApiGateway.Features.Products;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    public IEnumerable<Product> GetAll()
    {
        IEnumerable<Product> products = _repository.GetAll();

        return products.Any() ? products : Enumerable.Empty<Product>();
    }

    public Product? GetById(int id)
    {
        Product product = _repository.GetById(id)!;  

        if (product is null)
        {
            return null;
        }

        return product;
    }
}
