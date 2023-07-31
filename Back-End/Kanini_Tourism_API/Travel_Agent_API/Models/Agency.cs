using System.ComponentModel.DataAnnotations;

namespace Travel_Agent_API.Models
{
    public class Agency
    {
        [Key]
        public int AgencyId { get; set; }

        [Required(ErrorMessage = "Agency Name is required.")]
        [MaxLength(100, ErrorMessage = "Agency Name cannot exceed 100 characters.")]
        public string? AgencyName { get; set; }

        [Required(ErrorMessage = "Contact Person Name is required.")]
        [MaxLength(100, ErrorMessage = "Contact Person Name cannot exceed 100 characters.")]
        public string? ContactPersonName { get; set; }

        public string? AgencyImage { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        [MaxLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string? Country { get; set; }

        [MaxLength(2000, ErrorMessage = "About Agency cannot exceed 2000 characters.")]
        public string? AboutAgency { get; set; }

        [Required(ErrorMessage = "Date of Registration is required.")]
        [DataType(DataType.DateTime)]
        public DateTime RegistrationDate { get; set; }

        public ICollection<TravelAgent>? TravelAgents { get; set; }
    }
}
