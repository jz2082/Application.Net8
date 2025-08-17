using System.Collections.ObjectModel;

using StockApp.Net6.Models;
using StockApp.Net6.MVVM;

namespace StockApp.Net6.ViewModels;

internal class Demol23ViewModel : ViewModelBase
{
    public ObservableCollection<Item> Items { get; set; }

    public Demol23ViewModel()
    {
        Items = new ObservableCollection<Item>();
        Items.Add(new Item
        {
            Name = "Product 1",
            SerialNumber = "0001",
            Quantity = 5
        });
        Items.Add(new Item
        {
            Name = "Product 2",
            SerialNumber = "0002",
            Quantity = 6
        });
    }

    private Item selectedItem;

    public Item SelectedItem
    {
        get { return selectedItem; }
        set
        {
            selectedItem = value;
            OnPropertyChanged();
        }
    }

   

}
