#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build

WORKDIR /src
COPY ["PortfolioSiteExample.UnitTests/PortfolioSiteExample.UnitTests.csproj", "PortfolioSiteExample.UnitTests/"]
RUN dotnet restore "PortfolioSiteExample.UnitTests/PortfolioSiteExample.UnitTests.csproj"
COPY . .
WORKDIR "/src/PortfolioSiteExample.UnitTests"
RUN dotnet test "PortfolioSiteExample.UnitTests.csproj"

WORKDIR /src
COPY ["PortfolioSiteExample.Api/PortfolioSiteExample.Api.csproj", "PortfolioSiteExample.Api/"]
RUN dotnet restore "PortfolioSiteExample.Api/PortfolioSiteExample.Api.csproj"
COPY . .
WORKDIR "/src/PortfolioSiteExample.Api"
RUN dotnet build "PortfolioSiteExample.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PortfolioSiteExample.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PortfolioSiteExample.Api.dll"]