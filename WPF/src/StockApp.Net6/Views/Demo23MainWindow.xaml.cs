using StockApp.Net6.ViewModels;
using System.Windows;

namespace StockApp.Net6.Views;

public partial class Demo23MainWindow : Window
{
    public Demo23MainWindow()
    {
        InitializeComponent();
        Demol23ViewModel vm = new Demol23ViewModel();
        DataContext = vm;
    }

}