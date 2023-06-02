using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Issue
    {
        [Key]
        public int IssueId { get; set; }
        public DateTime IssueDate { get; set; }
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
