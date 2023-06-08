using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Users
    {
        public int UserId { get; set; }

       
        [Required(ErrorMessage = "UserName is the required field")]  
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is the required field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm_Password is the required field")]
        [DataType (DataType.Password)]
        [Compare("Password",ErrorMessage ="Password is not Identical")]
        public string Confirm_Password { get; set; }

        [Required(ErrorMessage = "Email is the required field")]
        public string? Email { get; set; }
        [RegularExpression("^[\\w\\.-]+@[\\w\\.-]+\\.\\w+$",ErrorMessage ="Invalid Email")]
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public bool Status { get; set; }

        public string? UserType { get; set; }
    }
}
