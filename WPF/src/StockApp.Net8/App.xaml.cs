using StockApp.Net8.Views;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.Extensions.Configuration;

using StockAppData.Net8;
using StockService.Net8;
using StockApp.Net8.ViewModels;

namespace StockApp.Net8
{


    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.Sources.Clear();
                    configuration
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                        .AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services
                        .Configure<AppSetting>(hostContext.Configuration.GetSection("Configuration"))
                        .AddInMemoryDbService()
                        .AddStockService()
                        .AddSingleton<StockTraderWindow>()
                        .AddSingleton<StockAppNavigationWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            // Demo - StockAppNavigationWindow
            //var startupForm = AppHost.Services.GetRequiredService<StockAppNavigationWindow>();

            // Demo - StockTraderWindow
            var startupForm = AppHost.Services.GetRequiredService<StockTraderWindow>();
            startupForm.DataContext = new MainViewModel();
            startupForm.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }

}
