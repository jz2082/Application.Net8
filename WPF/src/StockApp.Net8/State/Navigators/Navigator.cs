using StockApp.Net8.Commands;
using StockApp.Net8.MVVM;
using StockService.Net8.Services;
using System.Windows.Input;

namespace StockApp.Net8.State.Navigators;

public class Navigator(IMajorIndexService majorIndexService) : ViewModelBase, INavigator
{
    private ViewModelBase? _currentViewModel;

    private readonly IMajorIndexService _majorIndexService = majorIndexService;

    public ViewModelBase? CurrentViewModel
    {
        get
        {
            return _currentViewModel;
        }
        set
        {
            //_currentViewModel?.Dispose();

            _currentViewModel = value;
            
            OnPropertyChanged();
            //StateChanged?.Invoke();
        }
    }

    //public event Action StateChanged;

    public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this, _majorIndexService);

   
}
