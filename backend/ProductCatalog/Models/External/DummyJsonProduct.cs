namespace ProductCatalog.Models.External;

// pasted special as json as classes
public class DummyJsonProduct
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal Price { get; set; }
    public double DiscountPercentage { get; set; }
    public double Rating { get; set; }
    public int Stock { get; set; }
    public List<string> Tags { get; set; } = [];
    public string Brand { get; set; } = "";
    public string Sku { get; set; } = "";
    public int Weight { get; set; }
    public DummyJsonDimensions DummyJsonDimensions { get; set; } = new DummyJsonDimensions();
    public string WarrantyInformation { get; set; } = "";
    public string ShippingInformation { get; set; } = "";
    public string AvailabilityStatus { get; set; } = "";
    public List<DummyJsonReview> Reviews { get; set; } = [];
    public string ReturnPolicy { get; set; } = "";
    public int MinimumOrderQuantity { get; set; }
    public DummyJsonMeta DummyJsonMeta { get; set; } = new DummyJsonMeta();
    public List<string> Images { get; set; } = [];
    public string Thumbnail { get; set; } = "";
}