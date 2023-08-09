using System.ComponentModel.DataAnnotations;

namespace TravelAgencyManagementAPI.Models
{
    public class TourOffer
    {
        [Key]
        public int TourOfferId { get; set; }

        
        public string? OfferName { get; set; }

        public string? Description { get; set; }

      
        [DataType(DataType.Date)]
        public string? StartDate { get; set; }

        [DataType(DataType.Date)]
        public string? EndDate { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }


        // Navigation property
        public TravelAgent? Agent { get; set; }
    }
}
