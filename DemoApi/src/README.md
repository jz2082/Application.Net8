# Generate cert and configure local machine:

    dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\aspnetapp.pfx -p Dudu1120
    dotnet dev-certs https -ep $env:USERPROFILE\.aspnet\https\aspnetapp.pfx -p Dudu1120
    dotnet dev-certs https --trust

OR

    dotnet dev-certs https -ep C:\Users\Joseph\AppData\Roaming\.aspnet\https\aspnetapp.pfx -p Dudu1120
    dotnet dev-certs https -ep C:\Users\Joseph\AppData\Roaming\.aspnet\https\aspnetapp1.pfx -p Dudu1120

    dotnet dev-certs https
    dotnet dev-certs https --clean
    dotnet dev-certs https --trust

# production API -------------------------------------

    docker compose -f demoapi_compose.yml up -d
    docker compose -f demoapi_compose.yml down

## localhost / 192.168.10.20 / 192.168.1.30

# 1. DemoApp.DataApi:

    https://localhost:44803/swagger/index.html
    https://localhost:44813/swagger/index.html
    https://192.168.1.30:44813/swagger/index.html
    https://192.168.10.20:44813/swagger/index.html
    https://demodataapinet8.azurewebsites.net/swagger/index.html

# 2. Setting
