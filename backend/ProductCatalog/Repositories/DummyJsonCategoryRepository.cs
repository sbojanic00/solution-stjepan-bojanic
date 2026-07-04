using ProductCatalog.Mapping;
using ProductCatalog.Models.Dtos;
using ProductCatalog.Models.External;

namespace ProductCatalog.Repositories;

public class DummyJsonCategoryRepository(HttpClient httpClient, IProductMapper mapper) : ICategoryRepository
{
    public async Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct)
    {
        var categories = await httpClient.GetFromJsonAsync<List<DummyJsonCategory>>("products/categories", ct)
                         ?? [];

        return mapper.ToCategories(categories);
    }
}
