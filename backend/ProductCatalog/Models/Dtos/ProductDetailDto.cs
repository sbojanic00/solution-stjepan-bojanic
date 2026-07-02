namespace ProductCatalog.Models.Dtos;

public record ProductDetailDto(
    int Id,
    string Title,
    string Description,
    string Category,
    decimal Price,
    double DiscountPercentage,
    double Rating,
    int Stock,
    List<string> Tags,
    string Brand,
    ProductDimensionsDto Dimensions,
    string WarrantyInformation,
    string ShippingInformation,
    string AvailabilityStatus,
    List<ProductReviewDto> Reviews,
    string ReturnPolicy,
    int MinimumOrderQuantity,
    List<string> Images,
    string Thumbnail
    );