using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Purchase
    {
           public int PurchaseId { get; set; }

           public int Purchase_product { get; set; }
           public int Purchase_quantity { get; set; }

           public int Purchase_price { get; set; }
           public DateTime PurchaseDate { get; set; }
           public int PurchaseBy { get; set; }
           public bool Status { get; set; }
       
    }
}
