namespace StoreBlazor.Client.Models;

public class OrderedProduct : IModel
{
    public int? OrderId { get; set; }
    public string? Product { get; set; }
    public int? Price { get; set; }

}   