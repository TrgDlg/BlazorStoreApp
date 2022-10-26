using StoreBlazor.Client.Models;

namespace StoreBlazor.Client.HttpClients
{

    public class OrdersHttpClient
    {
        private readonly CommonHttpClient _http;

        public OrdersHttpClient(CommonHttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _http.Get<List<Order>>("api/orders/getallorders");
        }
    }
}
