using StockApp.Net6.Commands;
using StockApp.Net6.Models;
using StockApp.Net6.MVVM;

namespace StockApp.Net6.ViewModels;

public class ShellMenuViewModel
{
    public ShellMenuViewModel(IShell shell)
    {
        OpenCommand = new OpenMenuCommand(shell);
    }

    public IAsyncCommand OpenCommand
    { 
        get; 
    }
}
