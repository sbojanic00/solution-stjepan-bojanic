using ProductCatalog.Models.Dtos;
using ProductCatalog.Repositories;

namespace ProductCatalog.Services;

public class CategoryService(ICategoryRepository repository) : ICategoryService
{
    public async Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct)
    {
        var categories = await repository.GetCategoriesAsync(ct);
        return categories.OrderBy(c => c.Name).ToList();
    }
}
