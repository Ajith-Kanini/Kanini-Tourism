using System.ComponentModel.DataAnnotations;

namespace TravelAgencyManagementAPI.Models
{
    public class ImageGallery
    {
        [Key] 
        public int ImageId { get; set; }
        public string? ImageName { get; set; }
        public string? ImageUrl { get; set; }
    }
}
