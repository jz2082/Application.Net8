using StockApp.Net8.MVVM;
using System.Windows.Input;

namespace StockApp.Net8.State.Navigators;

public interface INavigator
{
    ViewModelBase CurrentViewModel { get; set; }
    
    //event Action StateChanged;

    ICommand UpdateCurrentViewModelCommand { get; }
}
