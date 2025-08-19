using System.Windows.Navigation;

namespace StockApp.Net6.Views
{
    /// <summary>
    /// Interaction logic for Demo23NavigationWindow.xaml
    /// </summary>
    public partial class Demo23NavigationWindow : NavigationWindow
    {
        public Demo23NavigationWindow()
        {
            InitializeComponent();

            var startPage = new Demo23MainWindow();

            this.Width = 500;
            this.Height = 350;
            this.NavigationService.Navigate(startPage);
        }
    }
}
