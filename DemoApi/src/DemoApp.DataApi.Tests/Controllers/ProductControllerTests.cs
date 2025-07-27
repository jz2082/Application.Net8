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
    public class ProductControllerTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly Mock<ILogger<ProductController>> _loggerMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _repositoryMock = new Mock<IProductRepository>();
            _loggerMock = new Mock<ILogger<ProductController>>();
            _controller = new ProductController(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkResult_WithProductList()
        {
            // Arrange
            var products = new List<Product> { new Product(), new Product() };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAllAsync();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetAsync_ReturnsOkResult_WithProduct()
        {
            // Arrange
            var product = new Product { ProductId = 1 };
            _repositoryMock.Setup(r => r.GetAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _controller.GetAsync(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task AddAsync_ReturnsOkResult_WithProduct()
        {
            // Arrange
            var product = new Product { ProductId = 2 };
            _repositoryMock.Setup(r => r.AddAsync(product)).ReturnsAsync(product);

            // Act
            var result = await _controller.AddAsync(product);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsOkResult_WithProduct()
        {
            // Arrange
            var product = new Product { ProductId = 3 };
            _repositoryMock.Setup(r => r.UpdateAsync(product)).ReturnsAsync(product);

            // Act
            var result = await _controller.UpdateAsync(product);

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