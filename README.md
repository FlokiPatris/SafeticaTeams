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

## 📁 Project Structure

Safetica/
├── 📂 src/                         → Source code for Playwright tests
│   ├── 📁 Config/                  → Configuration model (TestConfig.cs)
│   ├── 📁 Utils/                   → Environment loader (ConfigLoader.cs)
│   ├── 📁 Pages/                   → Page Object for Teams interactions
│   └── 📁 Tests/                   → Unified test file (TeamsTests.cs)

├── 📁 TestData/                    → Sample files used for upload tests
├── 📁 Downloads/                   → Folder for downloaded files (used in assertions)

├── 🐳 Dockerfile                   → Docker build configuration
├── 🐳 docker-compose.yml           → Docker orchestration and environment injection

├── ⚙️ .github/
│   └── 📁 workflows/
│       └── 🧪 test.yml             → GitHub Actions workflow for CI/CD

├── 📄 README.md                    → Project documentation and setup instructions
├── 🚫 .gitignore                   → Git tracking exclusions

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