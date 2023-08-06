using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TourManagementAPI.Models;

namespace TourManagementAPI.Repositories
{
    public interface ITouristAttractionRepository
    {
        Task<IEnumerable<TouristAttraction>> GetTouristAttractionsAsync();
        Task<TouristAttraction> GetTouristAttractionByIdAsync(int id);
        Task<TouristAttraction> AddTouristAttractionAsync([FromForm] TouristAttraction touristAttraction, IFormFile imageFile);
        Task<TouristAttraction> UpdateTouristAttractionAsync(int id, [FromForm] TouristAttraction touristAttraction, IFormFile imageFile);
        Task DeleteTouristAttractionAsync(TouristAttraction touristAttraction);
    }
}
