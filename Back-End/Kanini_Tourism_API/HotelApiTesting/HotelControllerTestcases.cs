using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagementAPI.Controllers;
using HotelManagementAPI.Models;
using HotelManagementAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HotelApiTesting
{
    public class HotelControllerTestcases
    {
        private readonly Mock<IHotelRepository> _mockRepository;
        private readonly HotelsController _controller;

        public HotelControllerTestcases()
        {
            _mockRepository = new Mock<IHotelRepository>();
            _controller = new HotelsController(_mockRepository.Object);
        }
        [Fact]
        public async Task GetHotels_ReturnsListOfHotels()
        {
            // Arrange
            var expectedHotels = new List<Hotel>
            {
                new Hotel { HotelId = 1, HotelName = "Hotel A",HotelCity="Chennai",StarRating=5 },
                new Hotel { HotelId = 2, HotelName = "Hotel B",HotelCity="Banglore",StarRating=4  }
            };

            _mockRepository.Setup(repo => repo.GetHotelsAsync()).ReturnsAsync(expectedHotels);

            var controller = new HotelsController(_mockRepository.Object);

            // Act
            var actionResult = await controller.GetHotels();
            var okResult = actionResult.Result as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

            var actualHotels = okResult.Value as IEnumerable<Hotel>;
            Assert.NotNull(actualHotels);
            Assert.IsType<List<Hotel>>(actualHotels);
        }


        [Fact]
        public async Task GetHotel_ReturnsHotelById()
        {
            // Arrange
            int hotelId = 11;
            var expectedHotel = new Hotel { HotelId = 11, HotelName = "Capella", HotelCity = "Chennai", StarRating = 5 };
            _mockRepository.Setup(repo => repo.GetHotelByIdAsync(hotelId)).ReturnsAsync(expectedHotel);

            // Act
            var result = await _controller.GetHotel(hotelId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualHotel = Assert.IsAssignableFrom<Hotel>(okResult.Value);
            Assert.Equal(expectedHotel.HotelId, actualHotel.HotelId);
            Assert.Equal(expectedHotel.HotelName, actualHotel.HotelName);
        }

        [Fact]
        public async Task GetHotel_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            int nonExistentHotelId = 11;
            _mockRepository.Setup(repo => repo.GetHotelByIdAsync(nonExistentHotelId)).ReturnsAsync(null as Hotel);

            // Act
            var result = await _controller.GetHotel(nonExistentHotelId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostHotel_ReturnsCreatedAtAction()
        {
            // Arrange
            var newHotel = new Hotel { HotelName = "New Hotel" };
            var createdHotel = new Hotel { HotelId = 3, HotelName = "New Hotel" };
            _mockRepository.Setup(repo => repo.AddHotelAsync(newHotel, null)).ReturnsAsync(createdHotel);

            // Act
            var result = await _controller.PostHotel(newHotel, null);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(HotelsController.GetHotel), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task DeleteHotel_ReturnsNotFoundForNonExistentId()
        {
            // Arrange
            int nonExistentHotelId = 99;
            _mockRepository.Setup(repo => repo.GetHotelByIdAsync(nonExistentHotelId)).ReturnsAsync(null as Hotel);

            // Act
            var result = await _controller.DeleteHotel(nonExistentHotelId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
