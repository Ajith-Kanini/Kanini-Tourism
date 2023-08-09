
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagementAPI.Models;
using UserManagementAPI.Models.DTO;

namespace UserManagementAPI.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> AddUserAsync([FromForm] User user , IFormFile imageFile);
        Task<User> UpdateUserAsync(int id, [FromForm] User user, IFormFile imageFile);
        Task DeleteUserAsync(User user);
        Task<UserDTO> Register(UserDTO user);
    }
}
