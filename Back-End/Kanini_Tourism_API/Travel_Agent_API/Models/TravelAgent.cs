using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyManagementAPI.Models
{
    public class TravelAgent
    {
        [Key]
        public int AgentId { get; set; }

        
        [MaxLength(100, ErrorMessage = "Agency Name cannot exceed 100 characters.")]
        public string? AgentName { get; set; }

        public string? AgencyName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [MaxLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string? AgentEmail { get; set; }


        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        public string? AgentPassword { get; set; }

        public string? AgentImage { get; set; }

        [MaxLength(10, ErrorMessage = "Phone number cannot exceed 10 characters.")]
        public string? AgentPhoneNumber { get; set; }

        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string? AgentAddress { get; set; }

        [MaxLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string? AgentCity { get; set; }

        public bool AgentStatus { get; set; }

        [DataType(DataType.DateTime)]
        public string? AgentRegistrationDate { get; set; }
    }
}
