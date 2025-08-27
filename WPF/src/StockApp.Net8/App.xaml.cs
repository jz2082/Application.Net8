using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Windows;

using StockApp.Net8.ViewModels;
using StockApp.Net8.Views;
using StockAppData.Net8;
using StockAppApiData.Net8;
using StockService.Net8;



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
                .ConfigureServices((context, services) =>
                {
                    services
                        .AddInMemoryDbService(context.Configuration)
                        .AddStockApiDataService(context.Configuration)
                        .AddStockService(context.Configuration)
                        .AddStockAppService(context.Configuration)
                        .AddSingleton<StockTraderWindow>()
                        .AddSingleton<StockAppNavigationWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            //new MajorIndexService().GetMajorIndex(StockService.Net8.Models.MajorIndexType.DowJones).ContinueWith(x =>
            //{
            //    var index = x.Result;
            //});

            // Demo - StockAppNavigationWindow
            //var startupForm = AppHost.Services.GetRequiredService<StockAppNavigationWindow>();

            // Demo - StockTraderWindow
            var startupForm = AppHost.Services.GetRequiredService<StockTraderWindow>();
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
