# Hands-on Projects

This repository contains practical projects for learning and demonstration purposes, including a .NET 8 Web API and an Angular frontend, both with Docker and CI/CD support.

## Projects

### DemoApi - DemoApp.DataApi
A lightweight RESTful API built with .NET 8, featuring:
- Dependency Injection, Entity Framework, In-Memory DB
- Polly for retry/circuit-breaker patterns
- Repository Pattern for decoupled, testable data access
- xUnit tests generated with GitHub Copilot
- HTTPS self-signed certificate support (including Docker)
- CI/CD pipeline (YAML) for Azure App Service deployment  
**Live Demo:** [Swagger UI](https://demodataapinet8.azurewebsites.net/swagger/index.html)

### Angular - DemoApp
A modern Angular 18 frontend application with:
- PrimeNG UI components, RxJS
- Microsoft Entra ID authentication
- Unit tests (*.spec.ts) generated with GitHub Copilot
- HTTPS self-signed certificate support (including Docker)
- CI/CD pipeline (YAML) for Azure App Service deployment  
**Live Demo:** [Dashboard](https://angularapp2025.azurewebsites.net/DemoWebApp/dashboard)

### React - globomantics
A modern React 18 frontend application with:
- Next.js
- HTTPS self-signed certificate support (including Docker)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js & npm](https://nodejs.org/)
- [Angular CLI](https://angular.io/cli)
- [Docker](https://www.docker.com/) (optional)

### Setup

1. **Clone the repository:**
    ```
    git clone https://github.com/jz2082/Application.Net8.git
    cd Application.Net8
    ```

2. **Build and run the API:**
    ```
    cd DemoApi/src/DemoApp.DataApi
    dotnet build
    dotnet run
    ```

3. **Build and run the Angular app:**
    ```
    cd Angular/src/DemoApp
    npm install
    ng serve -o
    ```

### Running Tests

- **.NET API (xUnit):**
    ```
    cd DemoApi/src/DemoApp.DataApi
    dotnet test
    ```
- **Angular (Jasmine/Karma):**
    ```
    cd Angular/src/DemoApp
    ng test
    ```

## Folder Structure

```
/DemoApi      # .NET 8 Web API
/Angular      # Angular frontend
/React        # React frontend
```

## Contributing

Pull requests are welcome!
