namespace ProductCatalog.Models.Dtos;

// wrapper for  list endpoint response
public record ProductListResultDto(
    List<ProductListItemDto> Items,
    int Total
    );