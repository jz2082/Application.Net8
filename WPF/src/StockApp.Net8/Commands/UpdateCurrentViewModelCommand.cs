using System.Windows.Input;

using StockApp.Net8.State.Navigators;
using StockApp.Net8.ViewModels;
using StockApp.Net8.ViewModels.Factories;

namespace StockApp.Net8.Commands;

public class UpdateCurrentViewModelCommand(INavigator navigator) : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly INavigator _navigator = navigator;
    //private readonly IStockTraderViewModelFactory _viewModelFactory;

    //public UpdateCurrentViewModelCommand(INavigator navigator, IStockTraderViewModelFactory viewModelFactory)
    //{
    //    _navigator = navigator;
    //    _viewModelFactory = viewModelFactory;
    //}

    //public UpdateCurrentViewModelCommand(INavigator navigator)
    //{
    //    _navigator = navigator;
    //    //_viewModelFactory = viewModelFactory;
    //}

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        if (parameter is ViewType)
        {
            ViewType viewType = (ViewType)parameter;

            //_navigator.CurrentViewModel = _viewModelFactory.CreateViewModel(viewType);

            switch (viewType)
            {
                case ViewType.Home:
                    _navigator.CurrentViewModel = new HomeViewModel();
                    break;
                case ViewType.Portfolio:
                    _navigator.CurrentViewModel = new PortfolioViewModel();
                    break;
                default:
                    break;
            }
        }
    }
}
