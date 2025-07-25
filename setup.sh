#!/bin/bash

echo ""
echo "🔧 Safetica Setup Starting..."
echo "========================================"

echo "⚙️  Step 1: SAFETICA_PROJECT_ROOT environment variable setup..."
export SAFETICA_PROJECT_ROOT="$(pwd)"
echo "📁 SAFETICA_PROJECT_ROOT set to '$SAFETICA_PROJECT_ROOT'"
echo ""

echo "⚙️  Step 2: Checking .NET SDK installation..."
if ! dotnet --version > /dev/null 2>&1; then
  echo "❌ .NET SDK not found. Please install it from:"
  echo "   https://dotnet.microsoft.com/download"
  exit 1
else
  echo "✅ .NET SDK is installed."
fi
echo ""

echo "🐳 Step 3: Checking Docker installation..."
if ! docker --version > /dev/null 2>&1; then
  echo "❌ Docker not found. Please install it from:"
  echo "   https://www.docker.com/"
  exit 1
else
  echo "✅ Docker is installed."
fi
echo ""

echo ""
echo "📁 Step 4: Navigating to test project folder..."
TEST_PROJECT="SafeticaTests"
if [ ! -d "$TEST_PROJECT" ]; then
  echo "❌ Folder $TEST_PROJECT not found."
  exit 1
fi
cd "$TEST_PROJECT"

echo ""
echo "📦 Step 5: Verifying project file..."
if [ ! -f "SafeticaTests.csproj" ]; then
  echo "❌ Project file not found in $TEST_PROJECT."
  exit 1
else
  echo "✅ Project file found: SafeticaTests.csproj"
fi

echo ""
echo "🔧 Step 6: Restoring dependencies..."
dotnet restore

echo ""
echo "🌐 Step 7: Installing Playwright browsers..."
if ! command -v npx; then
  echo "❌ npx not found. Please install Node.js from https://nodejs.org/"
  exit 1
fi
npx playwright install

echo ""
echo "🧩 Step 8: Creating solution file and adding project..."
cd ..

if [ ! -f "Safetica.sln" ]; then
  dotnet new sln -n Safetica
  echo "✅ Solution file created: Safetica.sln"
else
  echo "✅ Solution file already exists."
fi

# Check if project is already added to solution
if ! dotnet sln Safetica.sln list | grep -q "SafeticaTests.csproj"; then
  dotnet sln Safetica.sln add SafeticaTests/SafeticaTests.csproj
  echo "✅ Project added to solution."
else
  echo "✅ Project already linked in solution."
fi

echo ""
echo "✅ Setup Complete!"
echo "========================================"
echo "📁 You are now in: $(pwd)"
echo ""
echo "▶️ To run tests locally:      dotnet test"
echo "🐳 To run tests in Docker:    docker-compose up --build"
echo "========================================"
echo ""