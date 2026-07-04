using ProductCatalog.Models.Dtos;

namespace ProductCatalog.Repositories;

public interface ICategoryRepository
{
    Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct);
}
