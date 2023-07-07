using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Infrastructure
{
    public interface IProductRepository
    {
        public Task<bool> CreateProduct(Product product);
        public Task<List<Product>> GetAllProduct();
        public Task<Product> GetProductById(int id);
        public Task<bool> EditProduct(Product updatedProduct);
        public Task DeleteProduct(int productId);

        public Task<List<string>> GetAvailableProductNames();

        public Task<int> GetProductQuantityByNameAsync(string productName);
        public Task<IEnumerable<Product>> GetAllProductsAsync();

    }
}
