using StockService.Net8.Exceptions;
using StockService.Net8.Models;

namespace StockService.Net8.Services.TransactionServices;

public class BuyStockService(IStockPriceService stockPriceService, IDataService<Account> accountService) : IBuyStockService
{
    private readonly IStockPriceService _stockPriceService = stockPriceService;
    private readonly IDataService<Account> _accountService = accountService;

    public async Task<Account> BuyStock(Account buyer, string symbol, int shares)
    {
        double stockPrice = await _stockPriceService.GetPrice(symbol);

        double transactionPrice = stockPrice * shares;

        if (transactionPrice > buyer.Balance)
        {
            throw new InsufficientFundsException(buyer.Balance, transactionPrice);
        }

        AssetTransaction transaction = new AssetTransaction()
        {
            Account = buyer,
            Asset = new Asset()
            {
                PricePerShare = stockPrice,
                Symbol = symbol
            },
            DateProcessed = DateTime.Now,
            Shares = shares,
            IsPurchase = true
        };

        buyer.AssetTransactions.Add(transaction);
        buyer.Balance -= transactionPrice;

        await _accountService.Update(buyer.Id, buyer);

        return buyer;
    }
}
