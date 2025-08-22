using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace StockApp.Net6.MVVM;

public abstract class AsyncCommand : IAsyncCommand
{
    private readonly ObservableCollection<Task> _runningTasks;

    protected AsyncCommand()
    {
        _runningTasks = new ObservableCollection<Task>();
        _runningTasks.CollectionChanged += OnRunningTasksChanged;
    }
    public IEnumerable<Task> RunningTasks => _runningTasks;

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    bool ICommand.CanExecute(object? parameter)
    {
        return CanExecute();
    }

    async void ICommand.Execute(object? parameter)
    {
        var runningTask = ExecuteAsync();
        _runningTasks.Add(runningTask);
        try
        {
            await runningTask;
        } 
        finally 
        {
            _runningTasks.Remove(runningTask);
        }
    }

    public abstract bool CanExecute();

    public abstract Task ExecuteAsync();

    private void OnRunningTasksChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        CommandManager.InvalidateRequerySuggested();
    }
}
