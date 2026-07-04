using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models.Dtos;
using ProductCatalog.Services;

namespace ProductCatalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ProductListResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductListResultDto>> GetProducts(
        string? category,
        decimal? minPrice,
        decimal? maxPrice,
        int skip = 0,
        int limit = 20,
        CancellationToken ct = default)
    {
        if (skip < 0 || limit <= 0)
        {
            return BadRequest("skip must be >= 0 and limit must be > 0.");
        }

        var result = await productService.GetProductsAsync(skip, limit, category, minPrice, maxPrice, ct);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDetailDto>> GetProductById(int id, CancellationToken ct = default)
    {
        var product = await productService.GetProductByIdAsync(id, ct);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(ProductListResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductListResultDto>> Search(
        string q,
        int skip = 0,
        int limit = 20,
        CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(q))
        {
            return BadRequest("q is required.");
        }

        if (skip < 0 || limit <= 0)
        {
            return BadRequest("skip must be >= 0 and limit must be > 0.");
        }

        var result = await productService.SearchAsync(q, skip, limit, ct);
        return Ok(result);
    }
}
