using StockApp.Net6.Models;
using System;
using System.Collections;
using System.Windows;

namespace StockApp.Net6.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class Demo12MainWindow : Window
{
    public Demo12MainWindow()
    {
        InitializeComponent();
    }

    
    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        lvEntries.Items.Add(txtEntry.Text);
        txtEntry.Clear();
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        //int index = lvEntries.SelectedIndex;
        //lvEntries.Items.RemoveAt(index);

        object item = lvEntries.SelectedItem;
        var result = MessageBox.Show($"Are you sure you want to delete: {(string)item}?", "Sure?", MessageBoxButton.YesNo);
        if (result == MessageBoxResult.Yes)
        {
            lvEntries.Items.Remove(item);
        }

        //var items = lvEntries.SelectedItems;
        //var result = MessageBox.Show($"Are you sure you want to delete {items.Count} items?", "Sure?", MessageBoxButton.YesNo);
        //if (result == MessageBoxResult.Yes)
        //{
        //    var itemList = new ArrayList(items);
        //    foreach (var item in itemList)
        //    {
        //        lvEntries.Items.Remove(item);
        //    }
        //}
    }

    private void btnClear_Click(object sender, RoutedEventArgs e)
    {
        lvEntries.Items.Clear();
    }
}