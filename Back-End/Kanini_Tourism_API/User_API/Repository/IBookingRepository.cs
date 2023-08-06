
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementAPI.Models;

namespace UserManagementAPI.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Bookings>> GetBookingsAsync();
        Task<Bookings> GetBookingByIdAsync(int id);
        Task<Bookings> AddBookingAsync(Bookings booking);
        Task<Bookings> UpdateBookingAsync(int id, Bookings booking);
        Task DeleteBookingAsync(Bookings booking);
    }
}
