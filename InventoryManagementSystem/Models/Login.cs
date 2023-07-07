using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Login
    {
        public int UserId { get; set; }

        [Required(ErrorMessage="UserName is Required")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "UserType is Required")]
        public string? UserType { get; set; }
    }
}
