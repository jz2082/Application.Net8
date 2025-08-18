# StockApp.Net6

StockApp.Net6 is a Windows desktop application built with WPF, targeting .NET 6. It provides a simple interface for managing and viewing stock items, including product details and quantities. The application is designed using the MVVM (Model-View-ViewModel) pattern for clean separation of concerns and maintainability.

## Features

- View a list of stock items with name, serial number, and quantity.
- Select and inspect individual items.
- MVVM architecture for maintainability, testability, and scalability.
- Unit tests using xUnit for core view models.

## MVVM Support

StockApp.Net6 follows the MVVM pattern:

- **Models**: Represent the data structure (e.g., `Item`, `StockData`).
- **ViewModels**: Provide data-binding and business logic (e.g., `Demol23ViewModel`, `StockDataViewModel`). ViewModels implement property change notification for UI updates.
- **Views**: XAML files for UI layout (e.g., `Demo23MainWindow.xaml`). Views bind to ViewModels for dynamic data presentation.

This structure enables easy unit testing and future extension.

## Dependency Injection

install following NGet package
1. Microsoft.Extensions.DependencyInjection V6.0.0
2. Microsoft.Extensions.Hosting V6.0.0
3. Microsoft.Extensions.Logging V6.0.0

## Getting Started

### Prerequisites

- Windows 10 or later
- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- Visual Studio 2022 (recommended)

### Building the Application

1. Clone the repository:


