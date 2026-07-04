using ProductCatalog.Models.Dtos;

namespace ProductCatalog.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct);
}
