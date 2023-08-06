using Microsoft.EntityFrameworkCore;
using TravelAgencyManagementAPI.Models;
using TravelAgencyManagementAPI.Models.DTO;

namespace TravelAgencyManagementAPI.DB
{
    public class AgencyContext : DbContext
    {
        public DbSet<TravelAgent> TravelAgents { get; set; }
        public DbSet<HotelDTO> hotelDTOs { get; set; }
        public DbSet<TouristAttractionDTO> touristAttractionDTOs { get; set; }
        public DbSet<ImageGallery> ImageGalleries { get; set; }
        public DbSet<TourOffer> TourOffers { get; set; }
        public DbSet<Packages> Packages { get; set; }
        public AgencyContext(DbContextOptions<AgencyContext> options) : base(options)
        {

        }


    }
}
