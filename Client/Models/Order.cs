namespace StoreBlazor.Client.Models;

public class Order : IModel
{
    public int? OrderId { get; set; }
    public string? CreateDate { get; set; }
    public string? ChangeDate { get; set; }
    public string? Status { get; set; }
    public string? Amount { get; set; }

}   