using System.Collections.ObjectModel;
using System.Windows;

namespace StockApp.Net6.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class Demo13MainWindow : Window
{
    public Demo13MainWindow()
    {
        DataContext = this;
        _entries = new ObservableCollection<string>();
        InitializeComponent();
    }

    private ObservableCollection<string> _entries;

    public ObservableCollection<string> Entries
    {
        get { return _entries; }
        set { _entries = value; }
    }

    private void btnAdd_Click(object sender, RoutedEventArgs e)
    {
        Entries.Add(txtEntry.Text);
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
        string selectedItem = (string)lvEntries.SelectedItem;
        Entries.Remove(selectedItem);
    }

    private void btnClear_Click(object sender, RoutedEventArgs e)
    {
        Entries.Clear();
    }
}