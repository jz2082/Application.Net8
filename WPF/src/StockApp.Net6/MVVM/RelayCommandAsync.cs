using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace StockApp.Net6.MVVM;

public abstract class RelayCommandAsync<T> : ICommandAsync<T>
{
    private readonly ObservableCollection<Task> _runningTasks;

    protected RelayCommandAsync()
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
        return CanExecute((T)parameter);
    }

    async void ICommand.Execute(object? parameter)
    {
        var runningTask = ExecuteAsync((T)parameter);
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

    public abstract bool CanExecute(T parameter);

    public abstract Task ExecuteAsync(T parameter);

    private void OnRunningTasksChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        CommandManager.InvalidateRequerySuggested();
    }
}
