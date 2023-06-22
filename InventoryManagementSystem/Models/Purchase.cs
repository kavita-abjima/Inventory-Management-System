using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Purchase
    {
           public int PurchaseId { get; set; }

           public string? Purchase_product { get; set; }
           public int Purchase_quantity { get; set; }

           public decimal Purchase_price { get; set; }
         
           public string? PurchaseBy { get; set; }
           public bool Status { get; set; }
       
    }
}
