using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.Models
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }
    
        [Required]
        [MaxLength(10)]
        public string? RoomNumber { get; set; }
       

        [Range(0, double.MaxValue)]
        public decimal PricePerNight { get; set; }

        // Navigation properties
        public Hotel? Hotel { get; set; }
        public List<Booking>? Bookings { get; set; }
    }

}
