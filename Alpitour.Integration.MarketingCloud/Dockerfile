#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:2.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:2.1 AS build
WORKDIR /src
COPY ["Alpitour.Integration.MarketingCloud/Alpitour.Integration.MarketingCloud.csproj", "Alpitour.Integration.MarketingCloud/"]
RUN dotnet restore "Alpitour.Integration.MarketingCloud/Alpitour.Integration.MarketingCloud.csproj"
COPY . .
WORKDIR "/src/Alpitour.Integration.MarketingCloud"
RUN dotnet build "Alpitour.Integration.MarketingCloud.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Alpitour.Integration.MarketingCloud.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Alpitour.Integration.MarketingCloud.dll"]