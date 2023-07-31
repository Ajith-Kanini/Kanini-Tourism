using Kanini_Tourism_API.Models;
using Microsoft.EntityFrameworkCore;

namespace User_API.DB
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
    }
}
