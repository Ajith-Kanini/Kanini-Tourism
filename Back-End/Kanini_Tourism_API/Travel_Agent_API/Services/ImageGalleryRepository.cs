using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyManagementAPI.DB;
using TravelAgencyManagementAPI.Models;
using TravelAgencyManagementAPI.Repositories;

namespace TravelAgencyManagementAPI.Services
{
    public class ImageGalleryRepository : IImageGalleryRepository
    {
        private readonly AgencyContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public ImageGalleryRepository(AgencyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<ImageGallery>> GetAllImageGalleries()
        {
            try
            {
                return await _context.ImageGalleries.ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exception (log, etc.)
                throw new Exception("An error occurred while getting image galleries.", ex);
            }
        }

        public async Task<ImageGallery> GetImageGalleryById(int id)
        {
            try
            {
                return await _context.ImageGalleries.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Handle exception (log, etc.)
                throw new Exception($"An error occurred while getting image gallery with ID {id}.", ex);
            }
        }

        public async Task<ImageGallery> CreateImageGallery([FromForm] ImageGallery imageGallery, IFormFile imageFile)
        {
            try
            {
                if (imageFile == null || imageFile.Length == 0)
                {
                    throw new ArgumentException("Invalid file");
                }

                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/gallery");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                imageGallery.ImageUrl = fileName;
                _context.ImageGalleries.Add(imageGallery);

                await _context.SaveChangesAsync(); 

                return imageGallery;
            }
            catch (Exception ex)
            {
                // Handle exception (log, etc.)
                throw new Exception("An error occurred while creating the image gallery.", ex);
            }
        }

        public async Task<bool> UpdateImageGallery(int id, ImageGallery imageGallery)
        {
            try
            {
                if (id != imageGallery.ImageId)
                {
                    return false;
                }

                _context.Entry(imageGallery).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ImageGalleryExists(id))
                {
                    return false;
                }
                throw;
            }
            catch (Exception ex)
            {
                // Handle exception (log, etc.)
                throw new Exception($"An error occurred while updating image gallery with ID {id}.", ex);
            }
        }

        public async Task<bool> DeleteImageGallery(int id)
        {
            try
            {
                var imageGallery = await _context.ImageGalleries.FindAsync(id);
                if (imageGallery == null)
                {
                    return false;
                }

                _context.ImageGalleries.Remove(imageGallery);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Handle exception (log, etc.)
                throw new Exception($"An error occurred while deleting image gallery with ID {id}.", ex);
            }
        }

        private async Task<bool> ImageGalleryExists(int id)
        {
            return await _context.ImageGalleries.AnyAsync(e => e.ImageId == id);
        }
    }
}
