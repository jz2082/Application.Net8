using StockService.Net8.Models;

namespace StockApp.Net8.State.Accounts;

public class AccountStore : IAccountStore
{
    private Account _currentAccount;
    public Account CurrentAccount
    {
        get
        {
            return _currentAccount;
        }
        set
        {
            _currentAccount = value;
            StateChanged?.Invoke();
        }
    }

    public event Action StateChanged;

}
