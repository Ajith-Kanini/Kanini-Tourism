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
    public class HotelBookingUnitTest
    {
        private readonly Mock<IBookingRepository> _mockRepository;
        private readonly BookingsController _controller;

        public HotelBookingUnitTest()
        {
            _mockRepository = new Mock<IBookingRepository>();
            _controller = new BookingsController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetBookings_ReturnsListOfBookings()
        {
            // Arrange
            var expectedBookings = new List<Booking>
            {
                new Booking { BookingId = 1, UserId = 1 },
                new Booking { BookingId = 2, UserId =1 }
            };
            _mockRepository.Setup(repo => repo.GetBookingsAsync()).ReturnsAsync(expectedBookings);

            // Act
            var result = await _controller.GetBookings();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualBookings = Assert.IsAssignableFrom<IEnumerable<Booking>>(okResult.Value);

        }

        [Fact]
        public async Task GetBooking_ReturnsNotFoundForInvalidId()
        {
            // Arrange
            int nonExistentBookingId = 99;
            _mockRepository.Setup(repo => repo.GetBookingByIdAsync(nonExistentBookingId)).ReturnsAsync(null as Booking);

            // Act
            var result = await _controller.GetBooking(nonExistentBookingId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutBooking_ReturnsNotFoundForNonExistentId()
        {
            // Arrange
            int nonExistentBookingId = 99;
            var bookingToUpdate = new Booking { BookingId = nonExistentBookingId, UserId = 1 };
            _mockRepository.Setup(repo => repo.UpdateBookingAsync(nonExistentBookingId, bookingToUpdate)).ReturnsAsync(null as Booking);

            // Act
            var result = await _controller.PutBooking(nonExistentBookingId, bookingToUpdate);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteBooking_ReturnsNoContentForValidId()
        {
            // Arrange
            int bookingId = 1;
            var bookingToDelete = new Booking { BookingId = bookingId, UserId = 1 };
            _mockRepository.Setup(repo => repo.GetBookingByIdAsync(bookingId)).ReturnsAsync(bookingToDelete);

            // Act
            var result = await _controller.DeleteBooking(bookingId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
