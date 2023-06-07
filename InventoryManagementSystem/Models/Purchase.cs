using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Purchase
    {
        
            public int PurchaseId { get; set; }
            public int ProductId { get; set; }
            public int UserId { get; set; }
            public DateTime PurchaseDate { get; set; }
            public int Quantity { get; set; }
            public DateTime CreatedDate { get; set; }
            public int CreatedBy { get; set; }
            public DateTime ModifiedDate { get; set; }
            public int ModifiedBy { get; set; }
            public bool Status { get; set; }
        

    }
}
