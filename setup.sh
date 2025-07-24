#!/bin/bash
# filepath: c:\Users\fkotr\Desktop\safeticaTeams\Safetica\setup.sh

echo "🧾 Step 1: Cloning the repository..."
git clone https://github.com/FlokiPatris/Safetica.git
cd Safetica || exit

echo "⚙️ Step 2: Checking .NET SDK installation..."
dotnet --version || { echo "❌ .NET SDK not found. Please install it first."; exit 1; }

echo "🚀 Step 3: Setting up xUnit test project..."
mkdir -p src
cd src
dotnet new xunit -n SafeticaTests

echo "🔧 Restoring dependencies..."
cd SafeticaTests
dotnet restore

echo "🧪 Installing Playwright browsers..."
pwsh bin/Debug/net8.0/playwright.ps1 install

echo "✅ Setup complete. You are now in src/SafeticaTests. You can run: