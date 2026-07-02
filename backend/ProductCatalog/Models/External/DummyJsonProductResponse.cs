namespace ProductCatalog.Models.External;

public class DummyJsonProductResponse
{
    public List<DummyJsonProduct> Products { get; set; } = [];
    public int Total { get; set; }
    public int Skip { get; set; }
    public int Limit { get; set; }
}