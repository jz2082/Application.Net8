using StockApp.Net6.Models;

namespace StockApp.Net6.ViewModels;

public class ShellViewModel
{
    public ShellViewModel(IShell shell)
    {
        Shell = shell;
        Menu = new ShellMenuViewModel(shell);
    }
    
    public IShell Shell 
    { 
        get; 
    }

    public ShellMenuViewModel Menu
    { 
        get; 
    }
}
