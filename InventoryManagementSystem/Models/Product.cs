using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Product
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
