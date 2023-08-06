using System.ComponentModel.DataAnnotations;

namespace TourManagementAPI.Models
{
    public class TouristAttraction
    {
        [Key]
        public int TouristAttractionId { get; set; }

        [MaxLength(100, ErrorMessage = "Attraction Name cannot exceed 100 characters.")]
        public string? AttractionName { get; set; }

        [MaxLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string? Location { get; set; }

        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [MaxLength(200, ErrorMessage = "Image URL cannot exceed 200 characters.")]
        public string? ImageUrl { get; set; }

        [MaxLength(100, ErrorMessage = "Category cannot exceed 100 characters.")]
        public string? Category { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public double Rating { get; set; }
    }
}
