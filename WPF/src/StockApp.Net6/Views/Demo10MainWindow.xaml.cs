using Microsoft.Win32;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace StockApp.Net6.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class Demo10MainWindow : Window
{
    public Demo10MainWindow()
    {
        InitializeComponent();
    }

    private void btnFire_Click(object sender, RoutedEventArgs e)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = "c# Source Files | *.cs";
        //ofd.InitialDirectory = "c:\\temp";
        ofd.Title = "Please pick a CS Source file ...";

        bool? success = ofd.ShowDialog();
        if (success == true)
        {
            string path = ofd.FileName;
            string filename = ofd.SafeFileName;

            tbInfo.Text = filename;
        }
        else
        {
            // didnot pick anything
        }
    }

   
}