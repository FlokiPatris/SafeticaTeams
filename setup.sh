#!/bin/bash

echo "⚙️ Step 1: Checking .NET SDK installation..."
dotnet --version || { echo "❌ .NET SDK not found. Please install it first."; exit 1; }

echo "🐳 Step 2: Checking Docker installation..."
docker --version || { echo "❌ Docker not found. Please install Docker to run tests in containers."; }

echo "🚀 Step 3: Creating test project folder..."
mkdir -p src/SafeticaTests
cd src/SafeticaTests

echo "🧪 Step 4: Adding xUnit and Playwright dependencies..."
dotnet add package Microsoft.Playwright
dotnet add package xunit
dotnet add package xunit.runner.visualstudio

echo "🔧 Step 5: Restoring dependencies..."
dotnet restore

echo "🧪 Step 6: Installing Playwright browsers..."
pwsh bin/Debug/net8.0/playwright.ps1 install

echo "✅ Setup complete. You are now in src/SafeticaTests."
echo "▶️ You can run tests with: dotnet test"
echo "🐳 Or run headless in Docker with: docker-compose up --build"