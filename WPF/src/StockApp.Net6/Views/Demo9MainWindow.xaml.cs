using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace StockApp.Net6.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class Demo9MainWindow : Window
{
    public Demo9MainWindow()
    {
        InitializeComponent();
    }

    private void btnFire_Click(object sender, RoutedEventArgs e)
    {
        //MessageBox.Show("Your message here.", "ERROR!", MessageBoxButton.OK, MessageBoxImage.Error);
        //MessageBoxResult result = MessageBox.Show("Your message here.", "ERROR!", 
        //    MessageBoxButton.OK, MessageBoxImage.Error);
        MessageBoxResult result = MessageBox.Show("Do you agree?.", "Agreement",
           MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (result == MessageBoxResult.Yes)
        {
            tbInfo.Text = "Agreed";
        }
        else
        {
            tbInfo.Text = "Not Agreed";
        }
    }

   
}