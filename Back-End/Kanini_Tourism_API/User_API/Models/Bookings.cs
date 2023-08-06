using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserManagementAPI.Models.DTO;

namespace UserManagementAPI.Models
{
    public class Bookings
    {
        [Key]
        public int BookingId { get; set; }

        public DateTime BookingDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Number of persons must be greater than 0.")]
        public int NumberOfPersons { get; set; }

        public bool BookingStatus { get; set; }

        public int? Headcount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        // Foreign keys
        public int OfferId { get; set; }

        public int PackageId { get; set; }

        
        // Navigation properties

      

        public User? User { get; set; }
    }
}
