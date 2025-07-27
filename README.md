# Hands on Projects

This repository contains practical projects for learning and demonstration purposes.

## Projects

- DemoApi - DemoApp.DataApi: .NET 8 Web API with in-memory database and Docker support



- Angular - DemoApp: Frontend Angular application with Docker support



## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [Docker](https://www.docker.com/) (optional)

### Setup

1. Clone the repository:
    ```
    git clone https://github.com/jz2082/Application.Net8.git
    cd Application.Net8
    ```

2. Build and run the API:
    ```
    cd DemoApi/src/DemoApp.DataApi
    dotnet build
    dotnet run
    ```

3. Build and run the Angular app:
    ```
    cd Angular/src/DemoApp
    npm install
    ng serve -o
    ```

### Running Tests

- .NET API: DemoApp.DataApi:  
  ```
  cd DemoApi/src/DemoApp.DataApi
  dotnet test (xUnit unit testing framework for .Net and is commonly used with TDD)
  ```

- Angular: DemoApp   
  ```
  cd Angular/src/DemoApp 
  ng test (Jasmine is a behavior-driven development (BDD) framework for testing JavaScript code)
  ```

## Folder Structure

/DemoApi      # .NET 8 Web API
/Angular      # Angular frontend

## Contributing

Pull requests are welcome!
