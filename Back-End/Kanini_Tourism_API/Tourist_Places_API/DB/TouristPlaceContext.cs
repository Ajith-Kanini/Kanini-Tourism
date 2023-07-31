using Kanini_Tourism_API.Models;
using Microsoft.EntityFrameworkCore;
using Tourist_Places_API.Models;

namespace Tourist_Places_API.DB
{
    public class TouristPlaceContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Packages> Packages { get; set; }
        public TouristPlaceContext(DbContextOptions<TouristPlaceContext> options) : base(options)
        {

        }
    }
}
