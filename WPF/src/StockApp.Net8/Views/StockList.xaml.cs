using System.Windows.Controls;
using System.Windows.Input;

namespace StockApp.Net8.Views;

/// <summary>
/// Interaction logic for StockList.xaml
/// </summary>
public partial class StockList : Page
{
    public StockList()
    {
        InitializeComponent();
    }

    private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        // View Stock Data List
        StockDataList stockDataList = new StockDataList(this.stockListBox.SelectedItem);
        this.NavigationService.Navigate(stockDataList);
    }

}
