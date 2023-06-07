using System.ComponentModel.DataAnnotations;

namespace InventoryManagementSystem.Models
{
    public class Issue
    {
        public int IssueId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public bool Status { get; set; }
    }
}
