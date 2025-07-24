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

> 💡 All secrets are injected via GitHub Actions and are not stored in the repository.

## 🔧 Pre-Setup Requirements
✅ Required on all platforms:
- Git
- .NET SDK 8.0+
- Docker

### 🪟 Windows
- Use Git Bash (not PowerShell or CMD)

## 💻 Setup
### 1. 🧾 Clone and move to the repository.
git clone https://github.com/FlokiPatris/Safetica.git && cd Safetica 

### 2. 🚀 Run the setup script in the same folder where the repository is cloned. (Using Git Bash)
bash setup.sh

## 🧪 Running Tests
### 1. ▶️ Run tests with browser (headless = false)
dotnet test

### 2. 🐳 Run tests in Docker (headless = true)
docker-compose up --build

