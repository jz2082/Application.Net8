using StockApp.Net8.MVVM;

namespace StockApp.Net8.ViewModels;

public class MessageViewModel : ViewModelBase
{
    private string _message;
    public string Message
    {
        get
        {
            return _message;
        }
        set
        {
            _message = value;
            OnPropertyChanged(nameof(Message));
            OnPropertyChanged(nameof(HasMessage));
        }
    }

    public bool HasMessage => !string.IsNullOrEmpty(Message);
}
