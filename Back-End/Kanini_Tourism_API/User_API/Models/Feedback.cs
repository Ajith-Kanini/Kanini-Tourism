
using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }
        public string? UserName { get; set; }
        public string? UserEmail { get; set; }
        public string? FeedbackDescription { get; set; }

    }
}
