using StockApp.Net6.Models;
using StockApp.Net6.MVVM;


namespace StockApp.Net6.Commands;

public class OpenMenuCommand : AsyncCommand
{
    private readonly IShell _shell;

    public OpenMenuCommand(IShell shell)
    {
        _shell = shell;
    }

    public override bool CanExecute()
    {
        return RunningTasks.Count() == 0;
    }

    public override async Task ExecuteAsync()
    {
        _shell.StatusText = $"Running task {RunningTasks.Count()} of {RunningTasks.Count()}";

        await Task.Delay(2000);

        _shell.StatusText = $"Completed task {RunningTasks.Count()} of {RunningTasks.Count()}";
    }
}
