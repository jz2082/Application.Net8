using Microsoft.Extensions.Options;
using System.Windows;

using Framework.Net8;
using StockApp.Net8.ViewModels;

namespace StockApp.Net8.Views
{
    /// <summary>
    /// Interaction logic for StockTraderWindow.xaml
    /// </summary>
    public partial class StockTraderWindow : Window
    {
        private readonly AppSetting _appSetting;
        private readonly MainViewModel _mainViewModel;

        public StockTraderWindow(IOptions<AppSetting> appSetting, MainViewModel mainViewModel)
        {
            _appSetting = appSetting.Value;
            _mainViewModel = mainViewModel;

            InitializeComponent();
            DataContext = _mainViewModel;
        }
    }
}
