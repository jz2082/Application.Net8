using StockApp.Net8.MVVM;

namespace StockApp.Net8.ViewModels;

public class AssetViewModel : ViewModelBase
{
    public string Symbol { get; }
    public int Shares { get; }

    public AssetViewModel(string symbol, int shares)
    {
        Symbol = symbol;
        Shares = shares;
    }
}
