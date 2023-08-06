using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelManagementAPI.Models;
using HotelManagementAPI.DB;
using HotelManagementAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementAPI.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public HotelRepository(HotelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<Hotel>> GetHotelsAsync()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            return await _context.Hotels.FindAsync(id);
        }

        public async Task<Hotel> AddHotelAsync([FromForm] Hotel hotel, IFormFile imageFile)
        {

            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/hotels");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);
            hotel.HotelImage = fileName;
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();

            return hotel;
        }

        public async Task<Hotel> UpdateHotelAsync(int id, [FromForm] Hotel hotel, IFormFile imageFile)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            var existingHotel = await _context.Hotels.FindAsync(id);

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/hotels");

                if (!string.IsNullOrEmpty(existingHotel.HotelImage))
                {
                    var existingFilePath = Path.Combine(uploadsFolder, existingHotel.HotelImage);
                    if (File.Exists(existingFilePath))
                    {
                        File.Delete(existingFilePath);
                    }
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                hotel.HotelImage = fileName;
            }
            else
            {
                hotel.HotelImage = existingHotel.HotelImage;
            }

            if (existingHotel == null)
            {
                return null;
            }

            existingHotel.HotelName=hotel.HotelName;
            existingHotel.HotelAddress=hotel.HotelAddress;
            existingHotel.HotelCity=hotel.HotelCity;
            existingHotel.HotelCountry=hotel.HotelCountry;
            existingHotel.StarRating=hotel.StarRating;
            await _context.SaveChangesAsync();

            return existingHotel;
        }

        public async Task DeleteHotelAsync(Hotel hotel)
        {
            if (hotel == null)
            {
                throw new ArgumentNullException(nameof(hotel));
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();
        }
    }
}
