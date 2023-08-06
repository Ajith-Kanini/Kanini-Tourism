using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagementAPI.Models;

namespace HotelManagementAPI.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetRoomsAsync();
        Task<Room> GetRoomByIdAsync(int id);
        Task<Room> AddRoomAsync(Room room);
        Task<Room> UpdateRoomAsync(int id, Room room);
        Task DeleteRoomAsync(Room room);
    }
}
