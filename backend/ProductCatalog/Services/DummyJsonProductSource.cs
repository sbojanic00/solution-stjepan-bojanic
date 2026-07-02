using ProductCatalog.Models.Dtos;
using ProductCatalog.Models.External;

namespace ProductCatalog.Services;

public class DummyJsonProductSource : IProductSource
{
    private readonly HttpClient _httpClient;

    public DummyJsonProductSource(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<ProductListResultDto> GetProductsAsync(
        int skip,
        int limit,
        string? category,
        decimal? minPrice,
        decimal? maxPrice,
        CancellationToken ct)
    {
        var url = category is not null
            ? $"products/category/{category}?skip={skip}&limit={limit}"
            : $"products?skip={skip}&limit={limit}";

        var response = await _httpClient.GetFromJsonAsync<DummyJsonProductsResponse>(url, ct)
            ?? throw new InvalidOperationException("DummyJsonProductsResponse returned an empty response");

        var filtered = response.Products
            .Where(p => (!minPrice.HasValue || p.Price >= minPrice.Value) &&
                        (!maxPrice.HasValue || p.Price <= maxPrice.Value))
            .Select(MapToListItem)
            .ToList();

        return new ProductListResultDto(filtered, response.Total);
    }

    public async Task<ProductDetailDto?> GetProductByIdAsync(int id, CancellationToken ct)
    {
        var product = await _httpClient.GetFromJsonAsync<ProductDetailDto>($"products/{id}", ct);
        return product is null ? null : MapToDetail(product);
    }

    public async Task<ProductListResultDto> SearchAsync(
        string query,
        int skip,
        int limit,
        CancellationToken ct)
    {
        var url = $"products?search?q={Uri.EscapeDataString(query)}&skip={skip}&limit={limit}";

        var response = await _httpClient.GetFromJsonAsync<DummyJsonProductsResponse>(url, ct)
            ?? throw new InvalidOperationException("DummyJsonProductsResponse returned an empty response");

        var items = response.Products.Select(MapToListItem).ToList();
        return new ProductListResultDto(items, response.Total);
    }

    public async Task<List<CategoryDto>> GetCategoriesAsync(CancellationToken ct)
    {
        var categories = await _httpClient.GetFromJsonAsync<List<DummyJsonCategory>>("products/categories", ct)
                         ?? [];

        return categories.Select(c=> new CategoryDto(c.Slug, c.Name)).ToList();
    }

    private static ProductListItemDto MapToListItem(DummyJsonProduct product) =>
        new (product.Id, product.Thumbnail, product.Title, product.Price, Truncate(product.Description, 100));

    private static ProductDetailDto MapToDetail(DummyJsonProduct product) =>
        new(product.Id,
            product.Title,
            product.Description,
            product.Category,
            product.Price,
            product.DiscountPercentage,
            product.Rating,
            product.Stock,
            product.Tags,
            product.Brand,
            new ProductDimensionsDto(
                product.DummyJsonDimensions.Width,
                product.DummyJsonDimensions.Height,
                product.DummyJsonDimensions.Depth),
            product.WarrantyInformation,
            product.ShippingInformation,
            product.AvailabilityStatus,
            product.Reviews.Select(r => new ProductReviewDto(r.Rating, r.Comment, r.Date, r.ReviewerName)).ToList(),
            product.ReturnPolicy,
            product.MinimumOrderQuantity,
            product.Images,
            product.Thumbnail
        );

    private static string Truncate(string text, int maxLength) =>
        text.Length <= maxLength
            ? text
            : text[..maxLength].TrimEnd() + "...";
}