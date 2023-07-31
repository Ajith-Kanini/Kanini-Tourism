using System.ComponentModel.DataAnnotations;

namespace Tourist_Places_API.Models
{
    public class Packages
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Package Name is required.")]
        [MaxLength(100, ErrorMessage = "Package Name cannot exceed 100 characters.")]
        public string PackageName { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Destination is required.")]
        [MaxLength(100, ErrorMessage = "Destination cannot exceed 100 characters.")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Duration is required.")]
        [MaxLength(50, ErrorMessage = "Duration cannot exceed 50 characters.")]
        public string Duration { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        // Navigation properties to represent relationships with other entities
        /*public ICollection<Hotel> Hotels { get; set; }
        public ICollection<TouristAttraction> TouristAttractions { get; set; }*/

    }
}
