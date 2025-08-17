using Xunit;
using StockApp.Net6.ViewModels;
using StockApp.Net6.Models;

namespace StockApp.Net6.Tests
{
    public class Demol23ViewModelTests
    {
        [Fact]
        public void Constructor_InitializesItemsCorrectly()
        {
            var vm = new Demol23ViewModel();
            Assert.NotNull(vm.Items);
            Assert.Equal(2, vm.Items.Count);

            Assert.Equal("Product 1", vm.Items[0].Name);
            Assert.Equal("0001", vm.Items[0].SerialNumber);
            Assert.Equal(5, vm.Items[0].Quantity);

            Assert.Equal("Product 2", vm.Items[1].Name);
            Assert.Equal("0002", vm.Items[1].SerialNumber);
            Assert.Equal(6, vm.Items[1].Quantity);
        }

        [Fact]
        public void SelectedItem_SetAndGet_Works()
        {
            var vm = new Demol23ViewModel();
            var item = new Item { Name = "Test", SerialNumber = "1234", Quantity = 1 };
            vm.SelectedItem = item;
            Assert.Equal(item, vm.SelectedItem);
        }

        [Fact]
        public void Items_Collection_CanAddItem()
        {
            var vm = new Demol23ViewModel();
            var newItem = new Item { Name = "Product 3", SerialNumber = "0003", Quantity = 7 };
            vm.Items.Add(newItem);
            Assert.Contains(newItem, vm.Items);
            Assert.Equal(3, vm.Items.Count);
        }
    }
}