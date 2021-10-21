FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY ["src/Ozon.Route256.MerchandiseService/Ozon.Route256.MerchandiseService.csproj", "src/Ozon.Route256.MerchandiseService/"]
RUN dotnet restore "src/Ozon.Route256.MerchandiseService/Ozon.Route256.MerchandiseService.csproj"

COPY . .
WORKDIR "/src/src/Ozon.Route256.MerchandiseService"
RUN dotnet build "Ozon.Route256.MerchandiseService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ozon.Route256.MerchandiseService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS final
WORKDIR /app

EXPOSE 80

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Ozon.Route256.MerchandiseService.dll"]
