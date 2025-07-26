#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_ENVIRONMENT="Development"
ENV ASPNETCORE_HTTP_PORTS=8080
ENV ASPNETCORE_HTTPS_PORTS=8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DemoApp.DataApi/DemoApp.DataApi.csproj", "DemoApp.DataApi/"]
COPY ["Application.Framework/Application.Framework.csproj", "Application.Framework/"]
COPY ["DemoApp.InMemoryDb.Repository/DemoApp.InMemoryDb.Repository.csproj", "DemoApp.InMemoryDb.Repository/"]
COPY ["InMemoryDb.DataEntity/InMemoryDb.DataEntity.csproj", "InMemoryDb.DataEntity/"]
COPY ["InMemoryDb.Model/InMemoryDb.Model.csproj", "InMemoryDb.Model/"]
RUN dotnet restore "./DemoApp.DataApi/DemoApp.DataApi.csproj"
COPY . .
WORKDIR "/src/DemoApp.DataApi"
RUN dotnet build "./DemoApp.DataApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DemoApp.DataApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoApp.DataApi.dll"]