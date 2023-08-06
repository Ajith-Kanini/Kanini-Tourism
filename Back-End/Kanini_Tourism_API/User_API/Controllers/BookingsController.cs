using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Repositories;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingsController(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookings>>> GetBookings()
        {
            var bookings = await _bookingRepository.GetBookingsAsync();
            if (!bookings.Any())
            {
                return NotFound();
            }
            return Ok(bookings);
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bookings>> GetBookings(int id)
        {
            var bookings = await _bookingRepository.GetBookingByIdAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            return Ok(bookings);
        }

        // PUT: api/Bookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookings(int id, Bookings bookings)
        {
            if (id != bookings.BookingId)
            {
                return BadRequest();
            }

            var updatedBooking = await _bookingRepository.UpdateBookingAsync(id, bookings);
            if (updatedBooking == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<Bookings>> PostBookings(Bookings bookings)
        {
            var addedBooking = await _bookingRepository.AddBookingAsync(bookings);
            return CreatedAtAction("GetBookings", new { id = addedBooking.BookingId }, addedBooking);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookings(int id)
        {
            var bookings = await _bookingRepository.GetBookingByIdAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }

            await _bookingRepository.DeleteBookingAsync(bookings);
            return NoContent();
        }
    }
}
