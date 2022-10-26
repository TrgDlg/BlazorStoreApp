namespace StoreBlazor.Client.Models;

public class Product : IModel
{
    public int? ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? ProductType { get; set; }
    public string? SpecialtyType { get; set; }
    public string? Specialty { get; set; }
    public int? Price { get; set; }
    public int? Pages { get; set; }
}   