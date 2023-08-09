using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.Models.DTOs
{
    public class HotelDTO
    {
       
        public int HotelId { get; set; }

        
        public string? HotelName { get; set; }

        public string? HotelImage { get; set; }

      
        public string? HotelAddress { get; set; }

        public string? HotelCity { get; set; }

      
        public string? HotelCountry { get; set; }

        public int StarRating { get; set; }
    }
}
