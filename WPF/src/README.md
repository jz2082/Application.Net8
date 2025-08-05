# StockApp

StockApp is a Windows desktop application targeting .NET Framework 4.7.2.  
This project is configured for WinForms or WPF (ProjectTypeGuids indicate desktop UI).

## Features

- Built for AnyCPU platform
- Output type: Windows executable (`WinExe`)
- Auto-generates binding redirects for assemblies
- Deterministic builds for reproducibility
- Supports ClickOnce publishing (see `PublishUrl` and related settings)
- Configurable update intervals (currently disabled)

## Getting Started

1. **Requirements**
   - Visual Studio 2017 or newer
   - .NET Framework 4.7.2

2. **Build**
   - Open `StockApp.f47/StockApp.csproj` in Visual Studio.
   - Build the solution (`Ctrl+Shift+B`).

3. **Run**
   - Press `F5` to start debugging, or run the generated `.exe` from the output directory.

## Project Structure

- `StockApp.csproj`: Project configuration file
- Source code files: Located in the same directory

## Publishing

- ClickOnce publishing is enabled.
- Output directory: `C:\Works\Release\20230518\StockApp\`

## Configuration

- Default configuration: Debug, AnyCPU
- Application version: `1.0.0.*`

## License

Specify your license here.

---

*Generated from project settings in `StockApp.csproj`.*