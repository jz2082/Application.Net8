using System.Collections.Generic;
using System.Threading.Tasks;
using DemoApp.DataApi.Controllers;
using Application.Framework.Logging;
using Application.Framework;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DemoApp.DataApi.Tests.Controllers
{
    public class LoggerInfoInMemoryControllerTests
    {
        private readonly Mock<BaseEntityRepository<LoggerInfoEntity>> _repositoryMock;
        private readonly Mock<ILogger<LoggerInfoInMemoryController>> _loggerMock;
        private readonly LoggerInfoInMemoryController _controller;

        public LoggerInfoInMemoryControllerTests()
        {
            _repositoryMock = new Mock<BaseEntityRepository<LoggerInfoEntity>>();
            _loggerMock = new Mock<ILogger<LoggerInfoInMemoryController>>();
            _controller = new LoggerInfoInMemoryController(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetEntityListAsync_ReturnsOkResult_WithLoggerInfoList()
        {
            // Arrange
            var entities = new List<LoggerInfoEntity> { new LoggerInfoEntity(), new LoggerInfoEntity() };
            _repositoryMock.Setup(r => r.GetEntityListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(entities);

            // Act
            var result = await _controller.GetEntityListAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetEntityAsync_ReturnsOkResult_WithLoggerInfo()
        {
            // Arrange
            var entity = new LoggerInfoEntity { Id = Guid.NewGuid() };
            _repositoryMock.Setup(r => r.GetByIdAsync("abc", It.IsAny<CancellationToken>())).ReturnsAsync(entity);

            // Act
            var result = await _controller.GetEntityAsync(entity.Id.ToString());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}