using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Models.Dtos;
using ProductCatalog.Services;

namespace ProductCatalog.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryDto>>> GetCategories(CancellationToken ct)
    {
        var result = await categoryService.GetCategoriesAsync(ct);
        return Ok(result);
    }
}
