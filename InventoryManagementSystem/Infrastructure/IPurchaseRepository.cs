using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Infrastructure
{
    public interface IPurchaseRepository
    {
        public Task<List<Purchase>> GetAllPurchase();

        public Task<string> AddProductToPurchase(string productName, int quantity,DateTime purchaseDate, string purchaseBy);
        public Task<Purchase> GetPurchaseProductById(int productId);

        //public bool UpdatePurchaseProduct(Purchase updatedProduct);

        public void DeletePurchase(int purchaseId);
        public Task<IEnumerable<Purchase>> GetAllPurchaseProducts();

        public Task<IEnumerable<Purchase>> GetAllPurchaseProductsAsync(DateTime startDate, DateTime endDate);


    }
}