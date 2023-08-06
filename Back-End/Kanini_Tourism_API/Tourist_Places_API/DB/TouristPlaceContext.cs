using Microsoft.EntityFrameworkCore;
using TourManagementAPI.Models;

namespace TourManagementAPI.DB
{
    public class TouristPlaceContext : DbContext
    {
       

        public DbSet<TouristAttraction> TouristAttractions { get; set; }
        public TouristPlaceContext(DbContextOptions<TouristPlaceContext> options) : base(options)
        {

        }
    }
}
