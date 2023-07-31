using Hotels_API.Models;
using Kanini_Tourism_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Hotels_API.DB
{
    public class HotelContext:DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Hotel> Hotels { get; set; }

        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {

        }
    }
}
