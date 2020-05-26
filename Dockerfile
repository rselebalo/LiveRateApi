FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 5001
ENV ASPNETCORE_URLS=http://*:5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["LiveRatesApi.csproj", "./"]
RUN dotnet restore "./LiveRatesApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "LiveRatesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LiveRatesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LiveRatesApi.dll"]
