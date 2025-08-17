using System.Windows.Controls;
using System.Xml;

using StockApp.Net6.ViewModels;

namespace StockApp.Net6.Views;

/// <summary>
/// Interaction logic for StockDataList.xaml
/// </summary>
public partial class StockDataList : Page
{
    public StockDataList()
    {
        InitializeComponent();
    }

    public StockDataList(object data) : this()
    {
        // Bind to stock data.
        var stocElementk = data as XmlElement;
        var minPrice = Convert.ToInt16(stocElementk.Attributes[1].Value);
        var maxPrice = Convert.ToInt16(stocElementk.Attributes[2].Value);
        this.DataContext = data;
        this.dataGridStockData.ItemsSource = new StockDataViewModel(minPrice, maxPrice).View;
    }
}
