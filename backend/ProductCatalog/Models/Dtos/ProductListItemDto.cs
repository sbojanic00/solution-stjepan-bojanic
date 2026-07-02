namespace ProductCatalog.Models.Dtos;

public record ProductListItemDto (
    int Id,
    string Thumbnail,
    string Title,
    decimal Price,
    string ShortDescription
    );