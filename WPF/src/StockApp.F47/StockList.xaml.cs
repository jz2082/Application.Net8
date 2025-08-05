using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockApp
{
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
}
