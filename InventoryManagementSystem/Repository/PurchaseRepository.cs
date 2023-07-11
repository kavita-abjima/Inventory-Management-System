using Dapper;
using InventoryManagementSystem.Infrastructure;
using InventoryManagementSystem.Models;
using System.Data;

namespace InventoryManagementSystem.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly DapperContext _context;


        public PurchaseRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<Purchase>> GetAllPurchase()
        {
            using (var connection = _context.CreateConnection())
            {
                var purchases = await connection.QueryAsync<Purchase>("GetAllPurchase", commandType: CommandType.StoredProcedure);
                return purchases.ToList();
            }
        }

        public async Task<string> AddProductToPurchase(string productName, int quantity, DateTime purchaseDate, string purchaseBy)
        {

            try
            {
                using (var connection = _context.CreateConnection())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Purchase_product", productName);
                    parameters.Add("@Purchase_quantity", quantity);
                    //parameters.Add("@Purchase_price", price);
                    parameters.Add("@PurchaseDate", purchaseDate);
                    parameters.Add("@PurchaseBy", purchaseBy);

                    var result = await connection.QueryFirstOrDefaultAsync<string>("[dbo].[AddProductToPurchase]", param: parameters, commandType: CommandType.StoredProcedure);
                    return result;
                }
            }
            catch (Exception ex)
            {
                
                Console.WriteLine( ex.Message);
                throw;
            }

        }

        //For EditPurchased Product
        public async Task<Purchase> GetPurchaseProductById(int purchaseId)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@PurchaseId", purchaseId);

                return await connection.QueryFirstOrDefaultAsync<Purchase>("GetPurchaseProductById", parameters, commandType: CommandType.StoredProcedure);
            }
        }

        //public bool UpdatePurchaseProduct(Purchase updatedProduct)
        //{
        //    try
        //    {
        //        using (var connection = _context.CreateConnection())
        //        {
        //            DynamicParameters parameters = new DynamicParameters();
        //            parameters.Add("@PurchaseId", updatedProduct.PurchaseId);
        //            parameters.Add("@Purchase_product", updatedProduct.Purchase_product);
        //            parameters.Add("@Purchase_quantity", updatedProduct.Purchase_quantity);
        //            parameters.Add("@Purchase_price", updatedProduct.Purchase_price);
        //            parameters.Add("@PurchaseBy", updatedProduct.PurchaseBy);

        //            connection.Execute("UpdatePurchaseProduct", parameters, commandType: CommandType.StoredProcedure);
        //        }

        //        return true; 
        //    }
        //    catch (Exception)
        //    {
        //        return false; 
        //    }
        //}

        
        //to delete
        public void DeletePurchase(int purchaseId)
        {
            using (var connection = _context.CreateConnection())
            {
                connection.Execute("DeletePurchaseProduct", new { PurchaseId = purchaseId }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchaseProducts()
        {
            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryAsync<Purchase>("GetAllProductsFromPurchase", commandType: CommandType.StoredProcedure);
            }
        }
        public async Task<IEnumerable<Purchase>> GetAllPurchaseProductsAsync(DateTime startDate, DateTime endDate)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new { StartDate = startDate, EndDate = endDate };
                return await connection.QueryAsync<Purchase>("GetPurchaseReportByDateRange", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
