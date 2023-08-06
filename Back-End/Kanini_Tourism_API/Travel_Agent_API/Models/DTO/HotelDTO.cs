using System.ComponentModel.DataAnnotations;

namespace TravelAgencyManagementAPI.Models.DTO
{
    public class HotelDTO
    {
        [Key]
        public int HotelId { get; set; }
    }
}
