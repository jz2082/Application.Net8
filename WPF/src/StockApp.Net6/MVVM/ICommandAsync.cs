using System.Windows.Input;

namespace StockApp.Net6.MVVM;

public interface ICommandAsync<in T> : ICommand
{
    IEnumerable<Task> RunningTasks { get; }
    bool CanExecute(T parameter);
    Task ExecuteAsync(T parameter);
}
