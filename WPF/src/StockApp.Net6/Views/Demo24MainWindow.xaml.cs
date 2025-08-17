using StockApp.Net6.ViewModels;
using System.Windows.Controls;

namespace StockApp.Net6.Views;

/// <summary>
/// Interaction logic for Demo24MainWindow.xaml
/// </summary>
public partial class Demo24MainWindow : Page
{
    public Demo24MainWindow()
    {
        InitializeComponent();
        Demol24ViewModel vm = new Demol24ViewModel();
        DataContext = vm;
    }

}