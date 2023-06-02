using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Product
    {
        
            [Key]
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }

            public virtual ICollection<Purchase> Purchases { get; set; }
            public virtual ICollection<Issue> Issues { get; set; }
        
    }
}
