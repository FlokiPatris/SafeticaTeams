# Safetica
Safetica tests with pipeline set up.

## 🔐 Configuration

This project uses **GitHub Secrets** to securely inject credentials and configuration.  
No `.env` file is required or supported.

### Secrets used:
- `TEAMS_LOGIN`
- `TEAMS_PASSWORD`
- `TEAMS_CHAT_NAME`
- `TEAMS_BASE_URL`
- `HEADLESS`

> 💡 All secrets are injected via GitHub Actions and are not stored in the repository.

## 💻 Setup

### 1. 🧾 Clone the repository
git clone https://github.com/FlokiPatris/Safetica.git      
cd Safetica

### 2. ⚙️ Install .NET SDK (if not already installed)
Make sure you have .NET SDK 8.0+ installed.
Check version:     
dotnet --version

### 3. 📦 Restore dependencies
dotnet restore

### 4. 🧪 Install Playwright browsers
pwsh bin/Debug/net8.0/playwright.ps1 install

💡 If you're on Linux/macOS, use:
npx playwright install

### 5. ▶️ Run the test
dotnet test

Or with headless toggle:
HEADLESS=false dotnet test

### 6. 🐳 Run in Docker (if supported)
docker-compose up --build
