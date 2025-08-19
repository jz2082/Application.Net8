using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace StockApp.Net6.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class Demo8MainWindow : Window, INotifyPropertyChanged
{
    public Demo8MainWindow()
    {
        DataContext = this;
        InitializeComponent();
    }

	private string _boundText;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string BoundText
	{
		get { return _boundText; }
		set 
		{ 
			_boundText = value;
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BoundText"));
            OnPropertyChanged();
        }
    }

    private void btnSet_Click(object sender, RoutedEventArgs e)
    {
        BoundText = "set from code";
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}