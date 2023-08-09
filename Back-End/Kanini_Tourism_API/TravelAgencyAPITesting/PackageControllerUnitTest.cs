using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TravelAgencyManagementAPI.Controllers;
using TravelAgencyManagementAPI.Models;
using TravelAgencyManagementAPI.Repositories;

namespace TravelAgentsControllerUnitTest
{
    public class PackageControllerUnitTest
    {
        private readonly Mock<IPackageRepository> mockPackageRepository;
        private readonly PackagesController controller;

        public PackageControllerUnitTest()
        {
            mockPackageRepository = new Mock<IPackageRepository>();
            controller = new PackagesController(mockPackageRepository.Object);
        }

        [Fact]
        public async Task GetPackages_ReturnsListOfPackages()
        {
            // Arrange
            var expectedPackages = new List<Packages> { new Packages(), new Packages() };
            mockPackageRepository.Setup(repo => repo.GetPackagesAsync()).ReturnsAsync(expectedPackages);

            // Act
            var result = await controller.GetPackages();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualPackages = Assert.IsAssignableFrom<IEnumerable<Packages>>(okResult.Value);
            Assert.Equal(expectedPackages, actualPackages);
        }

        
        [Fact]
        public async Task GetPackage_WithValidId_ReturnsPackage()
        {
            // Arrange
            var expectedPackage = new Packages { PackageId = 1, PackageName = "Goa" };
            mockPackageRepository.Setup(repo => repo.GetPackageByIdAsync(1)).ReturnsAsync(expectedPackage);

            // Act
            var result = await controller.GetPackage(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualPackage = Assert.IsType<Packages>(okResult.Value);
            Assert.Equal(expectedPackage.PackageId, actualPackage.PackageId);
            Assert.Equal(expectedPackage.PackageName, actualPackage.PackageName);
        }

    }
}
