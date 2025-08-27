using System.Windows.Input;

using StockApp.Net8.State.Navigators;
using StockApp.Net8.ViewModels;
using StockApp.Net8.ViewModels.Factories;
using StockAppApiData.Net8.Services;
using StockService.Net8.Services;

namespace StockApp.Net8.Commands;

public class UpdateCurrentViewModelCommand(INavigator navigator, IMajorIndexService majorIndexService) : ICommand
{
    public event EventHandler CanExecuteChanged;

    private readonly INavigator _navigator = navigator;
    private readonly IMajorIndexService _majorIndexService = majorIndexService;
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
                    _navigator.CurrentViewModel = new HomeViewModel(MajorIndexViewModel.LoadMajorIndexViewModel(_majorIndexService));
                    break;
                case ViewType.Portfolio:
                    _navigator.CurrentViewModel = new PortfolioViewModel();
                    break;
                case ViewType.Buy:
                    _navigator.CurrentViewModel = new BuyViewModel();
                    break;
                case ViewType.Sell:
                    _navigator.CurrentViewModel = new SellViewModel();
                    break;
                case ViewType.Login:
                    _navigator.CurrentViewModel = new LoginViewModel();
                    break;
                default:
                    break;
            }
        }
    }
}
