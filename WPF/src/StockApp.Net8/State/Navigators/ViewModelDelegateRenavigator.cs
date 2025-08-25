using StockApp.Net8.MVVM;
using StockApp.Net8.ViewModels;
using StockApp.Net8.ViewModels.Factories;

namespace StockApp.Net8.State.Navigators;

public class ViewModelDelegateRenavigator<TViewModel> : IRenavigator where TViewModel : ViewModelBase
{
    private readonly INavigator _navigator;
    //private readonly CreateViewModel<TViewModel> _createViewModel;

    public ViewModelDelegateRenavigator(INavigator navigator)
    {
        _navigator = navigator;
        //_createViewModel = createViewModel;
    }

    //public ViewModelDelegateRenavigator(INavigator navigator, CreateViewModel<TViewModel> createViewModel)
    //{
    //    _navigator = navigator;
    //    _createViewModel = createViewModel;
    //}

    public void Renavigate()
    {
        //_navigator.CurrentViewModel = _createViewModel();
    }
}
