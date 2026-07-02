namespace ProductCatalog.Models.Dtos;

public record ProductReviewDto(
    int Rating,
    string Comment,
    DateTime Date,
    string ReviewerName
    );