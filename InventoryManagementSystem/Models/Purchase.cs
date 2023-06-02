using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Purchase
    {
            [Key]
            public int PurchaseId { get; set; }
            public DateTime PurchaseDate { get; set; }
            public int Quantity { get; set; }

            public int ProductId { get; set; }
            public virtual Product Product { get; set; }
        
    }
}
