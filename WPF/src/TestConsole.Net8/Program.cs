using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Framework.Net8;
using StockAppData.Net8;
using StockService.Net8;
using TestConsole.Net8;

namespace TestConsole.ConsoleApp;

public class Program
{
    public static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        using var tokenSource = new CancellationTokenSource();
        await host.RunAsync(tokenSource.Token);
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .UseContentRoot(Directory.GetCurrentDirectory())
        .ConfigureAppConfiguration((context, configuration) =>
        {
            configuration.Sources.Clear();
            configuration
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .AddCommandLine(args)
                .AddEnvironmentVariables();
        })
        .ConfigureServices((context, services) =>
        {
            services
                .Configure<AppSetting>(context.Configuration.GetSection("Configuration"))
                .AddHostedService<HostedService>()
                .AddInMemoryDbService(context.Configuration)
                .AddStockService(context.Configuration);
        })
        .UseConsoleLifetime();
}
