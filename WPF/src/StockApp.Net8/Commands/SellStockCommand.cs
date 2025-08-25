using System.ComponentModel;

using StockApp.Net8.State.Accounts;
using StockApp.Net8.ViewModels;
using StockService.Net8.Exceptions;
using StockService.Net8.Models;
using StockService.Net8.Services.TransactionServices;

namespace StockApp.Net8.Commands;

public class SellStockCommand : AsyncCommandBase
{
    private readonly SellViewModel _viewModel;
    private readonly ISellStockService _sellStockService;
    private readonly IAccountStore _accountStore;

    public SellStockCommand(SellViewModel viewModel, ISellStockService sellStockService, IAccountStore accountStore)
    {
        _viewModel = viewModel;
        _sellStockService = sellStockService;
        _accountStore = accountStore;

        _viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    public override bool CanExecute(object parameter)
    {
        return _viewModel.CanSellStock && base.CanExecute(parameter);
    }

    public override async Task ExecuteAsync(object parameter)
    {
        _viewModel.StatusMessage = string.Empty;
        _viewModel.ErrorMessage = string.Empty;

        try
        {
            string symbol = _viewModel.Symbol;
            int shares = _viewModel.SharesToSell;
            Account account = await _sellStockService.SellStock(_accountStore.CurrentAccount, symbol, shares);

            _accountStore.CurrentAccount = account;

            _viewModel.SearchResultSymbol = string.Empty;
            _viewModel.StatusMessage = $"Successfully sold {shares} shares of {symbol}.";
        }
        catch (InsufficientSharesException ex)
        {
            _viewModel.ErrorMessage = $"Account has insufficient shares. You only have {ex.AccountShares} shares.";
        }
        catch (InvalidSymbolException)
        {
            _viewModel.ErrorMessage = "Symbol does not exist.";
        }
        catch (Exception)
        {
            _viewModel.ErrorMessage = "Transaction failed.";
        }
    }

    private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SellViewModel.CanSellStock))
        {
            OnCanExecuteChanged();
        }
    }
}
