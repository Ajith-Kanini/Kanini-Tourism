using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using UserManagementAPI.Controllers;
using UserManagementAPI.Models;
using UserManagementAPI.Repositories;

namespace UserManagementApiTesting
{
    public class UserControllerUnitTest
    {
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly UsersController controller;

        public UserControllerUnitTest()
        {
            mockUserRepository = new Mock<IUserRepository>();
            controller = new UsersController(mockUserRepository.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsListOfUsers()
        {
            // Arrange
            var expectedUsers = new List<User> { new User(), new User() };
            mockUserRepository.Setup(repo => repo.GetUsersAsync()).ReturnsAsync(expectedUsers);

            // Act
            var result = await controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualUsers = Assert.IsAssignableFrom<IEnumerable<User>>(okResult.Value);
            Assert.Equal(expectedUsers, actualUsers);
        }

        

        // Example test for GetUser:
        [Fact]
        public async Task GetUser_WithValidId_ReturnsUser()
        {
            // Arrange
            var expectedUser = new User { UserId = 1, FullName = "Ajith" };
            mockUserRepository.Setup(repo => repo.GetUserByIdAsync(1)).ReturnsAsync(expectedUser);

            // Act
            var result = await controller.GetUser(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal(expectedUser.UserId, actualUser.UserId);
            Assert.Equal(expectedUser.FullName, actualUser.FullName);
        }

        
    }
}
