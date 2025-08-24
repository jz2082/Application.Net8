using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using StockAppData.Net8;
using StockService.Net8.Models;
using StockService.Net8.Services;

namespace TestConsole.Net8;

public class HostedService(
    IOptions<AppSetting> appSetting, 
    IDataService<User> dataService, 
    IHostApplicationLifetime lifeTime, 
    ILogger<HostedService> logger) : BackgroundService
{
    private readonly AppSetting _appSetting = appSetting.Value;
    private readonly IDataService<User> _dataService = dataService;
    private readonly IHostApplicationLifetime _lifeTime = lifeTime;
    private readonly ILogger<HostedService> _logger = logger;

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        bool quit = false;
        do
        {
            Console.WriteLine();
            Console.WriteLine("AppSetting - - -");
            Console.WriteLine($"AppSetting.Environment: {_appSetting.Environment}");
            Console.WriteLine($"AppSetting.DbConnection: {_appSetting.DbConnection}");
            Console.WriteLine($"AppSetting.CommandTimeout: {_appSetting.CommandTimeout}");
            Console.WriteLine($"AppSetting.AboutMessage: {_appSetting.AboutMessage}");
            Console.WriteLine("End AppSetting - - -");
            Console.WriteLine();
            try
            {
                var commandChar = System.Console.ReadKey().KeyChar;
                Console.WriteLine();
                switch (commandChar)
                {
                    case 't':
                    case 'T':
                        // test crate User
                        await _dataService.Create(new User { Username = "test"});
                        Console.WriteLine($"Count = { (await _dataService.GetAll()).Count()}");
                        break;
                    case 'x':
                    case 'X':
                        quit = true;
                        break;
                    default:
                        Console.WriteLine("Default Application run @" + await Task.FromResult(DateTime.Now));
                        break;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
        }
        while (!quit);
        _lifeTime.StopApplication();
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
    }
}
