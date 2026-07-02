using ProductCatalog.Models.Dtos;

namespace ProductCatalog.Services;

public interface IProductSource
{
    Task<ProductListResultDto> GetProductsAsync(
        int skip,
        int limit,
        string? category,
        decimal? minPrice,
        decimal? maxPrice,
        CancellationToken ct);

    Task<ProductDetailDto?> GetProductByIdAsync(
        int id,
        CancellationToken ct);

    Task<ProductListResultDto> SearchAsync(
        string query,
        int skip,
        int limit,
        CancellationToken ct);

    Task<List<CategoryDto>> GetCategoriesAsync(
        CancellationToken ct);
}