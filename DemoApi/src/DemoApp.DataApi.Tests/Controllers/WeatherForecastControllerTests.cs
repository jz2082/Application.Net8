using System;
using System.Collections.Generic;
using System.Linq;
using Demo.DataApi.Controllers;
using Microsoft.Extensions.Logging;
using Xunit;

namespace DemoApp.DataApi.Tests.Controllers
{
    public class WeatherForecastControllerTests
    {
        private readonly WeatherForecastController _controller;

        public WeatherForecastControllerTests()
        {
            var logger = new LoggerFactory().CreateLogger<WeatherForecastController>();
            _controller = new WeatherForecastController(logger);
        }

        [Fact]
        public void Get_ReturnsFiveForecasts()
        {
            // Act
            var result = _controller.Get();

            // Assert
            Assert.NotNull(result);
            var forecasts = result.ToList();
            Assert.Equal(5, forecasts.Count);
        }

        [Fact]
        public void Get_ForecastsHaveValidProperties()
        {
            // Act
            var result = _controller.Get();

            // Assert
            foreach (var forecast in result)
            {
                Assert.InRange(forecast.TemperatureC, -20, 55);
                Assert.False(string.IsNullOrEmpty(forecast.Summary));
                Assert.True(forecast.Date > DateOnly.FromDateTime(DateTime.Now));
            }
        }
    }
}