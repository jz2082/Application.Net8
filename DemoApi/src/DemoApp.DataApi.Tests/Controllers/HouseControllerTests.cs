using System.Collections.Generic;
using System.Threading.Tasks;
using DemoApp.DataApi.Controllers;
using InMemoryDb.Model;
using Demo.InMemoryDb.Repository;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DemoApp.DataApi.Tests.Controllers
{
    public class HouseControllerTests
    {
        private readonly Mock<IHouseRepository> _repositoryMock;
        private readonly Mock<ILogger<HouseController>> _loggerMock;
        private readonly HouseController _controller;

        public HouseControllerTests()
        {
            _repositoryMock = new Mock<IHouseRepository>();
            _loggerMock = new Mock<ILogger<HouseController>>();
            _controller = new HouseController(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResult_WithHouseList()
        {
            // Arrange
            var houses = new List<House> { new House(), new House() };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(houses);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetAsync_ReturnsOkResult_WithHouse()
        {
            // Arrange
            var house = new House();
            _repositoryMock.Setup(r => r.GetAsync(1)).ReturnsAsync(house);

            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task AddAsync_ReturnsOkResult_WithHouse()
        {
            // Arrange
            var house = new House();
            _repositoryMock.Setup(r => r.AddAsync(house)).ReturnsAsync(house);

            // Act
            var result = await _controller.AddAsync(house);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsOkResult_WithHouse()
        {
            // Arrange
            var house = new House();
            _repositoryMock.Setup(r => r.UpdateAsync(house)).ReturnsAsync(house);

            // Act
            var result = await _controller.UpdateAsync(house);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsOkResult_WithTrue()
        {
            // Arrange
            _repositoryMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}