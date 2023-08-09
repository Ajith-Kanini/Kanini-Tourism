using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TourManagementAPI.Controllers;
using TourManagementAPI.Models;
using TourManagementAPI.Repositories;
using Xunit;

namespace TourManagementApiTesting
{
    public class TouristAttractionsControllerUnitTesting
    {
        private readonly Mock<ITouristAttractionRepository> _mockRepository;
        private readonly TouristAttractionsController _controller;

        public TouristAttractionsControllerUnitTesting()
        {
            _mockRepository = new Mock<ITouristAttractionRepository>();
            _controller = new TouristAttractionsController(_mockRepository.Object);
        }

        [Fact]
        public async Task GetTouristAttractions_ReturnsListOfTouristAttractions()
        {

            // Arrange
            var expectedAttractions = new List<TouristAttraction>
            {
                new TouristAttraction { TouristAttractionId = 1, AttractionName = "Goa" },
                new TouristAttraction { TouristAttractionId = 2, AttractionName = "Goa" }
            };
            _mockRepository.Setup(repo => repo.GetTouristAttractionsAsync()).ReturnsAsync(expectedAttractions);

            // Act
            var result = await _controller.GetTouristAttractions();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualAttractions = Assert.IsAssignableFrom<IEnumerable<TouristAttraction>>(okResult.Value);

        }
        [Fact]
        public async Task GetTouristAttraction_ReturnsTouristAttractionById()
        {
            // Arrange
            int attractionId = 1;
            var expectedAttraction = new TouristAttraction { TouristAttractionId = attractionId, AttractionName = "Goa" };
            _mockRepository.Setup(repo => repo.GetTouristAttractionByIdAsync(attractionId)).ReturnsAsync(expectedAttraction);

            // Act
            var result = await _controller.GetTouristAttraction(attractionId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualAttraction = Assert.IsAssignableFrom<TouristAttraction>(okResult.Value);
            Assert.Equal(expectedAttraction.TouristAttractionId, actualAttraction.TouristAttractionId);
            Assert.Equal(expectedAttraction.AttractionName, actualAttraction.AttractionName);
        }

        [Fact]
            public async Task GetTouristAttraction_ReturnsNotFoundForInvalidId()
            {
                // Arrange
                int nonExistentAttractionId = 99;
                _mockRepository.Setup(repo => repo.GetTouristAttractionByIdAsync(nonExistentAttractionId)).ReturnsAsync(null as TouristAttraction);

                // Act
                var result = await _controller.GetTouristAttraction(nonExistentAttractionId);

                // Assert
                Assert.IsType<NotFoundResult>(result.Result);
            }

            [Fact]
            public async Task PutTouristAttraction_ReturnsNotFoundForNonExistentId()
            {
                // Arrange
                int nonExistentAttractionId = 99;
                var attractionToUpdate = new TouristAttraction { TouristAttractionId = nonExistentAttractionId, AttractionName = "Goa" };
                _mockRepository.Setup(repo => repo.UpdateTouristAttractionAsync(nonExistentAttractionId, attractionToUpdate, null)).ReturnsAsync(null as TouristAttraction);

                // Act
                var result = await _controller.PutTouristAttraction(nonExistentAttractionId, attractionToUpdate, null);

                // Assert
                Assert.IsType<NotFoundResult>(result);
            }

            [Fact]
            public async Task DeleteTouristAttraction_ReturnsNoContentForValidId()
            {
                // Arrange
                int attractionId = 1;
                var attractionToDelete = new TouristAttraction { TouristAttractionId = attractionId, AttractionName = "Goa" };
                _mockRepository.Setup(repo => repo.GetTouristAttractionByIdAsync(attractionId)).ReturnsAsync(attractionToDelete);

                // Act
                var result = await _controller.DeleteTouristAttraction(attractionId);

                // Assert
                Assert.IsType<NoContentResult>(result);
            }

           
        
    }
}