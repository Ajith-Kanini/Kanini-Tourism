using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagementAPI.Controllers;
using HotelManagementAPI.Models;
using HotelManagementAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HotelApiTesting
{
    public class RoomControllerUnitTest
    {
        private readonly Mock<IRoomRepository> _mockRepository;
        private readonly RoomsController _controller;

        public RoomControllerUnitTest()
        {
            _mockRepository = new Mock<IRoomRepository>();
            _controller = new RoomsController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetRooms_ReturnsListOfRooms()
        {
            // Arrange
            var expectedRooms = new List<Room>
            {
                new Room { RoomId = 1, RoomNumber = "101" },
                new Room { RoomId = 2, RoomNumber = "102" }
            };
            _mockRepository.Setup(repo => repo.GetRoomsAsync()).ReturnsAsync(expectedRooms);

            // Act
            var result = await _controller.GetRooms();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualRooms = Assert.IsAssignableFrom<IEnumerable<Room>>(okResult.Value);
        }

        [Fact]
        public async Task GetRoom_ReturnsRoomById()
        {
            // Arrange
            int roomId = 1;
            var expectedRoom = new Room { RoomId = roomId, RoomNumber = "101" };
            _mockRepository.Setup(repo => repo.GetRoomByIdAsync(roomId)).ReturnsAsync(expectedRoom);

            // Act
            var result = await _controller.GetRoom(roomId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualRoom = Assert.IsAssignableFrom<Room>(okResult.Value);
            Assert.Equal(expectedRoom.RoomId, actualRoom.RoomId);
            Assert.Equal(expectedRoom.RoomNumber, actualRoom.RoomNumber);
        }

        [Fact]
        public async Task PutRoom_ReturnsNotFoundForNonExistentId()
        {
            // Arrange
            int nonExistentRoomId = 99;
            var roomToUpdate = new Room { RoomId = nonExistentRoomId, RoomNumber = "Updated Room" };
            _mockRepository.Setup(repo => repo.UpdateRoomAsync(nonExistentRoomId, roomToUpdate)).ReturnsAsync(null as Room);

            // Act
            var result = await _controller.PutRoom(nonExistentRoomId, roomToUpdate);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteRoom_ReturnsNoContentForValidId()
        {
            // Arrange
            int roomId = 1;
            var roomToDelete = new Room { RoomId = roomId, RoomNumber = "101" };
            _mockRepository.Setup(repo => repo.GetRoomByIdAsync(roomId)).ReturnsAsync(roomToDelete);

            // Act
            var result = await _controller.DeleteRoom(roomId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
