using ProductCatalog.Mapping;
using ProductCatalog.Repositories;
using ProductCatalog.Services;

namespace ProductCatalog.Extensions;

public static class ServiceCollectionExtensions
{
    // DI wiring
    public static IServiceCollection AddProductCatalogServices(this IServiceCollection services, IConfiguration configuration)
    {
        var dummyJsonBaseUrl = configuration["DummyJson:BaseUrl"]
            ?? throw new InvalidOperationException("Missing configuration value 'DummyJson:BaseUrl'");

        services.AddSingleton<IProductMapper, ProductMapper>();

        services.AddHttpClient<IProductRepository, DummyJsonProductRepository>(client =>
        {
            client.BaseAddress = new Uri(dummyJsonBaseUrl);
        });

        services.AddHttpClient<ICategoryRepository, DummyJsonCategoryRepository>(client =>
        {
            client.BaseAddress = new Uri(dummyJsonBaseUrl);
        });

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        return services;
    }
}
