using StockApp.Net6.Utilitis;
using StockApp.Net6.ViewModels;
using System.Windows;

namespace StockApp.Net6.Views;

/// <summary>
/// Interaction logic for Demo24MainWindow.xaml
/// </summary>
public partial class Demo24MainWindow : Window
{
    private readonly IAbstractFactory<Demo23MainWindow> _factory;

    public Demo24MainWindow(IAbstractFactory<Demo23MainWindow> factory)
    {
        InitializeComponent();
        var vm = new Demol24ViewModel();
        DataContext = vm;
        _factory = factory;
        // testing
        _factory.Create().Show();
    }

    //private void openChildWindow_Click(object sender, RoutedEventArgs e)
    //{
    //    _factory.Create().Show();
    //}
}