using System.Collections.ObjectModel;

using StockApp.Net6.Models;
using StockApp.Net6.MVVM;

namespace StockApp.Net6.ViewModels;

public class Demol24ViewModel : ViewModelBase
{
    public ObservableCollection<Item> Items { get; set; }

    //public RelayCommand AddCommand => new RelayCommand(execute => { }, canExecute => { return true; });
    public RelayCommand AddCommand => new RelayCommand(execute => AddItem());
    public RelayCommand DeleteCommand => new RelayCommand(execute => DeleteItem(), canExecute => SelectedItem != null);
    public RelayCommand SaveCommand => new RelayCommand(execute => Save(), canExecute => CanSave());

    public Demol24ViewModel()
    {
        Items = new ObservableCollection<Item>();
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

   private void AddItem()
    {
        Items.Add(new Item
        {
            Name = "New Item",
            SerialNumber = "XXXXX",
            Quantity = 0
        });
    }

    private void DeleteItem()
    {
        Items.Remove(selectedItem);
    }

    private void Save()
    {
        // Save to file/database
    }

    private bool CanSave()
    {
        return true;
    }
}
