using HotelManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelManagementAPI.DB
{
    public class HotelContext : DbContext
    {
       

        public DbSet<Hotel> Hotels { get; set; }


        public DbSet<Booking> Bookings { get; set; }


        public DbSet<Room> Rooms { get; set; }

        public HotelContext(DbContextOptions<HotelContext> options) : base(options)
        {

        }

      
    }
}
