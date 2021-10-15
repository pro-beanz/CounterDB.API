#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["CounterDB.API/CounterDB.API.csproj", "CounterDB.API/"]
RUN dotnet restore "CounterDB.API/CounterDB.API.csproj"
COPY . .
WORKDIR "/src/CounterDB.API"
RUN dotnet build "CounterDB.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CounterDB.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CounterDB.API.dll"]
