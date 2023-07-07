using Microsoft.AspNetCore.Mvc;
using InventoryManagementSystem.Models;
using InventoryManagementSystem;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Data.Common;
using InventoryManagementSystem.Infrastructure;

namespace InventoryManagementSystem.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContext _context;

        public ProductRepository(DapperContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateProduct(Product product)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ProductName", product.ProductName);
                parameters.Add("@Quantity", product.Quantity);
                parameters.Add("@Price", product.Price);
               
                parameters.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("AddProductProcedure", parameters, commandType: CommandType.StoredProcedure);

                var success = parameters.Get<bool>("@Success");

                return success;
            }
        }
        //To get product
        public async Task<List<Product>> GetAllProduct()
        {
            using (var connection = _context.CreateConnection())
            {
                var products = await connection.QueryAsync<Product>("GetAllProduct", commandType: CommandType.StoredProcedure);
                return products.ToList();
            }
        }

        //Get product by id to edit and for productDetail
        public async Task<Product> GetProductById(int id)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ProductId", id);

                return await connection.QueryFirstOrDefaultAsync<Product>("GetProductById", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        //for update the product
        public async Task<bool> EditProduct(Product updatedProduct)
        {
            try
            {
                using (var connection = _context.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ProductId", updatedProduct.ProductId);
                    parameters.Add("@ProductName", updatedProduct.ProductName);
                    parameters.Add("@Quantity", updatedProduct.Quantity);
                    parameters.Add("@Price", updatedProduct.Price);

                    await connection.ExecuteAsync("UpdateProductProcedure", parameters, commandType: CommandType.StoredProcedure);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //to delete
        public async Task  DeleteProduct(int productId)
        {
            using (var connection = _context.CreateConnection())
            {
               await connection.ExecuteAsync("DeleteProduct", new { ProductId = productId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<List<string>> GetAvailableProductNames()
        {
            using (var connection = _context.CreateConnection())
            {
                var productList = await connection.QueryAsync<string>("GetAvailableProducts", commandType: CommandType.StoredProcedure);
                return productList.ToList();
            }
        }

        //Get Quantity
        public async Task<int> GetProductQuantityByNameAsync(string productName)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ProductName", productName);

                int quantity = await connection.QuerySingleOrDefaultAsync<int>("GetProductQuantityByName", parameters, commandType: CommandType.StoredProcedure);

                return quantity;
            }
        }

        //To show the report of product
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Product>("ProductReport", commandType: CommandType.StoredProcedure);
            }
        }
    }
}
