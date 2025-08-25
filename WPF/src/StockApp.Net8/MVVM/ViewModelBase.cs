using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace StockApp.Net8.MVVM;

public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

public class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public virtual void Dispose() { }
}
