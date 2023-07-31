
using Kanini_Tourism_API.Models;
using Microsoft.EntityFrameworkCore;
using Travel_Agent_API.Models;

namespace Travel_Agent_API.DB
{
    public class AgencyContext : DbContext
    {
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<TravelAgent> TravelAgents { get; set; }
        public DbSet<User> Users { get; set; }
        public AgencyContext(DbContextOptions<AgencyContext> options) : base(options)
        {

        }
    }
}
