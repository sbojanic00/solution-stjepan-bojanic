using ProductCatalog.Models.Dtos;
using ProductCatalog.Repositories;

namespace ProductCatalog.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    public async Task<ProductListResultDto> GetProductsAsync(
        int skip,
        int limit,
        string? category,
        decimal? minPrice,
        decimal? maxPrice,
        CancellationToken ct)
    {
        var result = await repository.GetProductsAsync(skip, limit, category, ct);

        var filteredItems = result.Items
            .Where(p => (!minPrice.HasValue || p.Price >= minPrice.Value) &&
                        (!maxPrice.HasValue || p.Price <= maxPrice.Value))
            .ToList();

        return result with { Items = filteredItems };
    }

    public Task<ProductDetailDto?> GetProductByIdAsync(int id, CancellationToken ct) =>
        repository.GetProductByIdAsync(id, ct);

    public Task<ProductListResultDto> SearchAsync(string query, int skip, int limit, CancellationToken ct) =>
        repository.SearchAsync(query, skip, limit, ct);
}
