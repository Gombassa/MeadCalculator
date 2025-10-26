# Multi-stage build optimized for Google Cloud Run
# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src

# Copy project files
COPY ["MeadCalculator.API/MeadCalculator.API.csproj", "MeadCalculator.API/"]
RUN dotnet restore "MeadCalculator.API/MeadCalculator.API.csproj"

# Copy source and build
COPY . .
WORKDIR "/src/MeadCalculator.API"
RUN dotnet build "MeadCalculator.API.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "MeadCalculator.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage - minimal image for Cloud Run
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine
WORKDIR /app

# Create non-root user for security
RUN addgroup -S dotnet && adduser -S dotnet -G dotnet
USER dotnet

# Copy published application
COPY --from=publish /app/publish .

# Cloud Run requires port 8080 by default
EXPOSE 8080

# Set environment for Cloud Run
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Health check for Cloud Run
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD dotnet-httpclient-health-check || exit 1

ENTRYPOINT ["dotnet", "MeadCalculator.API.dll"]
