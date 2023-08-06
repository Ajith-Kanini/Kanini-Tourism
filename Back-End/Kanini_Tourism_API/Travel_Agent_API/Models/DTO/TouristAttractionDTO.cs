using System.ComponentModel.DataAnnotations;

namespace TravelAgencyManagementAPI.Models.DTO
{
    public class TouristAttractionDTO
    {
        [Key]
        public int TouristAttractionId { get; set; }
    }
}
