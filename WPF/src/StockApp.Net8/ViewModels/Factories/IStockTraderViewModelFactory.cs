using StockApp.Net8.MVVM;
using StockApp.Net8.State.Navigators;

namespace StockApp.Net8.ViewModels.Factories;

public interface IStockTraderViewModelFactory
{
    ViewModelBase CreateViewModel(ViewType viewType);
}
