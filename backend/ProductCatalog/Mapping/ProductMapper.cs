using ProductCatalog.Models.Dtos;
using ProductCatalog.Models.External;
using Riok.Mapperly.Abstractions;

namespace ProductCatalog.Mapping;

// DTOs are slimmer projections of the external models, so only every *target*
// member must be mapped; leftover source members (Sku, Weight, DummyJsonMeta,
// category Url, ReviewerEmail) are expected and ignored.
[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class ProductMapper : IProductMapper
{
    private const int ShortDescriptionLength = 100;

    [MapProperty(
        nameof(DummyJsonProduct.Description),
        nameof(ProductListItemDto.ShortDescription),
        Use = nameof(TruncateDescription))]
    public partial ProductListItemDto ToListItem(DummyJsonProduct product);

    public partial List<ProductListItemDto> ToListItems(IEnumerable<DummyJsonProduct> products);

    [MapProperty(
        nameof(DummyJsonProduct.DummyJsonDimensions),
        nameof(ProductDetailDto.Dimensions))]
    public partial ProductDetailDto ToDetail(DummyJsonProduct product);

    public partial List<CategoryDto> ToCategories(IEnumerable<DummyJsonCategory> categories);

    private static string TruncateDescription(string text) =>
        text.Length <= ShortDescriptionLength
            ? text
            : text[..ShortDescriptionLength].TrimEnd() + "...";
}
