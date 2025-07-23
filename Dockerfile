# Use official .NET SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy project files
COPY . .

# Restore dependencies
RUN dotnet restore

# Install Playwright browsers
RUN pwsh bin/Debug/net8.0/playwright.ps1 install

# Run tests
CMD ["dotnet", "test"]