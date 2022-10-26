using Dapper;
using System.Data;
using Microsoft.Extensions.Options;
using StoreBlazor.Client.Models;
using StoreBlazor.Client.Aggregates;
using System.ComponentModel.Design;

namespace StoreBlazor.Server.Repositories
{
    public class BlazorShopRepository : DapperRepository, IBlazorShopRepository
    {
        public BlazorShopRepository(IOptions<ConnectionSettings> connectionSettings) : base(connectionSettings)
        {
        }
        
        public async Task CreateProduct(Product product)
        {
            using var connection = GetConnection();

            var parameters = new DynamicParameters();
                parameters.Add("@sCRUD_Type", "C", DbType.String, ParameterDirection.Input);           
                parameters.Add("@sProductName",  product.ProductName, DbType.String, ParameterDirection.Input);
                parameters.Add("@sProductType",  product.ProductType, DbType.String, ParameterDirection.Input);
                parameters.Add("@sSpecialtyType",  product.SpecialtyType, DbType.String, ParameterDirection.Input);
                parameters.Add("@sSpecialty",  product.Specialty, DbType.String, ParameterDirection.Input);
                parameters.Add("@nPrice",  product.Price, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@nAmount",  product.Pages, DbType.Int32, ParameterDirection.Input);

            var result = await connection.QueryAsync<Product>("BS_Products_CRUD", parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<List<Product>> GetAllProducts()
        {
            using var connection = GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@sCRUD_Type", "R", DbType.String, ParameterDirection.Input);

            var result = await connection.QueryAsync<Product>("BS_Products_CRUD", parameters,
                commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
        
        public async Task<Product> GetProductByName(string searchvalue)
        {
            using var connection = GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@sCRUD_Type", "R", DbType.String, ParameterDirection.Input);
            parameters.Add("@nPrice", searchvalue, DbType.Int32, ParameterDirection.Input);

            return await connection.QueryFirstOrDefaultAsync<Product>("BS_Products_CRUD", parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateProduct(Product product)
        {
            using var connection = GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@sCRUD_Type", "U", DbType.String, ParameterDirection.Input);

            var result = await connection.QueryAsync<Product>("BS_Products_CRUD", parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteProduct(Product product)
        {
            using var connection = GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@sCRUD_Type", "D", DbType.String, ParameterDirection.Input);           

            var result = await connection.QueryAsync<Product>("BS_Products_CRUD", parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateOrder(Order order)
        {
            using var connection = GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@sCRUD_Type", "C", DbType.String, ParameterDirection.Input);

            var result = await connection.QueryAsync<Product>("BS_Orders_CRUD", parameters,
                commandType: CommandType.StoredProcedure);
        }


        public async Task<List<Order>> GetAllOrders()
        {
            using var connection = GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@sCRUD_Type", "R", DbType.String, ParameterDirection.Input);           

            var result = await connection.QueryAsync<Order>("BS_Orders_CRUD", parameters,
                commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async Task UpdateOrder(Order order)
        {
            using var connection = GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@sCRUD_Type", "U", DbType.String, ParameterDirection.Input);

            var result = await connection.QueryAsync<Product>("BS_Orders_CRUD", parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteOrder(Order order)
        {
            using var connection = GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@sCRUD_Type", "D", DbType.String, ParameterDirection.Input);

            var result = await connection.QueryAsync<Product>("BS_Orders_CRUD", parameters,
                commandType: CommandType.StoredProcedure);
        }


        public async Task CreateCustomer(Customer customer)
        {
            using var connection = GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@sCRUD_Type", "C", DbType.String, ParameterDirection.Input);

            var result = await connection.QueryAsync<Product>("BS_Customers_CRUD", parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteCustomer(Customer customer)
        {
            using var connection = GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@sCRUD_Type", "D", DbType.String, ParameterDirection.Input);

            var result = await connection.QueryAsync<Product>("BS_Customers_CRUD", parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateOrderedProduct(Customer customer)
        {
            using var connection = GetConnection();

            var parameters = new DynamicParameters();
            parameters.Add("@sCRUD_Type", "C", DbType.String, ParameterDirection.Input);

            var result = await connection.QueryAsync<Product>("BS_Ordered_Products_CRUD", parameters,
                commandType: CommandType.StoredProcedure);
        }

    }
}