using System;
using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int UserId { get; set; }

        [DataType(DataType.Date)]
        public DateTime CheckInDate { get; set; }

        
        [DataType(DataType.Date)]
        public DateTime CheckOutDate { get; set; }

        [Required]
        public bool Status { get; set; }

        [Range(0, double.MaxValue)]
        public decimal TotalAmount { get; set; }

        // Navigation properties
        [Required]
        public Hotel? Hotel { get; set; }

        [Required]
        public Room? Room { get; set; }
    }
}
