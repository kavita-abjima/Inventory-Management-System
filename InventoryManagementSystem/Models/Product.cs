using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Product
    {

        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = null!;
        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
       
       
    }
}
