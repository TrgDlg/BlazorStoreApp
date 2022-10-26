namespace StoreBlazor.Client.Models;

public class Customer : IModel
{
    public int? CustomerId { get; set; }
    public string? Name { get; set; }
    public string? Rights { get; set; }
    public int? Age { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}   