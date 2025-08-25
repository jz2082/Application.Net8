using StockApp.Net8.State.Accounts;
using StockService.Net8.Models;
using StockService.Net8.Services.AuthenticationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Net8.State.Authenticators;

public class Authenticator : IAuthenticator
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IAccountStore _accountStore;

    public Authenticator(IAuthenticationService authenticationService, IAccountStore accountStore)
    {
        _authenticationService = authenticationService;
        _accountStore = accountStore;
    }

    public Account CurrentAccount
    {
        get
        {
            return _accountStore.CurrentAccount;
        }
        private set
        {
            _accountStore.CurrentAccount = value;
            StateChanged?.Invoke();
        }
    }

    public bool IsLoggedIn => CurrentAccount != null;

    public event Action StateChanged;

    public async Task Login(string username, string password)
    {
        CurrentAccount = await _authenticationService.Login(username, password);
    }

    public void Logout()
    {
        CurrentAccount = null;
    }

    public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword)
    {
        return await _authenticationService.Register(email, username, password, confirmPassword);
    }
}
