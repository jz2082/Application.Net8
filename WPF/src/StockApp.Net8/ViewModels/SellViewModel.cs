using System.Windows.Input;

using StockApp.Net8.MVVM;
using StockService.Net8.Services;
using StockService.Net8.Services.TransactionServices;
using StockApp.Net8.State.Accounts;
using StockApp.Net8.Commands;
using StockApp.Net8.State.Assets;

namespace StockApp.Net8.ViewModels;

public class SellViewModel : ViewModelBase, ISearchSymbolViewModel
{
    public AssetListingViewModel AssetListingViewModel { get; }

    private AssetViewModel _selectedAsset;
    public AssetViewModel SelectedAsset
    {
        get
        {
            return _selectedAsset;
        }
        set
        {
            _selectedAsset = value;
            OnPropertyChanged(nameof(SelectedAsset));
            OnPropertyChanged(nameof(Symbol));
            OnPropertyChanged(nameof(CanSearchSymbol));
        }
    }

    public string Symbol => SelectedAsset?.Symbol;

    public bool CanSearchSymbol => !string.IsNullOrEmpty(Symbol);

    private string _searchResultSymbol = string.Empty;

    public string SearchResultSymbol
    {
        get
        {
            return _searchResultSymbol;
        }
        set
        {
            _searchResultSymbol = value;
            OnPropertyChanged();
        }
    }

    private double _stockPrice;

    public double StockPrice
    {
        get
        {
            return _stockPrice;
        }
        set
        {
            _stockPrice = value;
            OnPropertyChanged(nameof(StockPrice));
            OnPropertyChanged(nameof(TotalPrice));
        }
    }

    private int _sharesToSell;
    public int SharesToSell
    {
        get
        {
            return _sharesToSell;
        }
        set
        {
            _sharesToSell = value;
            OnPropertyChanged(nameof(SharesToSell));
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(CanSellStock));
        }
    }

    public bool CanSellStock => SharesToSell > 0;

    public double TotalPrice => SharesToSell * StockPrice;

    public MessageViewModel ErrorMessageViewModel { get; }

    public string ErrorMessage
    {
        set => ErrorMessageViewModel.Message = value;
    }

    public MessageViewModel StatusMessageViewModel { get; }

    public string StatusMessage
    {
        set => StatusMessageViewModel.Message = value;
    }

    public ICommand SearchSymbolCommand { get; }
    public ICommand SellStockCommand { get; }

    public SellViewModel(AssetStore assetStore,
        IStockPriceService stockPriceService,
        IAccountStore accountStore,
        ISellStockService sellStockService)
    {
        AssetListingViewModel = new AssetListingViewModel(assetStore);

        SearchSymbolCommand = new SearchSymbolCommand(this, stockPriceService);
        SellStockCommand = new SellStockCommand(this, sellStockService, accountStore);

        ErrorMessageViewModel = new MessageViewModel();
        StatusMessageViewModel = new MessageViewModel();
    }

    public override void Dispose()
    {
        AssetListingViewModel.Dispose();
        ErrorMessageViewModel.Dispose();
        StatusMessageViewModel.Dispose();

        base.Dispose();
    }
}
