using System.Windows.Input;

namespace StockApp.Net6.MVVM;

public interface IAsyncCommand : ICommand
{
    IEnumerable<Task> RunningTasks { get; }
    bool CanExecute();
    Task ExecuteAsync();
}
