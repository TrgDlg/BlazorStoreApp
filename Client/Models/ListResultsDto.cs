namespace StoreBlazor.Client.Models
{
    public class ListResultsDto<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
    }
}
