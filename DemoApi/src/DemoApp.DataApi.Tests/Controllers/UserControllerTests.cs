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
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _repositoryMock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _controller = new UserController(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResult_WithUserList()
        {
            // Arrange
            var users = new List<UserModel> { new UserModel(), new UserModel() };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetAsync_ReturnsOkResult_WithUser()
        {
            // Arrange
            var user = new UserModel { Id = 1 };
            _repositoryMock.Setup(r => r.GetAsync("1")).ReturnsAsync(user);

            // Act
            var result = await _controller.GetAsync("1");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetByUsernameAndPasswordAsync_ReturnsOkResult_WithUser()
        {
            // Arrange
            var user = new UserModel { Name = "test", Password = "pass" };
            _repositoryMock.Setup(r => r.GetByUsernameAndPasswordAsync("test", "pass")).ReturnsAsync(user);

            // Act
            var result = await _controller.GetByUsernameAndPasswordAsync("test", "pass");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }
    }
}