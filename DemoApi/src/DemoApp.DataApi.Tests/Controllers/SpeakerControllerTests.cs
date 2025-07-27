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
    public class SpeakerControllerTests
    {
        private readonly Mock<ISpeakerRepository> _repositoryMock;
        private readonly Mock<ILogger<SpeakerController>> _loggerMock;
        private readonly SpeakerController _controller;

        public SpeakerControllerTests()
        {
            _repositoryMock = new Mock<ISpeakerRepository>();
            _loggerMock = new Mock<ILogger<SpeakerController>>();
            _controller = new SpeakerController(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResult_WithSpeakerList()
        {
            // Arrange
            var speakers = new List<Speaker> { new Speaker(), new Speaker() };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(speakers);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetAsync_ReturnsOkResult_WithSpeaker()
        {
            // Arrange
            var speaker = new Speaker { Id = 1 };
            _repositoryMock.Setup(r => r.GetAsync(1)).ReturnsAsync(speaker);

            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task AddAsync_ReturnsOkResult_WithSpeaker()
        {
            // Arrange
            var speaker = new Speaker { Id = 2 };
            _repositoryMock.Setup(r => r.AddAsync(speaker)).ReturnsAsync(speaker);

            // Act
            var result = await _controller.AddAsync(speaker);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsOkResult_WithSpeaker()
        {
            // Arrange
            var speaker = new Speaker { Id = 3 };
            _repositoryMock.Setup(r => r.UpdateAsync(speaker)).ReturnsAsync(speaker);

            // Act
            var result = await _controller.UpdateAsync(speaker);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsOkResult_WithTrue()
        {
            // Arrange
            _repositoryMock.Setup(r => r.DeleteAsync(4)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteAsync(4);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}