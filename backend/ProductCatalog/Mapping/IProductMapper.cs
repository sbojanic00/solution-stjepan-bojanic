using ProductCatalog.Models.Dtos;
using ProductCatalog.Models.External;

namespace ProductCatalog.Mapping;

public interface IProductMapper
{
    ProductListItemDto ToListItem(DummyJsonProduct product);

    List<ProductListItemDto> ToListItems(IEnumerable<DummyJsonProduct> products);

    ProductDetailDto ToDetail(DummyJsonProduct product);

    List<CategoryDto> ToCategories(IEnumerable<DummyJsonCategory> categories);
}
