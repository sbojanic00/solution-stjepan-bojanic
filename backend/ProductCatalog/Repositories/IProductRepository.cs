using ProductCatalog.Models.Dtos;

namespace ProductCatalog.Repositories;

public interface IProductRepository
{
    Task<ProductListResultDto> GetProductsAsync(
        int skip,
        int limit,
        string? category,
        CancellationToken ct);

    Task<ProductDetailDto?> GetProductByIdAsync(
        int id,
        CancellationToken ct);

    Task<ProductListResultDto> SearchAsync(
        string query,
        int skip,
        int limit,
        CancellationToken ct);
}
