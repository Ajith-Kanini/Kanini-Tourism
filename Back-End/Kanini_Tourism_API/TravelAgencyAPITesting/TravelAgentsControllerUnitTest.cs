using Microsoft.AspNetCore.Mvc;
using TravelAgencyManagementAPI.Controllers;
using TravelAgencyManagementAPI.Repositories;
using TravelAgencyManagementAPI.Models;
using Moq;

namespace TravelAgencyAPITesting
{
    public class TravelAgentsControllerUnitTest
    {
        // Mocked dependencies
        private readonly Mock<ITravelAgentRepository> mockRepository;
        private readonly TravelAgentsController controller;

        public TravelAgentsControllerUnitTest()
        {
            mockRepository = new Mock<ITravelAgentRepository>();
            controller = new TravelAgentsController(mockRepository.Object);
        }

        [Fact]
        public async Task GetTravelAgents_ReturnsOkResultWithListOfTravelAgents()
        {
            // Arrange
            var expectedTravelAgents = new List<TravelAgent> { new TravelAgent(), new TravelAgent() };
            mockRepository.Setup(repo => repo.GetTravelAgentsAsync()).ReturnsAsync(expectedTravelAgents);

            // Act
            var result = await controller.GetTravelAgents();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualTravelAgents = Assert.IsAssignableFrom<IEnumerable<TravelAgent>>(okResult.Value);
            Assert.Equal(expectedTravelAgents, actualTravelAgents);
        }

        
        [Fact]
        public async Task GetTravelAgent_WithValidId_ReturnsOkResultWithTravelAgent()
        {
            // Arrange
            var expectedTravelAgent = new TravelAgent { AgentId = 1, AgentName = "John Doe" };
            mockRepository.Setup(repo => repo.GetTravelAgentByIdAsync(1)).ReturnsAsync(expectedTravelAgent);

            // Act
            var result = await controller.GetTravelAgent(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualTravelAgent = Assert.IsType<TravelAgent>(okResult.Value);
            Assert.Equal(expectedTravelAgent.AgentId, actualTravelAgent.AgentId);
            Assert.Equal(expectedTravelAgent.AgentName, actualTravelAgent.AgentName);
        }

        [Fact]
        public async Task GetTravelAgent_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            mockRepository.Setup(repo => repo.GetTravelAgentByIdAsync(999)).ReturnsAsync((TravelAgent)null);

            // Act
            var result = await controller.GetTravelAgent(999);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }


    }
}
