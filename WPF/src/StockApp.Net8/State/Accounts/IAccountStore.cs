using StockService.Net8.Models;

namespace StockApp.Net8.State.Accounts;

public interface IAccountStore
{
    Account CurrentAccount { get; set; }
    event Action StateChanged;
}
