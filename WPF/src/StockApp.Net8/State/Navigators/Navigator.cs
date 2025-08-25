using StockApp.Net8.Commands;
using StockApp.Net8.MVVM;
using System.Windows.Input;

namespace StockApp.Net8.State.Navigators;

public class Navigator : ViewModelBase, INavigator
{
    private ViewModelBase _currentViewModel;

    public ViewModelBase CurrentViewModel
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

    public event Action StateChanged;

    public ICommand UpdateCurrentViewModelCommand => new UpdateCurrentViewModelCommand(this);

   
}
