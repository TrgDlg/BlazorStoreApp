namespace StoreBlazor.Client.Aggregates
{
    public class OrdersAggregate
    {
        public int? ProductId { get; set; }
        public string? Name { get; set; }
        public string? ProductType { get; set; }
        public string? SpecialityType { get; set; }
        public string? Speciality { get; set; }
        public int? Price { get; set; }
        public int? Pages { get; set; }
    }
}