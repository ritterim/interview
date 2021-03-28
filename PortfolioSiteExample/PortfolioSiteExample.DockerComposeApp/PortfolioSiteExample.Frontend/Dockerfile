#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["PortfolioSiteExample.Frontend/PortfolioSiteExample.Frontend.csproj", "PortfolioSiteExample.Frontend/"]
RUN dotnet restore "PortfolioSiteExample.Frontend/PortfolioSiteExample.Frontend.csproj"
COPY . .
WORKDIR "/src/PortfolioSiteExample.Frontend"
RUN dotnet build "PortfolioSiteExample.Frontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PortfolioSiteExample.Frontend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PortfolioSiteExample.Frontend.dll"]