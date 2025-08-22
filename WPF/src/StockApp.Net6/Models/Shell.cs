using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StockApp.Net6.Models;

public class Shell : IShell, INotifyPropertyChanged
{
    private string _statusText;
    public event PropertyChangedEventHandler? PropertyChanged;

    public string StatusText 
    { 
        get => _statusText; 
        set
        {
            _statusText = value;
            OnPropertyChanged();
        }
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
