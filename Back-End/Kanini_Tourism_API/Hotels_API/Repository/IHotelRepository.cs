using HotelManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagementAPI.Repository
{
    public interface IHotelRepository
    {
        Task<IEnumerable<Hotel>> GetHotelsAsync();
        Task<Hotel> GetHotelByIdAsync(int id);
        Task<Hotel> AddHotelAsync([FromForm] Hotel hotel, IFormFile imageFile);
        Task<Hotel> UpdateHotelAsync(int id, [FromForm] Hotel hotel, IFormFile imageFile);
        Task DeleteHotelAsync(Hotel hotel);
    }
}
