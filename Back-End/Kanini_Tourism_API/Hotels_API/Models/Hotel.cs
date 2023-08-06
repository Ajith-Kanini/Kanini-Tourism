using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        [Required(ErrorMessage = "Hotel Name is required.")]
        [MaxLength(100, ErrorMessage = "Hotel Name cannot exceed 100 characters.")]
        public string? HotelName { get; set; }

        public string? HotelImage { get; set; }

        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? HotelAddress { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string? HotelCity { get; set; }

        [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string? HotelCountry { get; set; }

        [Range(1, 5, ErrorMessage = "Star rating must be between 1 and 5.")]
        public int StarRating { get; set; }


    }
}
