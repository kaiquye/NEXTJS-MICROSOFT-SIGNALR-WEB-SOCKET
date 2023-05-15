FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebSocket.Api/WebSocket.Api.csproj", "WebSocket.Api/"]
RUN dotnet restore "WebSocket.Api/WebSocket.Api.csproj"
COPY . .
WORKDIR "/src/WebSocket.Api"
RUN dotnet build "WebSocket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebSocket.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebSocket.Api.dll"]
