using System.Windows.Input;

using StockApp.Net8.MVVM;
using StockApp.Net8.State.Authenticators;
using StockApp.Net8.State.Navigators;
using StockApp.Net8.ViewModels.Factories;
using StockApp.Net8.Commands;

namespace StockApp.Net8.ViewModels;

public class MainViewModel : ViewModelBase
{
    public INavigator Navigator { get; set; } = new Navigator();

    //private readonly IStockTraderViewModelFactory _viewModelFactory;
    //private readonly INavigator _navigator;
    //private readonly IAuthenticator _authenticator;

    //public bool IsLoggedIn => _authenticator.IsLoggedIn;
    //public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;

    //public ICommand UpdateCurrentViewModelCommand { get; }

    //public MainViewModel(INavigator navigator, IStockTraderViewModelFactory viewModelFactory, IAuthenticator authenticator)
    //{
    //    _navigator = navigator;
    //    _viewModelFactory = viewModelFactory;
    //    _authenticator = authenticator;

    //    _navigator.StateChanged += Navigator_StateChanged;
    //    _authenticator.StateChanged += Authenticator_StateChanged;

    //    UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
    //    UpdateCurrentViewModelCommand.Execute(ViewType.Login);
    //}

    //private void Authenticator_StateChanged()
    //{
    //    OnPropertyChanged(nameof(IsLoggedIn));
    //}

    //private void Navigator_StateChanged()
    //{
    //    OnPropertyChanged(nameof(CurrentViewModel));
    //}

    //public override void Dispose()
    //{
    //    _navigator.StateChanged -= Navigator_StateChanged;
    //    _authenticator.StateChanged -= Authenticator_StateChanged;

    //    base.Dispose();
    //}
}
