using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelAgencyManagementAPI.Models;

namespace TravelAgencyManagementAPI.Repositories
{
    public interface IImageGalleryRepository
    {
        Task<IEnumerable<ImageGallery>> GetAllImageGalleries();
        Task<ImageGallery> GetImageGalleryById(int id);
        Task<ImageGallery> CreateImageGallery( [FromForm] ImageGallery imageGallery, IFormFile imageFile);
        Task<bool> UpdateImageGallery(int id, ImageGallery imageGallery);
        Task<bool> DeleteImageGallery(int id);
    }
}
