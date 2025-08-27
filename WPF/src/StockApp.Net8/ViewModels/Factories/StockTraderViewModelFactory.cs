using StockApp.Net8.MVVM;
using StockApp.Net8.State.Navigators;

namespace StockApp.Net8.ViewModels.Factories;

public class StockTraderViewModelFactory : IStockTraderViewModelFactory
{
    private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
    private readonly CreateViewModel<PortfolioViewModel> _createPortfolioViewModel;
    private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
    private readonly CreateViewModel<BuyViewModel> _createBuyViewModel;
    private readonly CreateViewModel<SellViewModel> _createSellViewModel;

    public StockTraderViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel,
        CreateViewModel<PortfolioViewModel> createPortfolioViewModel,
        CreateViewModel<LoginViewModel> createLoginViewModel,
        CreateViewModel<BuyViewModel> createBuyViewModel,
        CreateViewModel<SellViewModel> createSellViewModel)
    {
        _createHomeViewModel = createHomeViewModel;
        _createPortfolioViewModel = createPortfolioViewModel;
        _createLoginViewModel = createLoginViewModel;
        _createBuyViewModel = createBuyViewModel;
        _createSellViewModel = createSellViewModel;
    }

    public ViewModelBase CreateViewModel(ViewType viewType)
    {
        return new ViewModelBase();
        switch (viewType)
        {
            case ViewType.Login:
                return _createLoginViewModel();
            case ViewType.Home:
                return _createHomeViewModel();
            case ViewType.Portfolio:
                return _createPortfolioViewModel();
            case ViewType.Buy:
                return _createBuyViewModel();
            case ViewType.Sell:
                return _createSellViewModel();
            default:
                throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
        }
    }
}
