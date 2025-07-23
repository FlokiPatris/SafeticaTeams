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
dotnet --version

### 3. 🚀 Run the setup script
bash setup.sh

### 4. ▶️ Run the test
cd src/SafeticaTests       
dotnet test

Or with headless toggle:
HEADLESS=false dotnet test

### 5. 🐳 Run in Docker (optional)
docker-compose up --build
