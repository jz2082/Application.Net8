# StockApp.Net8

StockApp.Net8 is a Windows desktop application for monitoring and visualizing stock data, built with .NET 8. It provides real-time updates and is designed with full MVVM (Model-View-ViewModel) support for maintainability and testability.

## Features

- Real-time stock data simulation and visualization
- MVVM architecture for clean separation of concerns
- Uses `CollectionViewSource` and `ObservableCollection` for dynamic data updates
- Property change notifications for UI binding via `INotifyPropertyChanged`
- ViewModel logic separated from UI for easier testing and maintenance
- Unit tests with xUnit

## MVVM Support

- **Models:** Represent stock data (`StockData`) and other domain entities.
- **ViewModels:** Encapsulate application logic and state (`StockDataViewModel`), expose bindable properties, and handle data updates.
- **Views:** Bind to ViewModel properties and collections for automatic UI updates.
- **Property Change Notification:** All ViewModels inherit from a base class implementing `INotifyPropertyChanged` for robust data binding.
- **Collection Binding:** Uses `CollectionViewSource` and `ObservableCollection` to provide live-updating collections to the UI.
- **Testability:** ViewModels are unit tested independently of the UI.

## Requirements

- Windows 10 or later
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or later
