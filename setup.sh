#!/bin/bash

echo "📁 Navigating to project root..."
cd "$(dirname "$0")"

echo "📁 Creating xUnit test project in src/..."
mkdir -p src
cd src
dotnet new xunit -n SafeticaTests

echo "🔧 Restoring dependencies..."
cd SafeticaTests
dotnet restore

echo "🧪 Installing Playwright browsers..."
pwsh bin/Debug/net8.0/playwright.ps1 install

echo "✅ Setup complete. You can now run: dotnet test"