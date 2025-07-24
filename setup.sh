echo ""
echo "🔧 Safetica Setup Starting..."
echo "========================================"

echo "⚙️  Step 1: Checking .NET SDK installation..."
dotnet --version || { echo "❌ .NET SDK not found. Please install it first."; exit 1; }

echo ""
echo "🐳 Step 2: Checking Docker installation..."
docker --version || { echo "❌ Docker not found. Please install Docker to run tests in containers."; }

echo ""
echo "🚀 Step 3: Creating test project folder..."
mkdir -p src/SafeticaTests
cd src/SafeticaTests

echo ""
echo "📦 Step 4: Initializing empty class library..."
dotnet new classlib

echo ""
echo "📦 Step 4: Adding xUnit and Playwright dependencies..."
dotnet add package Microsoft.Playwright
dotnet add package xunit
dotnet add package xunit.runner.visualstudio

echo ""
echo "🔧 Step 5: Restoring dependencies..."
dotnet restore

echo ""
echo "🌐 Step 6: Installing Playwright browsers..."
npx playwright install

echo ""
echo "✅ Setup Complete!"
echo "========================================"
echo "📁 You are now in: src/SafeticaTests"
echo ""
echo "▶️ To run tests locally:      dotnet test"
echo "🐳 To run tests in Docker:    docker-compose up --build"
echo "========================================"
echo ""