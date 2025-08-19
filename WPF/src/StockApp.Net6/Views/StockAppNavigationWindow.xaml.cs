using System.Windows.Navigation;

namespace StockApp.Net6.Views;

/// <summary>
/// Interaction logic for StockAppNavigationWindow.xaml
/// </summary>
public partial class StockAppNavigationWindow : NavigationWindow
{
    public StockAppNavigationWindow()
    {
        var startPage = new StockList();

        this.Width = 500; 
        this.Height = 350; 
        this.NavigationService.Navigate(startPage);
    }
}