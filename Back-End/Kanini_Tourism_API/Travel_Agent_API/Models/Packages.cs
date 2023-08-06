using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TravelAgencyManagementAPI.Models.DTO;

namespace TravelAgencyManagementAPI.Models
{
    public class Packages
    {
        [Key]
        public int PackageId { get; set; }

        //[MaxLength(100, ErrorMessage = "Package Name cannot exceed 100 characters.")]
        public string? PackageName { get; set; }

        //[MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public string? PackageImage { get; set; }
        //[MaxLength(100, ErrorMessage = "Destination cannot exceed 100 characters.")]
        public string? Destination { get; set; }

        public decimal? Price { get; set; }

        //[MaxLength(50, ErrorMessage = "Duration cannot exceed 50 characters.")]
        public string? Duration { get; set; }

        //[DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        //[DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public ICollection<HotelDTO>? HotelIds { get; set; }
        public ICollection<TouristAttractionDTO>? TouristAttractionIds { get; set; }


    }
}
