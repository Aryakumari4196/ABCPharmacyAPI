# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

WORKDIR /src/ABCPharmacyAPI

RUN dotnet restore "ABCPharmacyAPI.csproj"

RUN dotnet publish "ABCPharmacyAPI.csproj" \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "ABCPharmacyAPI.dll"]