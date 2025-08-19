using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

using StockApp.Net6.Views;
using StockApp.Net6.Utilitis;

namespace StockApp.Net6;

public partial class App : Application
{ 
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<StockAppNavigationWindow>();
                services.AddSingleton<Demo6MainWindow>();
                services.AddSingleton<Demo7MainWindow>();
                services.AddSingleton<Demo8MainWindow>();
                services.AddSingleton<Demo9MainWindow>();
                services.AddSingleton<Demo10MainWindow>();
                services.AddSingleton<Demo12MainWindow>();
                services.AddSingleton<Demo13MainWindow>();
                services.AddSingleton<Demo20MainWindow>();
                services.AddSingleton<Demo21MainWindow>();
                services.AddSingleton<Demo23MainWindow>();
                services.AddSingleton<Demo24MainWindow>();
                services.AddWindowFactory<StockApp.Net6.Views.MainWindow>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        // Demo - MainWindow
        //var startupForm = AppHost.Services.GetRequiredService<MainWindow>();

        // Demo - StockAppNavigationWindow
        var startupForm = AppHost.Services.GetRequiredService<StockAppNavigationWindow>();

        // Demo6 - Custom User Controls
        //var startupForm = AppHost.Services.GetRequiredService<Demo6MainWindow>();

        // Demo7 - Custom Textbox Control
        //var startupForm = AppHost.Services.GetRequiredService<Demo7MainWindow>();

        // Demo8 - Data Bindings using INotifyPropertyChanged
        //var startupForm = AppHost.Services.GetRequiredService<Demo8MainWindow

        // Demo9 -  MessageBox
        //var startupForm = AppHost.Services.GetRequiredService<Demo9MainWindow>();

        // Demo10 - OpenFileDialog (File Picker)
        //var startupForm = AppHost.Services.GetRequiredService<Demo10MainWindow>();

        // Demo12 - ListView
        //var startupForm = AppHost.Services.GetRequiredService<Demo12MainWindow>();

        // Demo13 - Observable Collection with ListView
        //var startupForm = AppHost.Services.GetRequiredService<Demo13MainWindow>();

        // Demo20 - Reusable Style Resources
        //var startupForm = AppHost.Services.GetRequiredService<Demo20MainWindow>();

        // Demo21 - Styles and ControlTemplates
        //var startupForm = AppHost.Services.GetRequiredService<Demo21MainWindow>();

        // Demo23 - Navigation - Using ViewModels in MVVM
        //var startupForm = AppHost.Services.GetRequiredService<Demo23NavigationWindow>();

        // Demo23 - Using ViewModels in MVVM
        //var startupForm = AppHost.Services.GetRequiredService<Demo23MainWindow>();

        // Demo24 - Using RelayCommand in MVVM
        //var startupForm = AppHost.Services.GetRequiredService<Demo24MainWindow>();

        startupForm.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}
