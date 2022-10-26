using StoreBlazor.Client.Aggregates;
using StoreBlazor.Client.Models;

namespace StoreBlazor.Server.Repositories
{
    public interface IBlazorShopRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductByName(string searchvalue);
        Task CreateProduct(Product product);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);

        Task<List<Order>> GetAllOrders();
        Task CreateOrder(Order order);
        Task DeleteOrder(Order order);
        Task UpdateOrder(Order order);

        Task CreateCustomer(Customer customer);
        Task DeleteCustomer(Customer customer);

    }
}
