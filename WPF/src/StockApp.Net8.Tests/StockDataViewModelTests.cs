using System.ComponentModel;
using System.Linq;
using StockApp.Net8.Models;
using StockApp.Net8.ViewModels;

namespace StockApp.Net8.Tests;

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

    [Fact]
    public void GetData_AddsStockDataToCollection()
    {
        var vm = new StockDataViewModel(10, 20);
        var initialCount = vm.View.Cast<object>().Count();
        // Wait for timer to tick and add data
        System.Threading.Thread.Sleep(1100);
        var newCount = vm.View.Cast<object>().Count();
        Assert.True(newCount > initialCount);
    }

    [Fact]
    public void Detail_Property_RaisesPropertyChanged()
    {
        var vm = new StockDataViewModel(10, 20);
        bool eventRaised = false;
        vm.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(vm.Detail))
                eventRaised = true;
        };
        vm.Detail = new StockData { Price = 50, TimeStamp = DateTime.Now, Color = "0" };
        Assert.True(eventRaised);
    }

    [Fact]
    public void View_Collection_ContainsStockDataWithCorrectColor()
    {
        var vm = new StockDataViewModel(10, 20);
        System.Threading.Thread.Sleep(1100);
        var items = vm.View.Cast<StockData>().ToList();
        Assert.All(items, item => Assert.Contains(item.Color, new[] { "0", "1", "2" }));
    }
}