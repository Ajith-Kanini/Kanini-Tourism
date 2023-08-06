using System.ComponentModel.DataAnnotations;

namespace UserManagementAPI.Models.DTO
{
    public class OfferDTO
    {
        [Key]
        public int TourOfferId { get; set; }


        public string? Name { get; set; }


    }
}
