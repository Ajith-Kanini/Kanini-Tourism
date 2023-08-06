using System.ComponentModel.DataAnnotations;

namespace TravelAgencyManagementAPI.Models.DTO
{
    public class PackageDTO
    {
       
        public int PackageId { get; set; }

        public string? PackageName { get; set; }

        public string? Description { get; set; }

        public string? Destination { get; set; }

        public decimal Price { get; set; }

        public string? Duration { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ICollection<HotelDTO>? HotelIds { get; set; }
        public ICollection<TouristAttractionDTO>? TouristAttractionIds { get; set; }

    }
}
