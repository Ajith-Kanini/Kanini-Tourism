using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Models;
using UserManagementAPI.DB;

namespace UserManagementAPI.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly UserContext _context;

        public BookingRepository(UserContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Bookings>> GetBookingsAsync()
        {
            return await _context.Bookings.Include(x=>x.User).ToListAsync();
        }

        public async Task<Bookings> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        public async Task<Bookings> AddBookingAsync(Bookings booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == booking.User.UserId);
            booking.User=user;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<Bookings> UpdateBookingAsync(int id, Bookings booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            var existingBooking = await _context.Bookings.FindAsync(id);
            if (existingBooking == null)
            {
                return null;
            }

            // Update properties of existingBooking

            _context.Entry(existingBooking).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingBooking;
        }

        public async Task DeleteBookingAsync(Bookings booking)
        {
            if (booking == null)
            {
                throw new ArgumentNullException(nameof(booking));
            }

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}
