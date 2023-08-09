using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HotelManagementAPI.Models;
using HotelManagementAPI.DB;

namespace HotelManagementAPI.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelContext _context;

        public RoomRepository(HotelContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetRoomByIdAsync(int id)
        {
            return await _context.Rooms.FindAsync(id);
        }

        public async Task<Room> AddRoomAsync(Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }
           
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();

            return room;
        }

        public async Task<Room> UpdateRoomAsync(int id, Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            var existingRoom = await _context.Rooms.FindAsync(id);
            if (existingRoom == null)
            {
                return null;
            }
            _context.Entry(existingRoom).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return existingRoom;
        }

        public async Task DeleteRoomAsync(Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room));
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }
    }
}
