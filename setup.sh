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
echo "▶️ To run tests locally (without Docker):"
echo "   Make sure you have .NET SDK installed and run one of the following:"
echo ""
echo "   🔍 Run with browser UI (headless = false):"
echo "     TEAMS_LOGIN=your_email@example.com \\"
echo "     TEAMS_PASSWORD=your_secure_password \\"
echo "     TEAMS_CHAT_NAME=\"Your Teams Chat Name\" \\"
echo "     TEAMS_BASE_URL=https://teams.microsoftExample.com/ \\"
echo "     HEADLESS=false \\"
echo "     dotnet test"
echo ""
echo "   ⚡ Run in headless mode (no browser UI):"
echo "     TEAMS_LOGIN=your_email@example.com \\"
echo "     TEAMS_PASSWORD=your_secure_password \\"
echo "     TEAMS_CHAT_NAME=\"Your Teams Chat Name\" \\"
echo "     TEAMS_BASE_URL=https://teams.microsoftExample.com/ \\"
echo "     dotnet test"
echo ""
echo "🐳 To run tests in Docker (always headless):"
echo "   Set the following environment variables in your shell or .env file:"
echo "     TEAMS_LOGIN=your_email@example.com"
echo "     TEAMS_PASSWORD=your_secure_password"
echo "     TEAMS_CHAT_NAME=\"Your Teams Chat Name\""
echo "     TEAMS_BASE_URL=https://teams.microsoftExample.com/"
echo ""
echo "   Then run:"
echo "     docker-compose up --build"
echo ""
echo "📦 Mounted project path: /app"
echo "🧪 Tests run from: /app/SafeticaTests inside the container"
echo "========================================"
echo ""

