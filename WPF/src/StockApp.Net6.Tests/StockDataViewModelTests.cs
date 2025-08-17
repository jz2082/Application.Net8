using Xunit;

using StockApp.Net6.ViewModels;
using StockApp.Net6.Models;
using System;

namespace StockApp.Net6.Tests
{
    public class StockDataViewModelTests
    {
        [Fact]
        public void Constructor_InitializesValuesCorrectly()
        {
            var vm = new StockDataViewModel(10, 20);
            Assert.NotNull(vm);
            Assert.NotNull(vm.View);
        }

        [Fact]
        public void Detail_Property_SetAndGet_Works()
        {
            var vm = new StockDataViewModel(10, 20);
            var stock = new StockData { Price = 100, TimeStamp = DateTime.Now, Color = "1" };
            vm.Detail = stock;
            Assert.Equal(stock, vm.Detail);
        }

        [Fact]
        public void View_Collection_IsNotNull()
        {
            var vm = new StockDataViewModel(10, 20);
            Assert.NotNull(vm.View);
        }
    }
}