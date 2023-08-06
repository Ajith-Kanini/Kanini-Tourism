using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TourManagementAPI.Models;
using TourManagementAPI.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace TourManagementAPI.Repositories
{
    public class TouristAttractionRepository : ITouristAttractionRepository
    {
        private readonly TouristPlaceContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public TouristAttractionRepository(TouristPlaceContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<TouristAttraction>> GetTouristAttractionsAsync()
        {
            return await _context.TouristAttractions.ToListAsync();
        }

        public async Task<TouristAttraction> GetTouristAttractionByIdAsync(int id)
        {
            return await _context.TouristAttractions.FindAsync(id);
        }

        public async Task<TouristAttraction> AddTouristAttractionAsync([FromForm] TouristAttraction touristAttraction, IFormFile imageFile)
        {
            if (touristAttraction == null)
            {
                throw new ArgumentNullException(nameof(touristAttraction));
            }
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/spots");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            touristAttraction.ImageUrl = fileName;


            _context.TouristAttractions.Add(touristAttraction);
            await _context.SaveChangesAsync();

            return touristAttraction;
        }

        public async Task<TouristAttraction> UpdateTouristAttractionAsync(int id, [FromForm] TouristAttraction touristAttraction, IFormFile imageFile)
        {
            var existingtouristAttraction = await _context.TouristAttractions.FindAsync(id);

            if (existingtouristAttraction == null)
            {
                throw new ArgumentException("Spot not found");
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/spots");

                if (!string.IsNullOrEmpty(existingtouristAttraction.ImageUrl))
                {
                    var existingFilePath = Path.Combine(uploadsFolder, existingtouristAttraction.ImageUrl);
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

                touristAttraction.ImageUrl = fileName;
            }
            else
            {
                touristAttraction.ImageUrl = existingtouristAttraction.ImageUrl;
            }

            existingtouristAttraction.AttractionName = touristAttraction.AttractionName;
            existingtouristAttraction.Description = touristAttraction.Description;
            existingtouristAttraction.Location = touristAttraction.Location;
            existingtouristAttraction.Category = touristAttraction.Category;
            existingtouristAttraction.Rating = touristAttraction.Rating;
            existingtouristAttraction.ImageUrl = touristAttraction.ImageUrl;
            await _context.SaveChangesAsync();

            return existingtouristAttraction;
        }

        public async Task DeleteTouristAttractionAsync(TouristAttraction touristAttraction)
        {
            if (touristAttraction == null)
            {
                throw new ArgumentNullException(nameof(touristAttraction));
            }

            _context.TouristAttractions.Remove(touristAttraction);
            await _context.SaveChangesAsync();
        }
    }
}
