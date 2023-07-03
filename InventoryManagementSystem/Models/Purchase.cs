using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        public IEnumerable<SelectListItem> ProductList { get; set; } = new List<SelectListItem>();
        [Required]
        public string? Purchase_product { get; set; }

        [Required]
        public int Purchase_quantity { get; set; }
        [Required]
        public decimal Purchase_price { get; set; }

        [Display(Name = "Purchase Date")]
        
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public string? PurchaseBy { get; set; }
        public bool Status { get; set; }

    }
}
