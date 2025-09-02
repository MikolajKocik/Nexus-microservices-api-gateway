using Microsoft.AspNetCore.Mvc;
using Nexus.ProductService.Features.Common;

namespace Nexus.ProductService.Features.Products;

[ApiController]
[Route("api/v{version:apiVersion}/products")]
[ApiVersion("1.0")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;
    private readonly LinkGenerator _linkGenerator;

    public ProductController(
        IProductService productService,
        ILogger<ProductController> logger,
        LinkGenerator linkGenerator
        )
    {
        _productService = productService;
        _logger = logger;
        _linkGenerator = linkGenerator;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProductDto>> GetAll()
    {
        IEnumerable<Product> products = _productService.GetAll();

        IEnumerable<ProductDto> result = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Links = new List<LinkDto>
            {
                new()
                {
                    Rel = "self",
                    Href = _linkGenerator.GetPathByAction(
                        "GetById",
                        "Product",
                        new { id = p.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() }) ?? "1",
                        Method = "GET"
                },
                new()
                {
                    Rel = "delete",
                    Href = _linkGenerator.GetPathByAction(
                        "Delete",
                        "Product",
                        new { id = p.Id, version = HttpContext.GetRequestedApiVersion()?.ToString() }) ?? "1",
                        Method = "DELETE"
                }
            }
        });

        return Ok(result);
    }

    [HttpGet("{id}")]
    public ActionResult<ProductDto> GetById(int id)
    {
        Product product = _productService.GetById(id)!;

        if (product is null)
        {
            _logger.LogWarning("Product with id: {id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Product found successful");
        return Ok(product);
    }
}
