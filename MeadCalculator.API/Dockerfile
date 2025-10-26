# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["MeadCalculator.API/MeadCalculator.API.csproj", "MeadCalculator.API/"]
RUN dotnet restore "MeadCalculator.API/MeadCalculator.API.csproj"

COPY . .
WORKDIR "/src/MeadCalculator.API"
RUN dotnet build "MeadCalculator.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeadCalculator.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "MeadCalculator.API.dll"]
