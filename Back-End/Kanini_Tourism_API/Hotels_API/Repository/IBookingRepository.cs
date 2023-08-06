using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagementAPI.Models;

namespace HotelManagementAPI.Repositories
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> GetBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task<Booking> AddBookingAsync(Booking booking);
        Task<Booking> UpdateBookingAsync(int id, Booking booking);
        Task DeleteBookingAsync(Booking booking);
    }
}
