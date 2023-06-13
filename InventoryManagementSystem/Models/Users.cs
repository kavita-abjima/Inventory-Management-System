using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Users
    {
        public int UserId { get; set; }

       
        [Required]  
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       

        [Required]
        [RegularExpression("^[\\w\\.-]+@[\\w\\.-]+\\.\\w+$", ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }
        [Required]
        public string? UserType { get; set; }
    }
}
