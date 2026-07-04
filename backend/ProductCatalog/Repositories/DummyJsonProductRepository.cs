using System.Net;
using ProductCatalog.Mapping;
using ProductCatalog.Models.Dtos;
using ProductCatalog.Models.External;

namespace ProductCatalog.Repositories;

public class DummyJsonProductRepository(HttpClient httpClient, IProductMapper mapper) : IProductRepository
{
    public async Task<ProductListResultDto> GetProductsAsync(int skip, int limit, string? category, CancellationToken ct)
    {
        var url = category is not null
            ? $"products/category/{category}?skip={skip}&limit={limit}"
            : $"products?skip={skip}&limit={limit}";

        var response = await httpClient.GetFromJsonAsync<DummyJsonProductsResponse>(url, ct)
            ?? throw new InvalidOperationException("DummyJsonProductsResponse returned an empty response");

        return new ProductListResultDto(mapper.ToListItems(response.Products), response.Total, response.Skip, response.Limit);
    }

    public async Task<ProductDetailDto?> GetProductByIdAsync(int id, CancellationToken ct)
    {

        var response = await httpClient.GetAsync($"products/{id}", ct);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var product = await response.Content.ReadFromJsonAsync<DummyJsonProduct>(cancellationToken: ct);
        return product is null ? null : mapper.ToDetail(product);
    }

    public async Task<ProductListResultDto> SearchAsync(string query, int skip, int limit, CancellationToken ct)
    {
        var url = $"products/search?q={Uri.EscapeDataString(query)}&skip={skip}&limit={limit}";

        var response = await httpClient.GetFromJsonAsync<DummyJsonProductsResponse>(url, ct)
            ?? throw new InvalidOperationException("DummyJsonProductsResponse returned an empty response");

        return new ProductListResultDto(mapper.ToListItems(response.Products), response.Total, response.Skip, response.Limit);
    }
}
