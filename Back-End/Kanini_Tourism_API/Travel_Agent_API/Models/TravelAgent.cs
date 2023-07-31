using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Travel_Agent_API.Models
{
    public class TravelAgent
    {
        [Key]
        public int AgentId { get; set; }

        [Required(ErrorMessage = "Agency Name is required.")]
        [MaxLength(100, ErrorMessage = "Agency Name cannot exceed 100 characters.")]
        public string? AgentName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? AgentEmail { get; set; }


        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string? AgentPassword { get; set; }

        public string? AgentImage { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 characters.")]
        public string? AgentPhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? AgentAddress { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string? AgentCity { get; set; }

        public bool AgentStatus { get; set; }

        [Required(ErrorMessage = "Date of Registration is required.")]
        [DataType(DataType.DateTime)]
        public DateTime AgentRegistrationDate { get; set; }

        public Agency? Agency { get; set; }
    }
}
