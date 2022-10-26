using StoreBlazor.Client.Models;

namespace StoreBlazor.Client.HttpClients
{

    public class ProductsHttpClient
    {
        private readonly CommonHttpClient _http;

        public ProductsHttpClient(CommonHttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _http.Get<List<Product>>("api/homepage/getallproducts");
        }

        public async Task<Product> GetProductByName(string searchValue)
        {
            var product = await _http.Get<Product>($"/api/card/{searchValue}");

            return product;
        }

        public async Task CreateCard(Product product)
        {
            await _http.Post<Product>($"/api/createcard/", product);
        }

    }
}
